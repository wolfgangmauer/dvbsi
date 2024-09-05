using System;
using System.Collections.Generic;

namespace dvbsi
{
	class ModuleLinkDescriptor : Descriptor
	{
        public ModuleLinkDescriptor(byte[] buffer) : base(buffer)
        {
            ASSERT_MIN_DLEN(3);
            Position = buffer[2];
            ModuleId = UINT16(buffer, 3);
        }

        public byte Position { get; private set; }

        public ushort ModuleId { get; private set; }
    }
}

