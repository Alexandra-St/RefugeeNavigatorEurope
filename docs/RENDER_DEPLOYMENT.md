# Render deployment preparation

This repository includes a production Dockerfile for a later Render Web Service deployment. No Render service has been created or deployed. Deployment readiness is currently **blocked by reverse-proxy trust configuration**; do not expose the private alpha until that blocker is resolved.

## Build and run locally

From the repository root:

```sh
docker build -t refugee-navigator-europe:local .
docker run --rm -p 10000:10000 \
  -e PORT=10000 \
  -e PRIVATE_ALPHA_ACCESS_MODE=public \
  refugee-navigator-europe:local
```

This local-only `public` mode is for checking packaging and routes. Do not use it for a private alpha deployment. The container listens on `0.0.0.0` at `PORT`, defaulting to `10000`. Its working directory is `/app`, which is also the application content root; published JSON catalogs are copied there by the web project, and the publish output includes the Blazor static assets and embedded localization resources.

To test private authentication locally, provide synthetic credentials through environment variables. Do not commit them or put real credentials in shell history. The password must be at least 16 characters and contain at least three character types. A direct HTTP connection to a production-mode private container intentionally receives HTTP 426; test authenticated private requests only through a local HTTPS-terminating proxy configured as an exact trusted proxy.

## Runtime configuration

Render supplies `PORT` (default `10000`). The Docker entry point binds Kestrel to `http://0.0.0.0:${PORT:-10000}`. TLS is terminated at Render's edge; the container itself serves HTTP and does not need a certificate.

Required application variables for a private alpha:

| Variable | Configuration |
| --- | --- |
| `PRIVATE_ALPHA_ACCESS_MODE` | Set to `private`. |
| `PRIVATE_ALPHA_USERNAME` | Set a non-empty username without a colon or control characters. |
| `PRIVATE_ALPHA_PASSWORD` | Set a unique strong secret meeting the application's length and character-type requirements. |
| `PRIVATE_ALPHA_TRUSTED_PROXY_IPS` | Set only after Render provides a reliable, supported way to determine the exact source IP(s) of its inbound proxy as seen by the application. No values are currently verified. |

Set credentials in Render's encrypted environment-variable settings, never in the Dockerfile, image, repository, URL, or logs. Do not set `ASPNETCORE_FORWARDEDHEADERS_ENABLED=true` as a workaround.

## Reverse-proxy blocker

Render documents that its edge proxy terminates public HTTPS and forwards plain HTTP to the service, and that applications can use `X-Forwarded-Proto` to learn the original scheme. The existing application only processes forwarded headers when the immediate remote address exactly matches an address in `PRIVATE_ALPHA_TRUSTED_PROXY_IPS`. Render's public documentation reviewed for this preparation does not provide a reliable list or guarantee for the inbound proxy source IP addresses visible to the application. Render's published IP-range documentation concerns outbound traffic and must not be used as a proxy allowlist.

Therefore, `PRIVATE_ALPHA_TRUSTED_PROXY_IPS` cannot currently be populated safely from verified documentation. With private mode and no trusted proxy address, the application rejects requests arriving over the proxy-to-container HTTP hop with HTTP 426. Do not guess addresses, trust all forwarded headers, disable the HTTPS check, or deploy the private service until the trusted-proxy arrangement is confirmed with Render and reviewed. If Render cannot provide stable proxy identities, a separately reviewed authentication/proxy design will be required in a later task.

## Health checks

Keep Render's default TCP health check for the initial private alpha. It verifies that the process accepts a connection on its listening port without adding a public unauthenticated route. Do not configure an HTTP health-check path unless a later security review approves its interaction with the authentication middleware.

## Free instance considerations

Render Free web services spin down after 15 minutes without inbound activity and take about a minute to start when requested again. Their filesystems are ephemeral; local changes are lost after restart, spin-down, or redeploy. The current JSON catalogs are bundled with the application image, so they are available at startup, but runtime edits to those files would not persist. Free services can also be restarted at any time, have limited monthly instance hours, and are not intended for production workloads. Review Render's current plan terms before choosing a service plan.

## Later deployment steps

After the trusted-proxy blocker is resolved:

1. Create a Render Web Service for this repository and select Docker with the root `Dockerfile`.
2. Keep the default TCP health check and confirm Render uses the container's bound port.
3. Configure private-alpha environment variables in Render's secret settings. Supply trusted proxy IPs only from a verified, supported source.
4. Deploy and test HTTPS, authentication challenges, cookies, localized routes, catalog loading, and static assets through the public endpoint.
5. Confirm unauthenticated page, asset, and language-setting requests remain protected; verify no secrets appear in logs or the image.

## Security checklist

- [ ] `PRIVATE_ALPHA_ACCESS_MODE=private` is explicit.
- [ ] Username and password are unique secrets set outside source control.
- [ ] HTTPS is active at the public edge.
- [ ] Proxy source addresses are verified and configured exactly; no broad forwarded-header trust is enabled.
- [ ] The origin is not publicly reachable through an alternate path that bypasses the trusted edge.
- [ ] TCP health checks are used; no authentication exemption or public health endpoint was added.
- [ ] Pages, static assets, and `/set-language` all require authentication.
- [ ] Authenticated HTTP requests, redirects, localized routes, search, comparison, catalogs, and static CSS have been checked after deployment.
- [ ] Free-instance cold starts and ephemeral storage are acceptable for the intended private alpha.

## References

- [Render Docker deployment](https://render.com/docs/docker)
- [Render web services and port binding](https://render.com/docs/web-services)
- [Render TLS termination and web-service request flow](https://render.com/tutorials/web-service-vs-static-site/web-services)
- [Render health checks](https://render.com/docs/health-checks)
- [Render Free instance limitations](https://render.com/docs/free)
- [Render outbound IP addresses](https://render.com/docs/outbound-ip-addresses) (outbound ranges only; not inbound proxy addresses)
