using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dvbsi
{
    class ApplicationInformation : DescriptorContainer 
    {
		protected override void Dispose(bool disposing)
        {
			ApplicationIdentifier = null;
			base.Dispose(disposing);
        }

        public ApplicationInformation(byte[] buffer)
        {
            ApplicationIdentifier = new ApplicationIdentifier(buffer);
            ApplicationControlCode = buffer[6];
            var applicationDescriptorsLoopLength = Descriptor.DVB_LENGTH(buffer, 7);

			for (var i = 0; i < applicationDescriptorsLoopLength; i += buffer[i + 10] + 2) {
				var tmpBuffer = new byte[buffer.Length - (i+9)];
				Buffer.BlockCopy (buffer, i + 9, tmpBuffer, 0, buffer.Length - (i + 9));
				descriptor (tmpBuffer, DescriptorScope.SCOPE_MHP);
				tmpBuffer = null;
			}
        }

        public ApplicationIdentifier ApplicationIdentifier { get; private set; }

        public byte ApplicationControlCode { get; private set; }
    }
}
