using System;
using System.Collections.Generic;

namespace dvbsi
{
	class LocationDescriptor : Descriptor
	{
        public LocationDescriptor(byte[] buffer) : base(buffer)
        {
            ASSERT_MIN_DLEN(1);
            LocationTag = buffer[2];
        }

        public byte LocationTag { get; private set; }
    }
}

