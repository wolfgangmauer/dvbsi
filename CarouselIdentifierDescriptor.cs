using System;
using System.Collections.Generic;

namespace dvbsi
{
	public class CarouselIdentifierDescriptor : Descriptor
	{
		public CarouselIdentifierDescriptor(IReadOnlyList<byte> buffer, int index) : base(buffer, index)
		{
		}
	}
}

