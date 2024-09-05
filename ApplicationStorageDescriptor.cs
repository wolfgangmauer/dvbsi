using System;
using System.Collections.Generic;

namespace dvbsi
{
    class ApplicationStorageDescriptor : Descriptor
	{
        public ApplicationStorageDescriptor(byte[] buffer) : base(buffer)
        {
            ASSERT_MIN_DLEN(7);
            StorageProperty = buffer[2];
            NotLaunchableFromBroadcast = (byte)((buffer[3] >> 7) & 0x01);
            Version = UINT32(buffer, 4);
            Priority = buffer[8];
        }
        public UInt32 Version { get; private set; }

        public byte StorageProperty { get; private set; }

        public byte NotLaunchableFromBroadcast { get; private set; }

        public byte Priority { get; private set; }
    }
}

