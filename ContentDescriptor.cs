using System;
using System.Collections.Generic;

namespace dvbsi
{
	public class ContentClassification
	{
		public byte ContentNibbleLevel1 {
			get;
			private set;
		}

		public byte ContentNibbleLevel2 {
			get;
			private set;
		}

		public byte UserNibble1 {
			get;
			private set;
		}

		public byte UserNibble2 {
			get;
			private set;
		}

		public ContentClassification(IReadOnlyList<byte> buffer, int index)
		{
			ContentNibbleLevel1 = (byte)((buffer[index+0] >> 4) & 0x0f);
			ContentNibbleLevel2 = (byte)(buffer[index+0] & 0x0f);
			UserNibble1 = (byte)((buffer[index+1] >> 4) & 0x0f);
			UserNibble2 = (byte)(buffer[index+1] & 0x0f);		
		}
	}

	public class ContentDescriptor : Descriptor
	{
		public List<ContentClassification> Classifications 
		{
			get;
			private set;
		}

		public ContentDescriptor(IReadOnlyList<byte> buffer, int index) : base(buffer, index)
		{
			Classifications = new List<ContentClassification> ();
			for (var i = 0; i < DescriptorLength; i += 2) 
			{
				ASSERT_MIN_DLEN(i + 2);
				Classifications.Add(new ContentClassification(buffer, index+i+2));
			}
			Valid = true;
		}

		#region IDisposable implementation

		protected override void Dispose (bool disposing)
		{
			// free managed resources
			if (disposing) {
				Classifications = null;
			}
			// free native resources if there are any.
			base.Dispose(disposing);
		}
		#endregion
	}
}

