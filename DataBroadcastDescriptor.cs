using System;
using System.Collections.Generic;

namespace dvbsi
{
	class DataBroadcastDescriptor : Descriptor
	{
		public List<byte> LanguageCode { get; private set; }
		public DataBroadcastDescriptor(IReadOnlyList<byte> buffer, int index) : base(buffer, index)
		{
			LanguageCode = new List<byte> ();
			DataBroadcastId = UINT16(buffer, index+2);
			var componentTag = buffer [index + 4];
			var selectorLength = buffer [index + 5];
			for (var i = 0; i < selectorLength; i ++) 
			{
				LanguageCode.Add(buffer[index+i+6]);
			}
			IsoLanguageCode = new DVBString (buffer, index+selectorLength+6, 3).Content;
			var textLength = buffer [index + selectorLength + 9];
			Text = new DVBString (buffer, index + selectorLength + 10, textLength).Content;
			Valid = true;
		}

		protected override void Dispose (bool disposing)
		{
			// free managed resources
			if (disposing) {
				LanguageCode = null;
			}
			base.Dispose (disposing);
		}

		public ushort DataBroadcastId { get; set;}
		public string Text { get; set;}
		public string IsoLanguageCode { get; set;}
	}
}

