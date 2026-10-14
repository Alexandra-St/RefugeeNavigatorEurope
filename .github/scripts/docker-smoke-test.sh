#!/usr/bin/env bash
set -euo pipefail

image="${1:?Usage: docker-smoke-test.sh IMAGE}"
work_dir="$(mktemp -d)"
public_container="refugee-navigator-public-${GITHUB_RUN_ID:-local}"
private_container="refugee-navigator-private-${GITHUB_RUN_ID:-local}"

cleanup() {
  docker rm --force "$public_container" "$private_container" >/dev/null 2>&1 || true
  rm -rf "$work_dir"
}
trap cleanup EXIT

fail() {
  printf 'Docker smoke test failed: %s\n' "$1" >&2
  exit 1
}

start_container() {
  local name="$1"
  shift
  docker run --detach --name "$name" --publish 127.0.0.1::10000 "$@" "$image" >/dev/null
  local published_port
  published_port="$(docker port "$name" 10000/tcp | sed 's/.*://')"
  [[ "$published_port" =~ ^[0-9]+$ ]] || fail "could not determine the published port for $name"
  printf 'http://127.0.0.1:%s' "$published_port"
}

request_status() {
  local base_url="$1"
  local path="$2"
  shift 2
  local status
  status="$(curl --silent --connect-timeout 2 --max-time 10 \
    --output "$work_dir/response-body" --write-out '%{http_code}' "$@" "$base_url$path")" \
    || status="000"
  printf '%s' "$status"
}

assert_status() {
  local expected="$1"
  local base_url="$2"
  local path="$3"
  shift 3
  local actual
  actual="$(request_status "$base_url" "$path" "$@")"
  [[ "$actual" == "$expected" ]] || fail "$path returned HTTP $actual; expected $expected"
}

wait_for_status() {
  local expected="$1"
  local base_url="$2"
  local path="$3"
  shift 3
  local attempt actual
  for attempt in $(seq 1 30); do
    actual="$(request_status "$base_url" "$path" "$@")"
    [[ "$actual" == "$expected" ]] && return 0
    sleep 2
  done
  fail "$path did not become ready with HTTP $expected"
}

check_startup_logs() {
  local container="$1"
  if docker logs "$container" >"$work_dir/$container.log" 2>&1; then
    if grep --ignore-case --extended-regexp \
      '(^|[[:space:]])(crit|critical|fatal|fail):|Unhandled exception|Application startup exception' \
      "$work_dir/$container.log" >/dev/null; then
      fail "$container startup logs contain a fatal application error"
    fi
  else
    fail "could not read startup logs for $container"
  fi
}

expect_startup_failure() {
  local label="$1"
  local expected_message="$2"
  shift 2
  local status
  status=0
  timeout 15s docker run --rm "$@" "$image" >"$work_dir/$label.log" 2>&1 || status=$?
  [[ "$status" -ne 0 && "$status" -ne 124 ]] || fail "$label configuration unexpectedly started or timed out"
  grep --fixed-strings "$expected_message" "$work_dir/$label.log" >/dev/null \
    || fail "$label configuration failed for an unexpected reason"
  printf '%s: rejected as expected\n' "$label"
}

printf 'Checking production image in public packaging mode...\n'
public_url="$(start_container "$public_container" --env PORT=10000 --env PRIVATE_ALPHA_ACCESS_MODE=public)"
wait_for_status 200 "$public_url" /

for route in \
  / \
  /search \
  /compare \
  /countries/denmark \
  /countries/denmark/categories/employment; do
  assert_status 200 "$public_url" "$route"
done

assert_status 200 "$public_url" /
mapfile -t stylesheet_urls < <(grep --only-matching --extended-regexp 'href="[^"]+\.css[^"]*"' \
  "$work_dir/response-body" | sed -E 's/^href="([^"]+)"$/\1/')
[[ "${#stylesheet_urls[@]}" -ge 2 ]] || fail "the page did not publish both global and isolated CSS assets"
primary_stylesheet_path=""
for stylesheet_url in "${stylesheet_urls[@]}"; do
  [[ "$stylesheet_url" != http* ]] || fail "a stylesheet unexpectedly points outside the application"
  [[ "$stylesheet_url" == /* ]] || stylesheet_url="/$stylesheet_url"
  assert_status 200 "$public_url" "$stylesheet_url"
  [[ -n "$primary_stylesheet_path" ]] || primary_stylesheet_path="$stylesheet_url"
done
assert_status 404 "$public_url" /unknown-ci-route

for culture in uk en ru; do
  case "$culture" in
    uk) localized_country='Данія' ;;
    en) localized_country='Denmark' ;;
    ru) localized_country='Дания' ;;
  esac
  assert_status 200 "$public_url" /countries/denmark -H "Accept-Language: $culture"
  grep --ignore-case --fixed-strings "<html lang=\"$culture\"" "$work_dir/response-body" >/dev/null \
    || fail "the $culture document language was not rendered"
  grep --fixed-strings "$localized_country" "$work_dir/response-body" >/dev/null \
    || fail "the $culture country translation was not rendered"
done
check_startup_logs "$public_container"
printf 'Public-mode routes, catalogs, localization and CSS: passed\n'

printf 'Checking production image in simulated private Render configuration...\n'
# Synthetic CI-only credentials. Do not enable shell tracing or print these values.
test_username='ci-alpha-user'
test_password='CI-Only-Private-Alpha-2026!'
private_url="$(start_container "$private_container" \
  --env PORT=10000 \
  --env PRIVATE_ALPHA_ACCESS_MODE=private \
  --env PRIVATE_ALPHA_TLS_MODE=render-free-edge \
  --env RENDER=true \
  --env RENDER_SERVICE_TYPE=web \
  --env RENDER_EXTERNAL_URL=https://ci-smoke.onrender.com \
  --env "PRIVATE_ALPHA_USERNAME=$test_username" \
  --env "PRIVATE_ALPHA_PASSWORD=$test_password")"
wait_for_status 401 "$private_url" /

assert_status 401 "$private_url" /
assert_status 401 "$private_url" / --user "$test_username:incorrect-ci-password"
assert_status 200 "$private_url" / --user "$test_username:$test_password"
assert_status 401 "$private_url" "$primary_stylesheet_path"
assert_status 200 "$private_url" "$primary_stylesheet_path" --user "$test_username:$test_password"
assert_status 401 "$private_url" '/set-language?culture=en&returnUrl=%2F'

language_status="$(curl --silent --connect-timeout 2 --max-time 10 \
  --user "$test_username:$test_password" \
  --dump-header "$work_dir/language-headers" --output /dev/null --write-out '%{http_code}' \
  "$private_url/set-language?culture=en&returnUrl=%2F")" || language_status="000"
[[ "$language_status" == 302 ]] || fail "/set-language returned HTTP $language_status; expected 302"
grep --ignore-case --extended-regexp '^Set-Cookie:.*;[[:space:]]*secure([;[:space:]]|$)' \
  "$work_dir/language-headers" >/dev/null || fail "the language cookie was not marked Secure"

grep --ignore-case --fixed-strings 'X-Robots-Tag: noindex, nofollow' \
  "$work_dir/language-headers" >/dev/null || fail "private response omitted X-Robots-Tag"

for route in /search /compare; do
  assert_status 200 "$private_url" "$route" --user "$test_username:$test_password"
done
check_startup_logs "$private_container"
printf 'Private-mode authentication, assets, language cookie and routes: passed\n'

expect_startup_failure missing-access-mode 'PRIVATE_ALPHA_ACCESS_MODE must be explicitly set' \
  --env ASPNETCORE_ENVIRONMENT=Production
expect_startup_failure invalid-private-configuration 'PRIVATE_ALPHA_USERNAME must be non-empty' \
  --env ASPNETCORE_ENVIRONMENT=Production --env PRIVATE_ALPHA_ACCESS_MODE=private

printf 'Docker container smoke tests passed.\n'
