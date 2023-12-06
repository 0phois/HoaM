using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Collections.Concurrent;
using Typely.Core;
using Typely.Core.Extensions;
using Typely.EfCore;

namespace HoaM.Infrastructure.Data
{
    internal class TypelyValueConverterSelector(ValueConverterSelectorDependencies dependencies) : ValueConverterSelector(dependencies)
    {
        private readonly ConcurrentDictionary<(Type ValueType, Type? UnderlyingType), ValueConverter> _converters = new();

        public override IEnumerable<ValueConverterInfo> Select(Type modelClrType, Type? providerClrType = null)
        {
            foreach (var converter in base.Select(modelClrType, providerClrType))
                yield return converter;

            if (modelClrType.ImplementsITypelyValue() && modelClrType.GetTypeOrUnderlyingType() is { } typeOrUnderlyingType)
            {
                var type = typeOrUnderlyingType.GetInterfaces()
                                               .First(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(ITypelyValue<,>))
                                               .GetGenericArguments()[0];

                providerClrType ??= (type == typeof(Guid)) ? typeof(string) : type;

                var converterType = typeof(TypelyValueConverter<,>).MakeGenericType(type, typeOrUnderlyingType);

                var instance = _converters.GetOrAdd((modelClrType, providerClrType), _ => (ValueConverter)Activator.CreateInstance(converterType)!);

                yield return new ValueConverterInfo(modelClrType, providerClrType, _ => instance);
            }
        }
    }
}
