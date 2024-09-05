using System;
using System.Collections.Generic;

namespace dvbsi
{
	public class DataBroadcastIdDescriptor : Descriptor
	{
		public string Iso639LanguageCode {
			get;
			private set;
		}

		public DataBroadcastIdDescriptor(IReadOnlyList<byte> buffer, int index) : base(buffer, index)
		{
			var data_broadcast_id = UINT16 (buffer, index+2);
			var component_t = buffer [index+4];
			var selector_leng  = buffer [index+5];
			Iso639LanguageCode = new DVBString(buffer, index, 100).Content;
			ASSERT_MIN_DLEN(3);
		}
	}
}

