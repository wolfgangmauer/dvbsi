using System;
using System.Collections.Generic;

namespace dvbsi
{
	class AncillaryDataDescriptor : Descriptor
	{
		public AncillaryDataDescriptor(IReadOnlyList<byte> buffer, int index)
            : base(buffer, index)
        {
            ASSERT_MIN_DLEN(1);
			AncillaryDataIdentifier = buffer[index+2];
        }

        public byte AncillaryDataIdentifier { get; private set; }
    }
}

