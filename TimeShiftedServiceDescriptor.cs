using System;
using System.Collections.Generic;

namespace dvbsi
{
	public class TimeShiftedServiceDescriptor : Descriptor
	{
		public TimeShiftedServiceDescriptor(IReadOnlyList<byte> buffer, int index) : base(buffer, index)
		{
		}
	}
}

