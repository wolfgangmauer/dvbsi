using System;
using System.Collections.Generic;

namespace dvbsi
{
	public class Event : DescriptorContainer
	{
		public ushort EventId {
			get;
			private set;
		}

		public DateTime StartTime {
			get;
			private set;
		}

		public long Duration {
			get;
			private set;
		}

		public RunningStatus RunningStatus {
			get;
			private set;
		}

		public int FreeCaMode {
			get;
			private set;
		}

	    private static DateTime ChangeUtCtoCtime (IReadOnlyList<byte> buffer, int i)
		{
			var mjd = (buffer[i+0] << 8) | buffer[i+1];
			var hour = buffer[i+2];
			var minutes = buffer[i+3];
			var seconds = buffer[i+4];
			var y   = (int) ((mjd - 15078.2) / 365.25);
			var m   = (int) ((mjd - 14956.1 - (int) (y * 365.25)) / 30.6001);
			var day  = mjd - 14956 - (int) (y * 365.25) - (int) (m * 30.60001);
			var k = (m == 14 || m == 15) ? 1 : 0;
			var year  = y + k + 1900;
			var month = m - 1 - k * 12;

			var tmMday = day;
			var tmMon = month;
			var tmYear = year;
			var tmHour = (hour >> 4) * 10 + (hour & 0x0f);
			var tmMin = (minutes >> 4) * 10 + (minutes & 0x0f);
			var tmSec = (seconds >> 4) * 10 + (seconds & 0x0f);

			return new DateTime (tmYear, tmMon, tmMday, tmHour, tmMin, tmSec).ToLocalTime ();
		}		

		public Event(IReadOnlyList<byte> buffer, int index)
		{
			EventId = dvbsi.Descriptor.UINT16(buffer, index+0);
			try
			{
				StartTime = ChangeUtCtoCtime(buffer, index + 2);
			}
			catch(ArgumentOutOfRangeException) 
			{
				StartTime = DateTime.MinValue;
			}
			Duration = ((buffer[index + 7])>>4)*10*3600L + ((buffer[index + 7])&0x0f)*3600L +
				((buffer[index + 8])>>4)*10*60L + ((buffer[index + 8])&0x0f)*60L +
					((buffer[index + 9])>>4)*10 + ((buffer[index + 9])&0x0f);
		
			RunningStatus = (RunningStatus)((buffer[index + 10] >> 5) & 0x07);
			FreeCaMode = (buffer[index + 10] >> 4) & 0x01;
			var descriptorsLoopLength = dvbsi.Descriptor.DVB_LENGTH(buffer, index + 10);

			for (var i = 12; i < descriptorsLoopLength + 12; i += buffer[index + i + 1] + 2) {
				Descriptor (buffer, index+i, DescriptorScope.ScopeSi);
			}
		}
	}

	public class EventInformationSection : LongCrcSection
	{
		public ushort TransportStreamId {
			get;
			private set;
		}

		public ushort OriginalNetworkId {
			get;
			private set;
		}

		public List<Event> Events
		{
			get;
			private set;
		}

		public EventInformationSection (IReadOnlyList<byte> buffer) : base(buffer)
		{
            Events = new List<Event> ();
			TransportStreamId = SectionLength > 10 ? Descriptor.UINT16(buffer, 8) : (ushort)0;
			OriginalNetworkId = SectionLength > 12 ? Descriptor.UINT16(buffer, 10) : (ushort)0;
			LastSectionNumber = SectionLength > 13 ? buffer[12] : SectionNumber;
			TableId = SectionLength > 14 ? buffer[13] : buffer[0];

			var pos = 14;
			var bytesLeft = SectionLength > 15 ? SectionLength-15 : 0;
			int loopLength;
			while (bytesLeft > 11 && bytesLeft >= (loopLength = 12 + Descriptor.DVB_LENGTH(buffer, pos+10))) {
				Events.Add(new Event(buffer, pos));
			    bytesLeft -= loopLength;
				pos += loopLength;
			}		
		}

		protected override void Dispose (bool disposing)
		{
			if (disposing)
			{
				foreach (var e in Events) {
					e.Dispose ();
				}
				Events.Clear ();
				Events = null;
			}
			base.Dispose (disposing);
		}
	}
}

