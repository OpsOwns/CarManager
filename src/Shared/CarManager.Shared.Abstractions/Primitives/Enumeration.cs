namespace CarManager.Shared.Abstractions.Primitives;

public abstract class Enumeration<TEnum> : IEquatable<Enumeration<TEnum>>, IComparable<Enumeration<TEnum>>
    where TEnum : Enumeration<TEnum>
{
    public int Key { get; private set; }
    public string Value { get; private set; }
    public static IReadOnlyCollection<TEnum> List => EnumerationsDictionary.Value.Values.ToList();

    private static readonly Lazy<Dictionary<int, TEnum>> EnumerationsDictionary =
        new(() => GetAllEnumerationOptions().ToDictionary(item => item.Key));


    protected Enumeration(int key, string value)
    {
        Key = key;
        Value = value;
    }

    protected Enumeration()
    {
        Key = default;
        Value = string.Empty;
    }

    public static Result<TEnum> GetValueByName(string value)
    {
        var hasValue = EnumerationsDictionary.Value.Any(x => x.Value.Value == value);

        if (!hasValue)
        {
            return Result.Failure<TEnum>(
                new Error("enumeration.error", $"Value {value} not exists in enumeration type"));
        }

        return Result.Success(EnumerationsDictionary.Value.Single(x => x.Value.Value == value).Value);
    }

    public static TEnum GetValueByKey(int key)
    {
        var enumerationValue = EnumerationsDictionary.Value.GetValueOrDefault(key);

        if (enumerationValue is null)
            throw new ArgumentNullException($"Value with key {key} not exists");

        return enumerationValue;
    }


    public static bool ContainsValue(int value) => EnumerationsDictionary.Value.ContainsKey(value);

    public static bool operator ==(Enumeration<TEnum>? a, Enumeration<TEnum>? b)
    {
        if (a is null && b is null)
        {
            return true;
        }

        if (a is null || b is null)
        {
            return false;
        }

        return a.Equals(b);
    }

    public static bool operator !=(Enumeration<TEnum> a, Enumeration<TEnum> b) => !(a == b);

    public bool Equals(Enumeration<TEnum>? other)
    {
        if (other is null)
        {
            return false;
        }

        return GetType() == other.GetType() && other.Key.Equals(Key);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (!(obj is Enumeration<TEnum> otherValue))
        {
            return false;
        }

        return GetType() == obj.GetType() && otherValue.Key.Equals(Key);
    }

    public int CompareTo(Enumeration<TEnum>? other) => other is null ? 1 : Key.CompareTo(other.Key);
    public override int GetHashCode() => Key.GetHashCode();

    private static IEnumerable<TEnum> GetAllEnumerationOptions()
    {
        Type enumType = typeof(TEnum);

        IEnumerable<Type> enumerationTypes = Assembly
            .GetAssembly(enumType)!
            .GetTypes()
            .Where(type => enumType.IsAssignableFrom(type));

        var enumerations = new List<TEnum>();

        foreach (Type enumerationType in enumerationTypes)
        {
            List<TEnum> enumerationTypeOptions = GetFieldsOfType<TEnum>(enumerationType);

            enumerations.AddRange(enumerationTypeOptions);
        }

        return enumerations;
    }

    private static List<TFieldType> GetFieldsOfType<TFieldType>(Type type) =>
        type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(fieldInfo => type.IsAssignableFrom(fieldInfo.FieldType))
            .Select(fieldInfo => (TFieldType)fieldInfo.GetValue(null!)!)
            .ToList();
}