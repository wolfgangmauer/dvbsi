using System.Collections.Generic;

namespace dvbsi
{
    public class AudioStreamDescriptor : Descriptor
	{
		public AudioStreamDescriptor(IReadOnlyList<byte> buffer, int index) : base(buffer, index)
		{
			ASSERT_MIN_DLEN(1);

			FreeFormatFlag = (byte)((buffer[index+2] >> 7) & 0x01);
			Id = (byte)((buffer[index + 2] >> 6) & 0x01);
			Layer = (byte)((buffer[index + 2] >> 4) & 0x03);
			VariableRateAudioIndicator = (byte)((buffer[index + 2] >> 3) & 0x01);
		    Valid = true;
		}
	
		public byte FreeFormatFlag { get; private set; }
		public byte Id { get; private set; }
		public byte Layer { get; private set; }
		public byte VariableRateAudioIndicator { get; private set; }
	}
}

