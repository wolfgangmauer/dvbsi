using System.Collections.Generic;
using System;

namespace dvbsi
{
	public class LongCrcSection : LongSection
	{
		public UInt32 Crc32 { get; private set; }
		public LongCrcSection (IReadOnlyList<byte> buffer) : base(buffer)
		{
			Crc32 = Descriptor.UINT32(buffer, SectionLength - 1);
		}
	}
}

