using System;
using System.Collections.Generic;
using System.Text;

namespace dvbsi
{
	class LabelDescriptor : Descriptor
	{
        public LabelDescriptor(byte[] buffer) : base(buffer)
        {
            Label = Encoding.UTF8.GetString(buffer, 2, DescriptorLength);
        }

        public string Label { get; private set; }
    }
}

