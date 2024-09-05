using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace dvbsi
{
    public class BouquetAssociation : DescriptorContainer
    {
        public BouquetAssociation(byte[] buffer)
        {
            TransportStreamId = Descriptor.UINT16(buffer, 0);
            OriginalNetworkId = Descriptor.UINT16(buffer, 2);
            var transportStreamLoopLength = Descriptor.DVB_LENGTH(buffer, 4);

            for (var i = 6; i < transportStreamLoopLength + 6; i += buffer[i + 1] + 2)
            {
				var tmpBuffer = new byte[buffer.Length - i];
				Buffer.BlockCopy (buffer, i, tmpBuffer, 0, buffer.Length - i);
				descriptor(tmpBuffer, DescriptorScope.SCOPE_SI);
				tmpBuffer = null;
            }
        }

        public ushort TransportStreamId { get; private set; }

        public ushort OriginalNetworkId { get; private set; }
    }

    public class BouquetAssociationSection : DescriptorContainer
    {
        public static PacketId PID = PacketId.PID_BAT;
        public static TableId TID = TableId.TID_BAT;
        public static UInt32 TIMEOUT = 12000;

		protected override void Dispose(bool disposing)
        {
            Bouquet.Clear();
            Bouquet = null;
			base.Dispose(disposing);
        }

        public BouquetAssociationSection(byte[] buffer)
        {
            var longCrcSection = new LongCrcSection(buffer);
            var bouquetDescriptorsLength = longCrcSection.SectionLength > 9 ? Descriptor.DVB_LENGTH(buffer, 8) : 0;

            var pos = 10;
            var bytesLeft = longCrcSection.SectionLength > 11 ? longCrcSection.SectionLength - 11 : 0;
            var loopLength = 0;
            var bytesLeft2 = bouquetDescriptorsLength;

            while (bytesLeft >= bytesLeft2 && bytesLeft2 > 1 && bytesLeft2 >= (loopLength = 2 + buffer[pos + 1]))
            {
				var tmpBuffer = new byte[buffer.Length - pos];
				Buffer.BlockCopy (buffer, pos, tmpBuffer, 0, buffer.Length - pos);
				descriptor(tmpBuffer, DescriptorScope.SCOPE_SI);
				tmpBuffer = null;
                pos += loopLength;
                bytesLeft -= loopLength;
                bytesLeft2 -= loopLength;
            }

            
            if (bytesLeft2 == 0 && bytesLeft > 1)
            {
                bytesLeft2 = Descriptor.DVB_LENGTH(buffer, pos);
                bytesLeft -= 2;
                pos += 2;

                Bouquet = new List<BouquetAssociation>();
                while (bytesLeft >= bytesLeft2 && bytesLeft2 > 4 && bytesLeft2 >= (loopLength = 6 + Descriptor.DVB_LENGTH(buffer, pos + 4)))
                {
                    var tmpBuffer = new byte[buffer.Length - pos];
                    Array.Copy(buffer, pos, tmpBuffer, 0, tmpBuffer.Length);
                    Bouquet.Add(new BouquetAssociation(tmpBuffer));
                    bytesLeft -= loopLength;
                    bytesLeft2 -= loopLength;
                    pos += loopLength;
                    tmpBuffer = null;
                }
            }
        }

        public List<BouquetAssociation> Bouquet { get; private set; }
    }
}
