using System;
using System.Collections.Generic;
using System.Linq;

namespace dvbsi
{
	public class LinkageDescriptor : Descriptor
	{
		public UInt16  transportStreamId { get; set; }
		public UInt16  originalNetworkId { get; set; }
		public UInt16  serviceId { get; set; }
		public UInt16 initialServiceId { get; set; }
		public UInt16 networkId { get; set; }
		public byte[] privateDataBytes { get; set; }

		public LinkageDescriptor(IReadOnlyList<byte> buffer, int index) : base(buffer, index)
		{
			var headerLength = 7;
			ASSERT_MIN_DLEN (headerLength);

			transportStreamId = UINT16 (buffer, index + 2);
			originalNetworkId = UINT16 (buffer, index + 4);
			serviceId = UINT16 (buffer,index + 6);
			var linkageType = buffer [index + 8];
			if (linkageType != 0x08) {
				privateDataBytes = new byte[DescriptorLength - headerLength];
				Array.Copy (buffer.ToArray (), index + 9, privateDataBytes, 0, (int)DescriptorLength - headerLength);
			} else {
				byte offset = 0;
				headerLength++;
				ASSERT_MIN_DLEN(headerLength);

				var handOverType = (buffer[index+9] >> 4) & 0x0f;
				var originType = buffer[index+9] & 0x01;

				if ((handOverType >= 0x01) && (handOverType <= 0x03)) {
					headerLength += 2;
					ASSERT_MIN_DLEN(headerLength);

					networkId = UINT16(buffer, index+10);
					offset += 2;
				}

				if (originType == 0x00) {
					headerLength += 2;
					ASSERT_MIN_DLEN(headerLength);

					initialServiceId = UINT16(buffer, index+offset + 10);
					offset += 2;
				}

				privateDataBytes = new byte[DescriptorLength - headerLength];
				Array.Copy(buffer.ToArray(), index+offset + 10, privateDataBytes, 0, DescriptorLength - headerLength);
			}
		}
	}
}

