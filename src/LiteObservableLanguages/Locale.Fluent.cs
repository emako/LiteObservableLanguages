using System.Globalization;
using System.Resources;

namespace LiteObservableLanguages;

/// <summary>
/// Provides fluent configuration methods for the Locale class.
/// </summary>
public partial class Locale
{
    /// <summary>
    /// Sets the ResourceManager to use for localization.
    /// </summary>
    /// <param name="resourceManager">The ResourceManager instance.</param>
    /// <returns>The current Locale instance for chaining.</returns>
    public Locale UseResourceManager(ResourceManager resourceManager)
    {
        ResourceManager = resourceManager;
        return this;
    }

    /// <summary>
    /// Sets the fallback culture to use if a resource is not found.
    /// </summary>
    /// <param name="fallback">The fallback CultureInfo.</param>
    /// <returns>The current Locale instance for chaining.</returns>
    public Locale UseFallback(CultureInfo fallback)
    {
        Fallback = fallback;
        return this;
    }

    /// <summary>
    /// Sets the current culture for localization.
    /// </summary>
    /// <param name="culture">The CultureInfo to use.</param>
    /// <returns>The current Locale instance for chaining.</returns>
    public Locale UseCulture(CultureInfo culture)
    {
        Culture = culture;
        return this;
    }
}
