using System.Collections.Generic;

namespace dvbsi
{
	public class LongSection : ShortSection
	{
		public ushort TableIdExtension { get; private set; }
		public byte VersionNumber { get; private set; }
		public byte CurrentNextIndicator { get; private set; }
		public byte SectionNumber { get; private set; }
		public byte LastSectionNumber { get; set; }

		public bool Equals(LongSection a)
		{
			return (SectionNumber == a.SectionNumber);
		}

		public LongSection(IReadOnlyList<byte> buffer) : base(buffer)
		{
			TableIdExtension = Descriptor.UINT16(buffer, 3);
			VersionNumber = (byte)((buffer[5] >> 1) & 0x1f);
			CurrentNextIndicator = (byte)(buffer[5] & 0x01);
			SectionNumber = buffer[6];
			LastSectionNumber = buffer[7];
		}	
	}
}

