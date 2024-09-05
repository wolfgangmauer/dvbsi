using System;
using System.Collections.Generic;

namespace dvbsi
{
    public class Ac3Descriptor : Descriptor
	{
        public Ac3Descriptor(byte[] buffer)
            : base(buffer)
        {
            // EN300468 says that descriptor_length must be >= 1,
            // but it's easy to set sane defaults in this case
            // and some broadcasters already got it wrong.
            if (DescriptorLength == 0)
            {
                Ac3TypeFlag = 0;
                BsidFlag = 0;
                MainidFlag = 0;
                AsvcFlag = 0;
                return;
            }

            Ac3TypeFlag = (buffer[2] >> 7) & 0x01;
            BsidFlag = (buffer[2] >> 6) & 0x01;
            MainidFlag = (buffer[2] >> 5) & 0x01;
            AsvcFlag = (buffer[2] >> 4) & 0x01;

            var headerLength = 1 + Ac3TypeFlag + BsidFlag + MainidFlag + AsvcFlag;
            ASSERT_MIN_DLEN(headerLength);

            var i = 3;
            if (Ac3TypeFlag == 1)
                Ac3Type = buffer[i++];

            if (BsidFlag == 1)
                Bsid = buffer[i++];

            if (MainidFlag == 1)
                Mainid = buffer[i++];

            if (AsvcFlag == 1)
                Avsc = buffer[i++];

            AdditionalInfo = new byte[DescriptorLength - headerLength];
            Array.Copy(buffer, i, AdditionalInfo, 0, DescriptorLength - headerLength);
        }

        public int Ac3TypeFlag { get; private set; }

        public int BsidFlag { get; private set; }

        public int MainidFlag { get; private set; }

        public int AsvcFlag { get; private set; }

        public byte Ac3Type { get; private set; }

        public byte Bsid { get; private set; }

        public byte Mainid { get; private set; }

        public byte Avsc { get; private set; }

        public byte[] AdditionalInfo { get; private set; }
    }
	
}

