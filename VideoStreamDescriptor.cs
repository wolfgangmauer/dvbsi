using System.Collections.Generic;

namespace dvbsi
{
	public class VideoStreamDescriptor : Descriptor
	{
		public VideoStreamDescriptor(IReadOnlyList<byte> buffer, int index) : base(buffer, index)
		{
			ASSERT_MIN_DLEN (1);

			MultipleFrameRateFlag = (byte)((buffer [index+2] >> 7) & 0x01);
			FrameRateCode = (byte)((buffer [index + 2] >> 3) & 0x0F);
			Mpeg1OnlyFlag = (byte)((buffer [index + 2] >> 2) & 0x01);
			ConstrainedParameterFlag = (byte)((buffer [index + 2] >> 1) & 0x01);

			if (Mpeg1OnlyFlag == 0)
			{
				ASSERT_MIN_DLEN (3);

				ProfileAndLevelIndication = buffer [index + 3];
				ChromaFormat = (byte)((buffer [index + 4] >> 6) & 0x03);
				FrameRateExtensionFlag = (byte)((buffer [index + 4] >> 5) & 0x01);
			}
		    Valid = true;
		}

		public byte MultipleFrameRateFlag { get; private set; }
		public byte FrameRateCode { get; private set; }
		public byte Mpeg1OnlyFlag { get; private set; }
		public byte ConstrainedParameterFlag { get; private set; }
		public byte StillPictureFlag { get; set; }
		public byte ProfileAndLevelIndication { get; private set; }
		public byte ChromaFormat { get; private set; }
		public byte FrameRateExtensionFlag { get; private set; }
	}
}

