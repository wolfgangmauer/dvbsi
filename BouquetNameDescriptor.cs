using System.Collections.Generic;

namespace dvbsi
{
    public class BouquetNameDescriptor : Descriptor
	{
        public BouquetNameDescriptor(IReadOnlyList<byte> buffer, int index) : base(buffer, index)
        {
			BouquetName = new DVBString(buffer, index+2, DescriptorLength).Content;
            Valid = true;
        }

        public string BouquetName { get; private set; }
    }
}

