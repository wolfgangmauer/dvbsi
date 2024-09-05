using System.Collections.Generic;

namespace dvbsi
{
	public class ComponentDescriptor : Descriptor
	{
		public int StreamContent {
			get;
			private set;
		}

		public byte ComponentType {
			get;
			private set;
		}

		public byte ComponentTag {
			get;
			private set;
		}

		public string LanguageCode {
			get;
			private set;
		}

		public string Text {
			get;
			private set;
		}

		public ComponentDescriptor(IReadOnlyList<byte> buffer, int index) : base(buffer, index)
		{
			ASSERT_MIN_DLEN(6);

			StreamContent = buffer[index+2] & 0x0f;
			ComponentType = buffer[index+3];
			ComponentTag = buffer[index+4];
			LanguageCode = new DVBString(buffer, index+5, 3).Content;
			Text = new DVBString(buffer, index+8, DescriptorLength - 6).Content;
			Valid = true;
		}
	}
}

