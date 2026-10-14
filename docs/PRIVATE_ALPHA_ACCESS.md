# Private alpha access

The application supports explicit `private` and `public` access modes. Private mode uses HTTP Basic Authentication for every request, including pages, static assets, and `/set-language`.

## Configuration

Configure these environment variables on the application host:

| Variable | Required value |
| --- | --- |
| `PRIVATE_ALPHA_ACCESS_MODE` | `private` or `public`; required outside Development. |
| `PRIVATE_ALPHA_USERNAME` | Required in private mode. Do not use a colon or control characters. |
| `PRIVATE_ALPHA_PASSWORD` | Required in private mode; at least 16 characters with at least three of lowercase, uppercase, digits, and symbols. Use a unique randomly generated password. |
| `PRIVATE_ALPHA_TRUSTED_PROXY_IPS` | Optional comma-separated list of exact proxy IP addresses. Only these proxies may supply forwarded scheme and client-IP headers. |
| `PRIVATE_ALPHA_TLS_MODE` | Optional. Defaults to `strict`. Set to `render-free-edge` only for a Render Free Web Service after confirming the compute plan in the Render Dashboard. |

Missing or invalid mode, or invalid private credentials, stops startup. There are no default credentials. In Development, omitting the mode allows local access without authentication; setting the mode to `private` enables the gate for local testing. Public access outside Development requires explicitly setting the mode to `public`.

Never put credentials in source control, URLs, screenshots, or logs. Configure the username and password through the hosting platform's encrypted secret/environment-variable settings, then rotate or remove them when needed. For local private testing, set the same variables only in the local shell environment. For example, use placeholder values in your shell and do not paste a real password into a shared terminal transcript. In strict mode, a non-Development direct HTTP request is rejected with HTTP 426. Do not enable `render-free-edge` for ordinary local access: it deliberately treats requests as HTTPS and its Render environment variables are not proof that a local request passed through Render.

## HTTPS and reverse proxies

HTTP Basic credentials are Base64-encoded, not encrypted. Private mode requires HTTPS outside Development. With the default `strict` mode, if TLS terminates at a reverse proxy, configure only its exact trusted IP address(es) with `PRIVATE_ALPHA_TRUSTED_PROXY_IPS`; forwarded headers from all other sources are ignored. Do not trust arbitrary `X-Forwarded-*` headers. If the application's proxy address is not trusted/configured, private requests are rejected with HTTP 426 rather than accepting credentials over HTTP.

### Render Free edge mode

`PRIVATE_ALPHA_TLS_MODE=render-free-edge` is a separate, explicit exception for **Render Free Web Services only**. Startup requires `PRIVATE_ALPHA_ACCESS_MODE=private`, valid private credentials, `RENDER=true`, `RENDER_SERVICE_TYPE=web`, and a valid HTTPS `RENDER_EXTERNAL_URL` on `onrender.com`. It rejects Development, public access, unknown TLS modes, and a simultaneous `PRIVATE_ALPHA_TRUSTED_PROXY_IPS` value. It does not use or trust client-supplied forwarded headers.

These Render variables are configuration sanity checks, not cryptographic authentication of individual requests and not proof of the service's compute plan. Before enabling this mode, an operator must independently confirm in the Render Dashboard that the service is actually on the Free compute plan. Render documents that Free Web Services cannot receive private-network traffic and that the public application port is reached through its ingress; Render redirects public HTTP to HTTPS and terminates TLS at its edge before forwarding HTTP to the application. The application therefore treats requests as externally HTTPS only under this explicit mode and documented Free-plan network assumption.

The edge-to-container hop is HTTP, so credentials are not protected by end-to-end TLS inside Render-managed infrastructure. Use a unique Basic Auth password that is not used for any other account or service. This mode depends on Render's documented Free-plan network isolation; it does not authenticate a request's origin. Paid instances have different private-network capabilities. If the service is upgraded, immediately remove `render-free-edge` and review the trust boundary before serving private traffic. Do not enable it on paid instances or other hosting platforms.

Private responses include `X-Robots-Tag: noindex, nofollow`. This discourages indexing but does not provide access protection.

## HTTP Basic Authentication limitations

Basic Authentication is a single shared credential, not individual tester accounts. Anyone who knows it can access the app; it does not provide per-person revocation, multi-factor authentication, or an audit trail. Browsers may cache credentials for a session. Use a strong unique password over HTTPS and rotate it if it is shared beyond the invited group. If individual identity or revocation becomes necessary, use a dedicated identity solution in a later phase.

To intentionally disable the gate for a future Public Beta, explicitly set `PRIVATE_ALPHA_ACCESS_MODE=public` in the hosting environment and remove the private username/password secrets. Do not rely on missing configuration to make a non-Development deployment public.
