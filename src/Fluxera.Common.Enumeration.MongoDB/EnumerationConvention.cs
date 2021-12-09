namespace Fluxera.Enumeration.MongoDB
{
	using System;
	using global::MongoDB.Bson.Serialization;
	using global::MongoDB.Bson.Serialization.Conventions;
	using JetBrains.Annotations;

	[PublicAPI]
	public class EnumerationConvention : ConventionBase, IMemberMapConvention
	{
		private readonly bool useValueConverter;

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
				Type serializerTypeTemplate = this.useValueConverter
					? typeof(EnumerationValueSerializer<>)
					: typeof(EnumerationNameSerializer<>);
				Type serializerType = serializerTypeTemplate.MakeGenericType(memberType);

				IBsonSerializer enumerationSerializer = (IBsonSerializer)Activator.CreateInstance(serializerType);
				memberMap.SetSerializer(enumerationSerializer);
			}
		}
	}
}
