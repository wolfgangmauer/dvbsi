using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dvbsi
{
    class ApplicationProfile
    {
        public ApplicationProfile(byte[] buffer)
        {
            Profile = Descriptor.UINT16(buffer, 0);
            VersionMajor = buffer[2];
            VersionMinor = buffer[3];
            VersionMicro = buffer[4];
        }

        public UInt16 Profile { get; private set; }

        public byte VersionMajor { get; private set; }

        public byte VersionMinor { get; private set; }

        public byte VersionMicro { get; private set; }
    }
}
