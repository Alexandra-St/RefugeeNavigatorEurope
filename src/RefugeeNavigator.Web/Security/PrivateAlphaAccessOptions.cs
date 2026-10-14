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

public sealed class PrivateAlphaAccessOptions
{
    private readonly byte[]? _usernameHash;
    private readonly byte[]? _passwordHash;

    private PrivateAlphaAccessOptions(
        PrivateAlphaAccessMode mode,
        bool isDevelopment,
        string? username,
        string? password)
    {
        Mode = mode;
        IsDevelopment = isDevelopment;

        if (mode == PrivateAlphaAccessMode.Private)
        {
            _usernameHash = HashCredential(username!);
            _passwordHash = HashCredential(password!);
        }
    }

    public PrivateAlphaAccessMode Mode { get; }

    public bool IsDevelopment { get; }

    public bool RequiresAuthentication => Mode == PrivateAlphaAccessMode.Private;

    public static PrivateAlphaAccessOptions FromConfiguration(
        IConfiguration configuration,
        bool isDevelopment)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        var configuredMode = configuration["PRIVATE_ALPHA_ACCESS_MODE"];
        if (string.IsNullOrWhiteSpace(configuredMode) && isDevelopment)
        {
            return new PrivateAlphaAccessOptions(
                PrivateAlphaAccessMode.Development,
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

        if (mode == PrivateAlphaAccessMode.Public)
        {
            return new PrivateAlphaAccessOptions(mode, isDevelopment, null, null);
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

        return new PrivateAlphaAccessOptions(mode, isDevelopment, username, password);
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

    private static byte[] HashCredential(string credential) =>
        SHA256.HashData(Encoding.UTF8.GetBytes(credential));
}
