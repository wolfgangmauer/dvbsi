using System;
using System.Collections.Generic;
using System.Linq;

namespace dvbsi
{
    public class ExtensionDescriptor : Descriptor
    {
        public byte[] SelectorBytes { get; private set; }

        public byte ExtensionTag { get; private set; }

        public ExtensionDescriptor(IReadOnlyList<byte> buffer, int index) : base(buffer, index)
        {
            ASSERT_MIN_DLEN(1);

            ExtensionTag = buffer[index + 2];
            SelectorBytes = new byte[DescriptorLength - 1];
            Array.Copy(buffer.ToArray(), 3, SelectorBytes, 0, DescriptorLength - 1);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                SelectorBytes = null;
            }
            base.Dispose(disposing);
        }
    }
}

