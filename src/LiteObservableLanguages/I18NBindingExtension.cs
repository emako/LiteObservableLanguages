using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace LiteObservableLanguages;

/// <summary>
/// MarkupExtension for localization binding, especially for use in Setter.Value and similar scenarios.
/// Returns a Binding that updates automatically when the language changes.
/// </summary>
[MarkupExtensionReturnType(typeof(BindingBase))]
public class I18NBindingExtension : MarkupExtension
{
    /// <summary>
    /// The resource key to be localized.
    /// </summary>
    public string Key { get; set; } = string.Empty;

    /// <summary>
    /// Default constructor.
    /// </summary>
    public I18NBindingExtension()
    {
    }

    /// <summary>
    /// Constructor with key parameter.
    /// </summary>
    /// <param name="key">The resource key.</param>
    public I18NBindingExtension(string key) => Key = key;

    /// <summary>
    /// Provides the value for the markup extension, returning a Binding that updates on language change.
    /// </summary>
    /// <param name="serviceProvider">The service provider.</param>
    /// <returns>A BindingBase instance for localization.</returns>
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        // Create a binding to LocaleNotifier.Tick, using KeyConverter to translate the key.
        var binding = new Binding(nameof(LocaleNotifier.Tick))
        {
            Source = LocaleNotifier.Instance,
            Converter = new KeyConverter(Key),
            Mode = BindingMode.OneWay,
        };
        return binding.ProvideValue(serviceProvider);
    }

    /// <summary>
    /// Converter that translates the key to a localized string using I18NExtension.
    /// </summary>
    private sealed class KeyConverter(string key) : IValueConverter
    {
        private readonly string _key = key;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                // Attempt to translate the key; fallback to key itself if not found.
                return I18NExtension.Translate(_key) ?? _key;
            }
            catch
            {
                return _key;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotSupportedException();
    }

    /// <summary>
    /// Singleton notifier for language changes. When the language changes, increments Tick and notifies bindings.
    /// </summary>
    private sealed class LocaleNotifier : INotifyPropertyChanged
    {
        /// <summary>
        /// Singleton instance.
        /// </summary>
        public static readonly LocaleNotifier Instance = new();

        private LocaleNotifier()
        {
            // Subscribe to I18NExtension's culture change event.
            I18NExtension.CultureChanged += OnCultureChanged;
        }

        /// <summary>
        /// Property that changes when the language changes, triggering binding updates.
        /// </summary>
        public int Tick { get; private set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        /// <summary>
        /// Handler for culture change events. Increments Tick and notifies bindings.
        /// </summary>
        private void OnCultureChanged(CultureInfo culture)
        {
            Tick++;
            OnPropertyChanged(nameof(Tick));
        }
    }
}
