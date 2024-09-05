using System;
using System.Collections.Generic;

namespace dvbsi
{
    class ApplicationDescriptor : Descriptor
    {
        private byte applicationProfilesLength;

		protected override void Dispose(bool disposing)
        {
            ApplicationProfiles.Clear();
            TransportProtocolLabels.Clear();
			base.Dispose(disposing);
        }

        public ApplicationDescriptor(byte[] buffer)
            : base(buffer)
        {
            var headerLength = 3;
            ASSERT_MIN_DLEN(headerLength);

            applicationProfilesLength = buffer[2];

            headerLength += applicationProfilesLength;
            ASSERT_MIN_DLEN(headerLength);

            ApplicationProfiles = new List<ApplicationProfile>();
            TransportProtocolLabels = new List<byte>();
            for (var i = 0; i < applicationProfilesLength; i += 5)
            {
                var tmpBuffer = new byte[buffer.Length - (i + 3)];
                Array.Copy(buffer, i + 3, tmpBuffer, 0, tmpBuffer.Length);
                ApplicationProfiles.Add(new ApplicationProfile(tmpBuffer));
            }
            ServiceBoundFlag = (byte)((buffer[applicationProfilesLength + 3] >> 7) & 0x01);
            Visibility = (byte)((buffer[applicationProfilesLength + 3] >> 5) & 0x02);
            ApplicationPriority = buffer[applicationProfilesLength + 4];

            for (var i = 0; i < DescriptorLength - applicationProfilesLength - 3; i += 1)
                TransportProtocolLabels.Add(buffer[i + applicationProfilesLength + 5]);
        }

        public List<byte> TransportProtocolLabels { get; private set; }

        public List<ApplicationProfile> ApplicationProfiles { get; private set; }

        public byte ApplicationPriority { get; private set; }

        public byte Visibility { get; private set; }

        public byte ServiceBoundFlag { get; private set; }
    }
}

