using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Markup;

namespace LiteObservableLanguages;

/// <summary>
/// It's not a binding, Just a key to get the value from the dictionary.
/// </summary>
[MarkupExtensionReturnType(typeof(Binding))]
public sealed class LanguageBinding : Binding
{
    public LanguageBinding()
    {
    }

    public LanguageBinding(string key)
    {
        Key = key;
    }

    [DefaultValue("")]
    public string? Key
    {
        get => field;
        set
        {
            if (string.IsNullOrEmpty(value)) return;
            field = value;
        }
    }
}
