using System;
using System.Collections.Generic;
using System.Linq;

namespace dvbsi
{
    class AacDescriptor : Descriptor
    {
        public AacDescriptor(IReadOnlyList<byte> buffer, int index)
            : base(buffer, index)
        {
            var headerLength = 2;
            ASSERT_MIN_DLEN(headerLength);

            ProfileLevel = buffer[index+2];
            AACTypeFlag = (buffer[index + 3] >> 7) & 0x01;

            var i = 4;
            if (AACTypeFlag == 0x01)
            {
                headerLength++;
                ASSERT_MIN_DLEN(headerLength);

                AACType = buffer[index + i++];
            }

            AdditionalInfo = new byte[index + DescriptorLength - headerLength];
            Array.Copy(buffer.ToArray(), index + i, AdditionalInfo, 0, DescriptorLength - headerLength);
        }

        protected override void Dispose(bool disposing)
        {
            // free managed resources
            if (disposing)
            {
                AdditionalInfo = null;
            }
            // free native resources if there are any.
            base.Dispose(disposing);
        }

        public byte ProfileLevel { get; private set; }

        public int AACTypeFlag { get; private set; }

        public byte AACType { get; private set; }

        public byte[] AdditionalInfo { get; private set; }
    }
}

