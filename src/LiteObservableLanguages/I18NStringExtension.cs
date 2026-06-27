using System.Windows.Markup;

namespace LiteObservableLanguages;

/// <summary>
/// MarkupExtension that returns a localized string directly (not a Binding).
/// * Useful in code-behind or non-WPF contexts where a static string value is needed.
/// * Dynamic update is not supported because string is returned directly instead of Binding
/// </summary>
[MarkupExtensionReturnType(typeof(string))]
public class I18NStringExtension : MarkupExtension
{
    /// <summary>
    /// The resource key to be localized.
    /// </summary>
    public string Key { get; set; } = string.Empty;

    /// <summary>
    /// Default constructor.
    /// </summary>
    public I18NStringExtension()
    {
    }

    /// <summary>
    /// Constructor with key parameter.
    /// </summary>
    /// <param name="key">The resource key.</param>
    public I18NStringExtension(string key) => Key = key;

    /// <summary>
    /// Provides the value for the markup extension, returning the localized string for the given key.
    /// </summary>
    /// <param name="serviceProvider">The service provider.</param>
    /// <returns>The localized string, or the key itself if not found.</returns>
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return I18NExtension.Translate(Key)!;
    }
}
