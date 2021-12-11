namespace Fluxera.Enumeration.UnitTests.Enums
{
	public class MessageType : Enumeration<MessageType, int>
	{
		public static readonly MessageType Email = new EmailType();
		public static readonly MessageType Postal = new PostalType();
		public static readonly MessageType TextMessage = new TextMessageType();

		/// <inheritdoc />
		private MessageType(int value, string name)
			: base(value, name)
		{
		}

		private sealed class EmailType : MessageType
		{
			/// <inheritdoc />
			public EmailType() : base(0, "Email")
			{
			}
		}

		private sealed class PostalType : MessageType
		{
			/// <inheritdoc />
			public PostalType() : base(0, "Postal")
			{
			}
		}

		private sealed class TextMessageType : MessageType
		{
			/// <inheritdoc />
			public TextMessageType() : base(0, "TextMessage")
			{
			}
		}
	}
}
