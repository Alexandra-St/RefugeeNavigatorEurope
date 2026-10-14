using System.Net;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace RefugeeNavigator.Web.Security;

public enum PrivateAlphaAccessMode
{
    Development,
    Private,
    Public
}

public enum PrivateAlphaTlsMode
{
    Strict,
    RenderFreeEdge
}

public sealed class PrivateAlphaAccessOptions
{
    private readonly byte[]? _usernameHash;
    private readonly byte[]? _passwordHash;

    private PrivateAlphaAccessOptions(
        PrivateAlphaAccessMode mode,
        PrivateAlphaTlsMode tlsMode,
        bool isDevelopment,
        string? username,
        string? password)
    {
        Mode = mode;
        TlsMode = tlsMode;
        IsDevelopment = isDevelopment;

        if (mode == PrivateAlphaAccessMode.Private)
        {
            _usernameHash = HashCredential(username!);
            _passwordHash = HashCredential(password!);
        }
    }

    public PrivateAlphaAccessMode Mode { get; }

    public PrivateAlphaTlsMode TlsMode { get; }

    public bool UsesRenderFreeEdgeTls => TlsMode == PrivateAlphaTlsMode.RenderFreeEdge;

    public bool IsDevelopment { get; }

    public bool RequiresAuthentication => Mode == PrivateAlphaAccessMode.Private;

    public static PrivateAlphaAccessOptions FromConfiguration(
        IConfiguration configuration,
        bool isDevelopment)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        var tlsMode = configuration["PRIVATE_ALPHA_TLS_MODE"]?.Trim().ToLowerInvariant() switch
        {
            null or "" or "strict" => PrivateAlphaTlsMode.Strict,
            "render-free-edge" => PrivateAlphaTlsMode.RenderFreeEdge,
            _ => throw new InvalidOperationException(
                "PRIVATE_ALPHA_TLS_MODE must be 'strict' or 'render-free-edge'.")
        };

        var configuredMode = configuration["PRIVATE_ALPHA_ACCESS_MODE"];
        if (string.IsNullOrWhiteSpace(configuredMode) && isDevelopment)
        {
            if (tlsMode == PrivateAlphaTlsMode.RenderFreeEdge)
            {
                ValidateRenderFreeEdgeConfiguration(
                    configuration,
                    PrivateAlphaAccessMode.Development,
                    isDevelopment);
            }

            return new PrivateAlphaAccessOptions(
                PrivateAlphaAccessMode.Development,
                tlsMode,
                isDevelopment,
                null,
                null);
        }

        var mode = configuredMode?.Trim().ToLowerInvariant() switch
        {
            "private" => PrivateAlphaAccessMode.Private,
            "public" => PrivateAlphaAccessMode.Public,
            _ => throw new InvalidOperationException(
                "PRIVATE_ALPHA_ACCESS_MODE must be explicitly set to 'private' or 'public'.")
        };

        if (tlsMode == PrivateAlphaTlsMode.RenderFreeEdge)
        {
            ValidateRenderFreeEdgeConfiguration(configuration, mode, isDevelopment);
        }

        if (mode == PrivateAlphaAccessMode.Public)
        {
            return new PrivateAlphaAccessOptions(mode, tlsMode, isDevelopment, null, null);
        }

        var username = configuration["PRIVATE_ALPHA_USERNAME"];
        var password = configuration["PRIVATE_ALPHA_PASSWORD"];
        if (string.IsNullOrWhiteSpace(username) || username.Contains(':') || username.Any(char.IsControl))
        {
            throw new InvalidOperationException(
                "PRIVATE_ALPHA_USERNAME must be non-empty and cannot contain a colon or control characters.");
        }

        if (string.IsNullOrWhiteSpace(password) || !IsStrongPassword(password))
        {
            throw new InvalidOperationException(
                "PRIVATE_ALPHA_PASSWORD must be at least 16 characters and include at least three of lowercase, uppercase, digits, and symbols.");
        }

        return new PrivateAlphaAccessOptions(mode, tlsMode, isDevelopment, username, password);
    }

    public static IReadOnlyList<IPAddress> ParseTrustedProxyAddresses(string? configuredAddresses)
    {
        if (string.IsNullOrWhiteSpace(configuredAddresses))
        {
            return Array.Empty<IPAddress>();
        }

        var addresses = new List<IPAddress>();
        foreach (var item in configuredAddresses.Split(',', StringSplitOptions.TrimEntries))
        {
            if (item.Length == 0 || !IPAddress.TryParse(item, out var address))
            {
                throw new InvalidOperationException(
                    "PRIVATE_ALPHA_TRUSTED_PROXY_IPS must contain only comma-separated IP addresses.");
            }

            addresses.Add(address);
        }

        return addresses;
    }

    public bool MatchesCredentials(string username, string password)
    {
        if (!RequiresAuthentication || _usernameHash is null || _passwordHash is null)
        {
            return false;
        }

        var usernameMatches = CryptographicOperations.FixedTimeEquals(
            _usernameHash,
            HashCredential(username));
        var passwordMatches = CryptographicOperations.FixedTimeEquals(
            _passwordHash,
            HashCredential(password));

        return usernameMatches & passwordMatches;
    }

    private static bool IsStrongPassword(string password)
    {
        if (password.Length < 16)
        {
            return false;
        }

        var characterTypes = 0;
        characterTypes += password.Any(char.IsLower) ? 1 : 0;
        characterTypes += password.Any(char.IsUpper) ? 1 : 0;
        characterTypes += password.Any(char.IsDigit) ? 1 : 0;
        characterTypes += password.Any(character => !char.IsLetterOrDigit(character)) ? 1 : 0;
        return characterTypes >= 3;
    }

    private static void ValidateRenderFreeEdgeConfiguration(
        IConfiguration configuration,
        PrivateAlphaAccessMode mode,
        bool isDevelopment)
    {
        if (isDevelopment || mode != PrivateAlphaAccessMode.Private)
        {
            throw new InvalidOperationException(
                "PRIVATE_ALPHA_TLS_MODE=render-free-edge requires non-Development private access mode.");
        }

        if (!string.Equals(configuration["RENDER"]?.Trim(), "true", StringComparison.OrdinalIgnoreCase)
            || !string.Equals(configuration["RENDER_SERVICE_TYPE"]?.Trim(), "web", StringComparison.OrdinalIgnoreCase)
            || !IsRenderExternalUrl(configuration["RENDER_EXTERNAL_URL"]))
        {
            throw new InvalidOperationException(
                "PRIVATE_ALPHA_TLS_MODE=render-free-edge requires Render web-service runtime indicators and an HTTPS onrender.com external URL.");
        }

        if (!string.IsNullOrWhiteSpace(configuration["PRIVATE_ALPHA_TRUSTED_PROXY_IPS"]))
        {
            throw new InvalidOperationException(
                "PRIVATE_ALPHA_TLS_MODE=render-free-edge cannot be combined with PRIVATE_ALPHA_TRUSTED_PROXY_IPS.");
        }
    }

    private static bool IsRenderExternalUrl(string? configuredUrl)
    {
        return Uri.TryCreate(configuredUrl, UriKind.Absolute, out var uri)
            && uri.Scheme == Uri.UriSchemeHttps
            && uri.Host.EndsWith(".onrender.com", StringComparison.OrdinalIgnoreCase)
            && uri.UserInfo.Length == 0
            && uri.Query.Length == 0
            && uri.Fragment.Length == 0
            && uri.AbsolutePath == "/";
    }

    private static byte[] HashCredential(string credential) =>
        SHA256.HashData(Encoding.UTF8.GetBytes(credential));
}
