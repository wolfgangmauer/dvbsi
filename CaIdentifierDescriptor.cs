using System;
using System.Collections.Generic;

namespace dvbsi
{
    class CaIdentifierDescriptor : Descriptor
	{
		public CaIdentifierDescriptor(IReadOnlyList<byte> buffer, int index) : base(buffer, index)
        {
            CaSystemIds = new List<ushort>();
            for (var i = 0; i < DescriptorLength; i += 2)
				CaSystemIds.Add(UINT16(buffer, index+ i + 2));
        }

		protected override void Dispose (bool disposing)
		{
			// free managed resources
			if (disposing) {
				CaSystemIds = null;
			}
			base.Dispose (disposing);
		}

        public List<UInt16> CaSystemIds { get; private set; }
	}
}

