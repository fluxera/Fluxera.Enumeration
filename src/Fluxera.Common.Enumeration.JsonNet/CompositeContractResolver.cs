namespace Fluxera.Enumeration.JsonNet
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using Guards;
	using JetBrains.Annotations;
	using Newtonsoft.Json.Serialization;

	[PublicAPI]
	public class CompositeContractResolver : IContractResolver, IEnumerable<IContractResolver>
	{
		private readonly IList<IContractResolver> contractResolvers = new List<IContractResolver>();
		private readonly DefaultContractResolver defaultContractResolver = new DefaultContractResolver();

		public JsonContract ResolveContract(Type type)
		{
			return this.contractResolvers
				.Select(x => x.ResolveContract(type))
				.FirstOrDefault(x => x != null)!;
		}

		public IEnumerator<IContractResolver> GetEnumerator()
		{
			return this.contractResolvers.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		public void Add(IContractResolver contractResolver)
		{
			Guard.Against.Null(contractResolver, nameof(contractResolver));

			if(this.contractResolvers.Contains(this.defaultContractResolver))
			{
				this.contractResolvers.Remove(this.defaultContractResolver);
			}

			this.contractResolvers.Add(contractResolver);
			this.contractResolvers.Add(this.defaultContractResolver);
		}
	}
}
