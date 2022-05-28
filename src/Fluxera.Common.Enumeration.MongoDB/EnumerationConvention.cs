namespace Fluxera.Enumeration.MongoDB
{
	using System;
	using global::MongoDB.Bson.Serialization;
	using global::MongoDB.Bson.Serialization.Conventions;
	using JetBrains.Annotations;

	/// <summary>
	///     A member configuration to serialize enumeration properties.
	/// </summary>
	[PublicAPI]
	public sealed class EnumerationConvention : ConventionBase, IMemberMapConvention
	{
		private readonly bool useValueConverter;

		/// <summary>
		///     Initializes a new instance of the <see cref="EnumerationConvention" /> type.
		/// </summary>
		/// <param name="useValueConverter"></param>
		public EnumerationConvention(bool useValueConverter = false)
		{
			this.useValueConverter = useValueConverter;
		}

		/// <inheritdoc />
		public void Apply(BsonMemberMap memberMap)
		{
			Type originalMemberType = memberMap.MemberType;
			Type memberType = Nullable.GetUnderlyingType(originalMemberType) ?? originalMemberType;

			if(memberType.IsEnumeration())
			{
				Type valueType = memberType.GetValueType();
				Type serializerTypeTemplate = this.useValueConverter
					? typeof(EnumerationValueSerializer<,>)
					: typeof(EnumerationNameSerializer<,>);
				Type serializerType = serializerTypeTemplate.MakeGenericType(memberType, valueType);

				IBsonSerializer enumerationSerializer = (IBsonSerializer)Activator.CreateInstance(serializerType);
				memberMap.SetSerializer(enumerationSerializer);
			}
		}
	}
}
