using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public abstract class Enumeration<TEnum> : IEquatable<Enumeration<TEnum>> where TEnum : Enumeration<TEnum>
    {
        private static readonly Dictionary<int, TEnum> Enumerations = CreateEumeration();

        private static Dictionary<int, TEnum> CreateEumeration()
        {
            var enumerationType = typeof(TEnum);

            var fieldFor = enumerationType
                .GetFields(BindingFlags.Public |
                           BindingFlags.Static |
                           BindingFlags.FlattenHierarchy)
                .Where(fieldinfo =>
                       enumerationType.IsAssignableFrom(fieldinfo.FieldType))
                .Select(fieldinfo =>
                        (TEnum)fieldinfo.GetValue(default)!);

            return fieldFor.ToDictionary(x => x.Id);
        }

        public int Id { get; protected init; }
        public string Name { get; protected init; } = string.Empty;

        public Enumeration(int value, string name)
        {
            Id = value;
            Name = name;
        }
        public static TEnum? FromValue(int value)
        {
            return Enumerations.TryGetValue(value, out TEnum? enumeration) ? enumeration : default;
        }

        public static IReadOnlyCollection<TEnum>? GetValues() => Enumerations.Values.ToList();
        public static TEnum? FromName(string name)
        {
            return Enumerations.Values.SingleOrDefault(x => x.Name == name);
        }
        public bool Equals(Enumeration<TEnum>? other)
        {
            if (other is null)
            {
                return false;
            }

            return GetType() == (other.GetType()) &&
                Id == other.Id;
        }
        public override bool Equals(object? obj)
        {
            return obj is Enumeration<TEnum> other && Equals(other);

        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
