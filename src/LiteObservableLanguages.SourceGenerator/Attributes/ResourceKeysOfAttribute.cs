namespace LiteObservableLanguages.Attributes;

#pragma warning disable CS9113 // Parameter is unread.

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class ResourceKeysOfAttribute(Type resourceType) : Attribute;

#pragma warning restore CS9113 // Parameter is unread.
