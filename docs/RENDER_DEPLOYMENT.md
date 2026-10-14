# Render deployment preparation

This repository includes a production Dockerfile and explicit TLS compatibility mode for a later Render Free Web Service deployment. No Render service has been created or deployed. **Before enabling private traffic, an operator must confirm in the Render Dashboard that the actual service compute plan is Free.** Runtime environment variables do not identify or prove the selected plan.

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

GitHub Actions builds the root production Dockerfile without pushing the image and runs public-mode and synthetic private-mode container smoke tests. These checks verify application behavior under the supplied configuration; they do not verify a real Render service, its selected compute plan, or traffic through Render’s HTTPS edge. Public mode is used only in isolated CI packaging checks and must not be used for the Private Alpha deployment.

To test private authentication locally, provide synthetic credentials through environment variables. Do not commit them or put real credentials in shell history. The password must be at least 16 characters and contain at least three character types. In strict mode, a direct HTTP connection to a production-mode private container intentionally receives HTTP 426; test authenticated private requests through a local HTTPS-terminating proxy configured as an exact trusted proxy. Do not use `render-free-edge` for ordinary local access: it deliberately treats requests as HTTPS, and manually supplied Render environment variables do not prove that the request passed through Render's edge.

## Runtime configuration

Render supplies `PORT` (default `10000`). The Docker entry point binds Kestrel to `http://0.0.0.0:${PORT:-10000}`. TLS is terminated at Render's edge; the container itself serves HTTP and does not need a certificate.

Required application variables for a private alpha:

| Variable | Configuration |
| --- | --- |
| `PRIVATE_ALPHA_ACCESS_MODE` | Set to `private`. |
| `PRIVATE_ALPHA_USERNAME` | Set a non-empty username without a colon or control characters. |
| `PRIVATE_ALPHA_PASSWORD` | Set a unique strong secret meeting the application's length and character-type requirements. |
| `PRIVATE_ALPHA_TLS_MODE` | Set to `render-free-edge` only after confirming the service is actually on Render Free. |
| `RENDER` | Render-provided value must be `true`; configuration sanity check only. |
| `RENDER_SERVICE_TYPE` | Render-provided value must be `web`; configuration sanity check only. |
| `RENDER_EXTERNAL_URL` | Render-provided HTTPS `onrender.com` URL; configuration sanity check only. |
| `PRIVATE_ALPHA_TRUSTED_PROXY_IPS` | Leave unset in `render-free-edge` mode. It is incompatible with that mode. |

Set credentials in Render's encrypted environment-variable settings, never in the Dockerfile, image, repository, URL, or logs. Do not set `ASPNETCORE_FORWARDEDHEADERS_ENABLED=true` as a workaround.

## Render Free trust boundary and limitations

Render documents that Free Web Services cannot receive incoming private-network traffic, the web-service application port is not directly reachable over the public internet, and the public ingress redirects HTTP to HTTPS before terminating TLS and forwarding the request over HTTP to the app. `render-free-edge` relies on those documented properties: the app sets HTTPS request semantics only after explicit configuration validation, without trusting `X-Forwarded-*` headers. [Render Free](https://render.com/docs/free), [private network](https://render.com/docs/private-network), [web services](https://render.com/docs/web-services), and [TLS](https://render.com/docs/tls).

The mode's environment checks (`RENDER=true`, service type `web`, and HTTPS external URL) are configuration sanity checks only. They do not authenticate individual requests and do not prove that the plan is Free. Confirm the compute plan independently in the Render Dashboard every time this mode is enabled. Do not proceed if that confirmation is unavailable. If the service is upgraded to a paid plan, disable this mode immediately and review network access before serving private traffic; paid services have different private-network capabilities.

The internal edge-to-container connection uses HTTP, not end-to-end TLS. Basic Auth credentials therefore pass through Render-managed infrastructure on that HTTP hop. Render describes private networking as isolated from the public internet, but this application mode depends on Render's Free-plan ingress/network behavior; it does not cryptographically authenticate each request as edge-originated. Use a strong, unique shared password that is not used for other accounts. Do not enable this mode on paid Render services or another host.

## Health checks

Keep Render's default TCP health check for the initial private alpha. It verifies that the process accepts a connection on its listening port without adding a public unauthenticated route. Do not configure an HTTP health-check path unless a later security review approves its interaction with the authentication middleware.

## Free instance considerations

Render Free web services spin down after 15 minutes without inbound activity and take about a minute to start when requested again. Their filesystems are ephemeral; local changes are lost after restart, spin-down, or redeploy. The current JSON catalogs are bundled with the application image, so they are available at startup, but runtime edits to those files would not persist. Free services can also be restarted at any time, have limited monthly instance hours, and are not intended for production workloads. Review Render's current plan terms before choosing a service plan.

## Later deployment steps

After an operator confirms the actual Render Free compute plan in the Dashboard:

1. Create a Render Web Service for this repository and select Docker with the root `Dockerfile`.
2. Keep the default TCP health check and confirm Render uses the container's bound port.
3. Configure `PRIVATE_ALPHA_ACCESS_MODE=private`, `PRIVATE_ALPHA_TLS_MODE=render-free-edge`, and private credentials in Render's secret settings. Do not set `PRIVATE_ALPHA_TRUSTED_PROXY_IPS` in this mode.
4. Deploy and test HTTPS, authentication challenges, cookies, localized routes, catalog loading, and static assets through the public endpoint.
5. Confirm unauthenticated page, asset, and language-setting requests remain protected; verify no secrets appear in logs or the image.

## Security checklist

- [ ] `PRIVATE_ALPHA_ACCESS_MODE=private` is explicit.
- [ ] Username and password are unique secrets set outside source control.
- [ ] HTTPS is active at the public edge.
- [ ] Render Dashboard independently confirms the service compute plan is Free.
- [ ] `RENDER=true`, `RENDER_SERVICE_TYPE=web`, and HTTPS `RENDER_EXTERNAL_URL` pass startup validation; these values are not treated as proof of the compute plan or per-request origin.
- [ ] `PRIVATE_ALPHA_TLS_MODE=render-free-edge` is used only on that confirmed Free Web Service; no forwarded headers are trusted.
- [ ] If the service is upgraded, render-free-edge mode is removed and the network trust boundary is reviewed before serving traffic.
- [ ] The operator accepts that the edge-to-container HTTP hop is not end-to-end TLS and uses a unique Basic Auth password.
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

## Post-deployment verification

After deployment is approved and the service URL and credentials are available, run the read-only verification from a trusted local environment. The script requires `RENDER_TARGET_URL`, `PRIVATE_ALPHA_USERNAME`, and `PRIVATE_ALPHA_PASSWORD` in the process environment; it accepts only an HTTPS `*.onrender.com` origin, uses bounded requests, does not follow redirects, and never prints credentials or Authorization headers. Provide credentials interactively and do not place them in a file or command history. See [the Private Alpha launch checklist](PRIVATE_ALPHA_LAUNCH_CHECKLIST.md) for a safe invocation and manual acceptance steps.

The script checks a live deployment only when explicitly run with a real URL. CI tests its logic using synthetic responses and never contacts Render. A passing script does not verify the actual Render compute plan or independently prove Render edge behavior; confirm those manually in the dashboard and browser.
