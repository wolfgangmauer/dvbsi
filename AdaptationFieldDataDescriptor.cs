using System;
using System.Collections.Generic;

namespace dvbsi
{
	class AdaptationFieldDataDescriptor : Descriptor
	{
        public AdaptationFieldDataDescriptor(byte[] buffer)
            : base(buffer)
        {
            ASSERT_MIN_DLEN(1);
            AdaptationFieldDataIdentifier = buffer[2];
        }

        public byte AdaptationFieldDataIdentifier { get; private set; }
    }
}

