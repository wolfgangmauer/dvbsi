using System;
using System.Collections.Generic;
using System.Text;

namespace dvbsi
{
    class ApplicationIconsDescriptor : Descriptor
    {
        public ApplicationIconsDescriptor(byte[] buffer)
            : base(buffer)
        {
            ASSERT_MIN_DLEN(3);
            var iconLocatorLength = buffer[2];
            ASSERT_MIN_DLEN(iconLocatorLength + 3);
			IconLocator = new DVBString(buffer, 3, iconLocatorLength).Content;
            IconFlags = UINT16(buffer, iconLocatorLength + 3);
        }

        public ushort IconFlags { get; private set; }

        public string IconLocator { get; private set; }
    }
}

