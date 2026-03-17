using System.Globalization;
using System.Resources;

namespace LiteObservableLanguages;

/// <summary>
/// Locale manager for handling culture changes and resource fallback.
/// </summary>
public partial class Locale
{
    public static Locale Default { get; set; } = new();

    /// <summary>
    /// Event triggered when the culture changes.
    /// </summary>
    public event Action<object?, CultureInfo>? CultureChanged;

    /// <summary>
    /// The ResourceManager used to retrieve localized resources.
    /// </summary>
    public virtual ResourceManager? ResourceManager { get; set; }

    /// <summary>
    /// The fallback culture to use if a resource is not found. Defaults to "en-US".
    /// </summary>
    public virtual CultureInfo Fallback { get; set; } = new CultureInfo("en-US");

    /// <summary>
    /// Gets or sets the current UI culture. Setting will update the culture and trigger resource lookup.
    /// </summary>
    public virtual CultureInfo Culture
    {
        get => field ?? CultureInfo.CurrentUICulture;
        set => SetCulture(field = value, ResourceManager);
    } = null;

    /// <summary>
    /// Sets the current culture, falling back to parent cultures or the fallback if resources are missing.
    /// </summary>
    /// <param name="value">The new culture to set.</param>
    /// <param name="resourceManager">The ResourceManager to use for resource lookup.</param>
    protected virtual void SetCulture(CultureInfo? value, ResourceManager? resourceManager)
    {
        _ = resourceManager ?? throw new ArgumentNullException(nameof(resourceManager));

        CultureInfo culture = value ?? Fallback;

        // Traverse up the culture hierarchy until a resource set is found or fallback is reached.
        while (resourceManager.GetResourceSet(culture, true, false) is null)
        {
            if (culture.Parent == CultureInfo.InvariantCulture)
            {
                culture = Fallback;
                break;
            }
            culture = culture.Parent;
        }

        // Notify I18NExtension of the culture change.
        I18NExtension.Culture = culture;

        // Raise the CultureChanged event.
        CultureChanged?.Invoke(CultureChanged.Target, culture);
    }
}
