using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvbsi
{
    class ApplicationInformationSection : DescriptorContainer
    {
        public ApplicationInformationSection(byte[] buffer)
		{
		    var longCrcSection = new LongCrcSection(buffer);

		    var commonDescriptorsLength = longCrcSection.SectionLength > 10 ? Descriptor.DVB_LENGTH(buffer, 8) : 0;

			var pos = 10;
		    var bytesLeft = longCrcSection.SectionLength > 11 ? longCrcSection.SectionLength - 11 : 0;
			var loopLength = 0;
			var bytesLeft2 = commonDescriptorsLength;

			while (bytesLeft >= bytesLeft2 && bytesLeft2 > 1 && bytesLeft2 >= (loopLength = 2 + buffer[pos+1])) {
				var tmpBuffer = new byte[buffer.Length - pos];
				Buffer.BlockCopy (buffer, pos, tmpBuffer, 0, buffer.Length - pos);
				descriptor (tmpBuffer, DescriptorScope.SCOPE_MHP);
				tmpBuffer = null;
				pos += loopLength;
				bytesLeft -= loopLength;
				bytesLeft2 -= loopLength;
			}

			if (bytesLeft2 != 0 && bytesLeft > 1) {
				bytesLeft2 = Descriptor.DVB_LENGTH (buffer, pos);
				pos += 2;
				bytesLeft -= 2;
				while (bytesLeft >= bytesLeft2 && bytesLeft2 > 8 && bytesLeft2 >= (loopLength = 9 + Descriptor.DVB_LENGTH(buffer, pos + 7))) {
					var tmpBuffer = new byte[buffer.Length - pos];
					Array.Copy (buffer, pos, tmpBuffer, 0, tmpBuffer.Length);
					ApplicationInformation.Add (new ApplicationInformation (tmpBuffer));
					pos += loopLength;
					bytesLeft -= loopLength;
					bytesLeft2 -= loopLength;
				}
			}
		}
        public List<ApplicationInformation> ApplicationInformation { get; private set; }
	}
}
