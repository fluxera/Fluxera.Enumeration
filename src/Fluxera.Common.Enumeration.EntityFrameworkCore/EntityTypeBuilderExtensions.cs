namespace Fluxera.Enumeration.EntityFrameworkCore
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using JetBrains.Annotations;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.ChangeTracking;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;
	using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

	/// <summary>
	///     Extension methods for the <see cref="ModelBuilder" /> type.
	/// </summary>
	[PublicAPI]
	public static class EntityTypeBuilderExtensions
	{
		/// <summary>
		///     Applies the conversions for all <see cref="Enumeration{TEnum, TValue}" /> based properties
		///     for all entities present on the given <see cref="ModelBuilder" />.
		/// </summary>
		/// <param name="entityTypeBuilder">The <see cref="EntityTypeBuilder" /> for which to apply the value conversions.</param>
		/// <param name="useValue"></param>
		/// <returns>The <see cref="ModelBuilder" /> passed in.</returns>
		public static void UseEnumeration(this EntityTypeBuilder entityTypeBuilder, bool useValue = false)
		{
			Guard.ThrowIfNull(entityTypeBuilder);

			IEnumerable<PropertyInfo> properties = entityTypeBuilder.Metadata
				.ClrType
				.GetProperties()
				.Where(type => type.PropertyType.IsEnumeration());

			foreach(PropertyInfo property in properties)
			{
				Type enumerationType = property.PropertyType;

				if(enumerationType.IsEnumeration())
				{
					Type valueType = enumerationType.GetEnumerationValueType();

					Type converterTypeTemplate = useValue
						? typeof(EnumerationValueConverter<,>)
						: typeof(EnumerationNameConverter<,>);

					Type converterType = converterTypeTemplate.MakeGenericType(enumerationType, valueType);

					ValueConverter converter = (ValueConverter)Activator.CreateInstance(converterType);

					entityTypeBuilder
						.Property(property.Name)
						.HasConversion(converter);
				}
			}
		}
	}
}
