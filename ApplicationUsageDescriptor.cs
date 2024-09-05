using System;
using System.Collections.Generic;

namespace dvbsi
{
	class ApplicationUsageDescriptor : Descriptor
	{
        public ApplicationUsageDescriptor(byte[] buffer) : base(buffer)
        {
            ASSERT_MIN_DLEN(1);
            UsageType = buffer[2];
        }

        public byte UsageType { get; private set; }
    }
}

