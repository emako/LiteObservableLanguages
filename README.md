[![NuGet](https://img.shields.io/nuget/v/LiteObservableLanguages.svg)](https://nuget.org/packages/LiteObservableLanguages) [![Actions](https://github.com/emako/LiteObservableLanguages/actions/workflows/library.nuget.yml/badge.svg)](https://github.com/emako/LiteObservableLanguages/actions/workflows/library.nuget.yml)

# LiteObservableLanguages

Lightweight I18N and locale support for WPF: culture-aware markup extensions (`I18NExtension`, `I18NBindingExtension`), a `Locale` manager with resource fallback, and source-generated resource keys.

## Features

- **Locale**: Central manager for current UI culture, `ResourceManager`, and fallback culture; raises `CultureChanged` when culture changes; supports fluent configuration (`UseResourceManager`, `UseFallback`, `UseCulture`).
- **I18NExtension** (`{I18N ...}`): Markup extension that resolves resource keys to localized strings in XAML; supports static keys (`{x:Static m:LangKeys.Key}`) and dynamic keys (`{Binding SelectedKey}`); supports composite strings via `LanguageBinding` and nested bindings.
- **I18NBindingExtension** (`{I18NBinding ...}`): Returns a binding that updates automatically when the language changes; ideal for `Setter.Value`, `Trigger` setters, and other binding-only scenarios.
- **LocaleExtension**: Extension method `"Key".Tr()` and `"Key".Tr(args)` for code-behind or view models.
- **Source-generated resource keys**: Use `[ResourceKeysOf(typeof(Resources))]` on a static partial class to generate type-safe key constants (e.g. `LangKeys.SomeKey`) from your `.resx` keys.
- **ResourceProvider**: Abstract provider for custom resource sources; register with `ResourceProvider.Providers` for integration with `I18NExtension`.

## Installation

```bash
dotnet add package LiteObservableLanguages
```

## Demos (WpfApp)

The **WpfApp** project demonstrates the following.

### 1. Language switcher and Locale

ViewModel exposes `Culture` and `AvailableCultures`; setting `Culture` updates `I18NExtension.Culture` so all I18N bindings refresh.

```c#
public CultureInfo Culture
{
    get;
    set
    {
        if (field.EnglishName.Equals(value.EnglishName)) return;
        field = value;
        I18NExtension.Culture = value;
        OnPropertyChanged();
    }
} = new("zh");
```

### 2. I18N with static key

```xml
<TextBlock Text="{I18N {x:Static m:LangKeys.Select_your_text_to_translate}}" />
```

### 3. I18N with dynamic key (binding)

Key can come from a binding (e.g. selected item or user input):

```xml
<TextBox Text="{I18N {Binding SelectedKey}}" />
<TextBlock Text="{I18N {Binding InputText}}" />
```

### 4. Composite string with LanguageBinding

Use `I18N` with a template that mixes literal placeholders and `LanguageBinding`/`Binding` for dynamic parts:

```xml
<TextBlock.Text>
    <I18N Key="{x:Static m:LangKeys.Current_language_is}">
        <LanguageBinding Key="{x:Static m:LangKeys.Language}" />
        <Binding Path="Culture.EnglishName" />
    </I18N>
</TextBlock.Text>
```

### 5. I18NBinding in styles and triggers

Use `I18NBinding` where only a binding is allowed (e.g. `Setter.Value`):

```xml
<Setter Property="Content" Value="{I18NBinding {x:Static m:LangKeys.Off}}" />
<Trigger Property="IsChecked" Value="True">
    <Setter Property="Content" Value="{I18NBinding {x:Static m:LangKeys.On}}" />
</Trigger>
```

### 6. Source-generated keys

Define a static partial class and point it at your default resource class; the source generator emits key constants:

```c#
[ResourceKeysOf(typeof(Resources))]
public static partial class LangKeys;
```

Then use `LangKeys.SomeKey` in XAML or code.

Run **WpfApp** and switch languages from the list; all text bound with `I18N` / `I18NBinding` updates without restart.

## Attribution

This project is a **fork** of [Antelcat/I18N](https://github.com/Antelcat/I18N) — reactive language support for WPF/Avalonia applications when using .resx files.

## License

[MIT](LICENSE)
