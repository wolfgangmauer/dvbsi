using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace dvbsi
{
	public class ExtendedEvent
	{
		public byte ItemDescriptionLength { get; private set; }

		public string ItemDescription
		{
			get;
			private set;
		}

		public string Item
		{
			get;
			private set;
		}

		public byte ItemLength
		{
			get; private set;
		}

		public ExtendedEvent(IReadOnlyList<byte> buffer, int index)
		{
			ItemDescriptionLength = buffer[index + 0];
			ItemDescription = new DVBString(buffer, index + 1, ItemDescriptionLength).Content;
			ItemLength = buffer[index + ItemDescriptionLength + 1];
			Item = new DVBString(buffer, index + ItemDescriptionLength + 2, ItemLength).Content;
		}
	}

	public class ExtendedEventDescriptor : Descriptor
	{
		public string LanguageCode {
			get;
			private set;
		}

		public int DescriptorNumber {
			get;
			private set;
		}

		public int LastDescriptorNumber {
			get;
			private set;
		}

		public string Text {
			get;
			private set;
		}

		public List<ExtendedEvent> Items {
			get;
			private set;
		}

		public ExtendedEventDescriptor(IReadOnlyList<byte> buffer, int index) : base(buffer, index)
		{
			Items = new List<ExtendedEvent> ();
			var headerLength = 6;
			ASSERT_MIN_DLEN(headerLength);

			DescriptorNumber = (buffer[index+2] >> 4) & 0x0f;
			LastDescriptorNumber = buffer[index + 2] & 0x0f;
			LanguageCode = new DVBString(buffer, index + 3, 3).Content;
			var lengthOfItems = buffer[index + 6];

			headerLength += lengthOfItems;
			ASSERT_MIN_DLEN(headerLength);

			var i = 0;
			while (i < lengthOfItems) 
			{
				var e = new ExtendedEvent (buffer, index +i + 7);
				Items.Add(e);
				i += e.ItemDescriptionLength + e.ItemLength + 2;
			}

			var textLength = buffer[index+lengthOfItems + 7];

			headerLength += textLength;
			ASSERT_MIN_DLEN(headerLength);
			Text = new DVBString (buffer, index+lengthOfItems + 8, textLength).Content;
		    Valid = true;
		}

        protected override void Dispose(bool disposing)
        {
            // free managed resources
            if (disposing)
            {
                Items = null;
            }
            // free native resources if there are any.
            base.Dispose(disposing);
        }
    }
}

