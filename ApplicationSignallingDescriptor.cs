using System;
using System.Collections.Generic;

namespace dvbsi
{
    class ApplicationSignalling
    {
		public ApplicationSignalling(IReadOnlyList<byte> buffer, int index)
        {
			ApplicationType = Descriptor.UINT16(buffer,index+0);
			AitVersionNumber = (byte)(buffer[index+2] & 0x1f);
        }
        public ushort ApplicationType { get; private set; }

        public byte AitVersionNumber { get; private set; }
    }

	class ApplicationSignallingDescriptor : Descriptor
	{
		public ApplicationSignallingDescriptor(IReadOnlyList<byte> buffer, int index) : base(buffer, index)
        {
            ApplicationSignallings = new List<ApplicationSignalling>();
            for (var i = 0; i < DescriptorLength; i += 3)
            {
                ASSERT_MIN_DLEN(i + 3);
				ApplicationSignallings.Add(new ApplicationSignalling(buffer, index+i+2));
            }
        }

		protected override void Dispose (bool disposing)
		{
			// free managed resources
			if (disposing) {
				ApplicationSignallings = null;
			}
			base.Dispose (disposing);
		}
        public List<ApplicationSignalling> ApplicationSignallings { get; private set; }
	}
}

