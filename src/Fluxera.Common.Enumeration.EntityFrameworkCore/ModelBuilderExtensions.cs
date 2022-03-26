namespace Fluxera.Enumeration.EntityFrameworkCore
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using Fluxera.Guards;
	using JetBrains.Annotations;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata;
	using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

	[PublicAPI]
	public static class ModelBuilderExtensions
	{
		/// <summary>
		///     Applies the conversions for all <see cref="Enumeration{TEnum, TValue}" /> based properties
		///     for all entities present on the given <see cref="ModelBuilder" />.
		/// </summary>
		/// <param name="modelBuilder">The <see cref="ModelBuilder" /> for which to apply the value conversions.</param>
		/// <param name="useValue"></param>
		/// <returns>The <see cref="ModelBuilder" /> passed in.</returns>
		public static void UseEnumeration(this ModelBuilder modelBuilder, bool useValue = false)
		{
			Guard.Against.Null(modelBuilder, nameof(modelBuilder));

			foreach(IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
			{
				IEnumerable<PropertyInfo> properties = entityType
					.ClrType
					.GetProperties()
					.Where(type => type.PropertyType.IsEnumeration());

				foreach(PropertyInfo property in properties)
				{
					Type enumerationType = property.PropertyType;
					Type valueType = enumerationType.GetValueType();

					Type converterTypeTemplate = useValue
						? typeof(EnumerationValueConverter<,>)
						: typeof(EnumerationNameConverter<,>);

					Type converterType = converterTypeTemplate.MakeGenericType(enumerationType, valueType);

					ValueConverter? converter = (ValueConverter?)Activator.CreateInstance(converterType);

					modelBuilder
						.Entity(entityType.ClrType)
						.Property(property.Name)
						.HasConversion(converter);
				}
			}
		}
	}
}
