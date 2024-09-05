using System.Collections.Generic;

namespace dvbsi
{
    public class CableDeliverySystemDescriptor : Descriptor
    {
        public uint Frequency { get; private set; }
        public uint SymbolRate { get; private set; }
        public uint Modulation { get; private set; }
        public uint CodeRate { get; private set; }
        public uint Inversion { get; private set; }

        public CableDeliverySystemDescriptor(IReadOnlyList<byte> buffer, int index) : base(buffer, index)
        {
            Inversion = 2; // AUTO
            Frequency =
                (uint)(
               ((buffer[index + 2] >> 4)*1000000000) +
               ((buffer[index + 2] & 0x0F)*100000000) +
               ((buffer[index + 3] >> 4)*10000000) +
               ((buffer[index + 3] & 0x0F)*1000000) +
               ((buffer[index + 4] >> 4)*100000) +
               ((buffer[index + 4] & 0x0F)*10000) +
               ((buffer[index + 5] >> 4)*1000) +
               ((buffer[index + 5] & 0x0F)*100));
            if (Frequency > 1000*1000)
                Frequency /= 1000;
            SymbolRate = (uint)(
               ((buffer[index + 9] >> 4)*100000000) +
               ((buffer[index + 9] & 0x0F)*10000000) +
               ((buffer[index + 10] >> 4)*1000000) +
               ((buffer[index + 10] & 0x0F)*100000) +
               ((buffer[index + 11] >> 4)*10000) +
               ((buffer[index + 11] & 0x0F)*1000) +
               ((buffer[index + 12] >> 4)*100));
            Modulation = buffer[index + 8];
            CodeRate = (uint)(buffer[index + 12] & 0x0F);
        }
    }
}

