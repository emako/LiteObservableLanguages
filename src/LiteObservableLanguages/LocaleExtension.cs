namespace LiteObservableLanguages;

/// <summary>
/// Provides extension methods for string localization.
/// </summary>
public static class LocaleExtension
{
    /// <summary>
    /// Translates the given key using I18NExtension. Returns the localized string or an empty string if not found.
    /// </summary>
    /// <param name="key">The resource key to translate.</param>
    /// <returns>The localized string, or empty string if not found.</returns>
    public static string Tr(this string key)
    {
        try
        {
            return I18NExtension.Translate(key) ?? string.Empty;
        }
        catch (Exception e)
        {
            _ = e;
        }
        return null!;
    }

    /// <summary>
    /// Translates the given key and formats it with the provided arguments.
    /// </summary>
    /// <param name="key">The resource key to translate.</param>
    /// <param name="args">Arguments to format the localized string.</param>
    /// <returns>The formatted localized string.</returns>
    public static string Tr(this string key, params object[] args)
    {
        return string.Format(Tr(key)?.ToString() ?? string.Empty, args);
    }
}
