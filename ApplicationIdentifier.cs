using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvbsi
{
    class ApplicationIdentifier
    {
        public ApplicationIdentifier(byte[] buffer)
        {
            OrganisationId = Descriptor.UINT32(buffer, 0);
            ApplicationId = Descriptor.UINT16(buffer, 4);
        }

        public uint ApplicationId { get; private set; }
        public uint OrganisationId { get; private set; }
    }
}
