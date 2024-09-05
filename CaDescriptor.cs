using System;
using System.Collections.Generic;
using System.Linq;

namespace dvbsi
{
	public class CaDescriptor : Descriptor
	{
		public CaDescriptor(IReadOnlyList<byte> buffer, int index) : base(buffer, index)
		{
			ASSERT_MIN_DLEN(4);

			CaSystemId = UINT16(buffer, index+2);
			CaPid = DVB_PID(buffer, index + 4);

            CaDataBytes = new byte[DescriptorLength - 4];
            Array.Copy(buffer.ToArray(), index + 6, CaDataBytes, 0, CaDataBytes.Length);
		    Valid = true;
		}
        protected override void Dispose(bool disposing)
        {
            // free managed resources
            if (disposing)
            {
                CaDataBytes = null;
            }
            // free native resources if there are any.
            base.Dispose(disposing);
        }
        public ushort CaSystemId { get; private set; } 
		public ushort CaPid { get; private set; } 
		public byte[] CaDataBytes { get; private set; } 
	}
}

