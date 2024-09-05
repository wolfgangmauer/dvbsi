using System;
using System.Collections.Generic;

namespace dvbsi
{
    internal class Announcement
    {
		public Announcement(IReadOnlyList<byte> buffer, int index)
        {
			AnnouncementType = (byte)((buffer[index+0] >> 4) & 0x0f);
			ReferenceType = (byte)(buffer[index+0] & 0x07);

            if ((ReferenceType >= 0x01) && (ReferenceType <= 0x03))
            {
				OriginalNetworkId = Descriptor.UINT16(buffer, index+1);
				TransportStreamId = Descriptor.UINT16(buffer, index+3);
				ServiceId = Descriptor.UINT16(buffer, index+5);
				ComponentTag = buffer[index+7];
            }
        }

        public byte AnnouncementType { get; set; }
        public byte ReferenceType { get; set; }
        public ushort OriginalNetworkId { get; set; }
        public ushort TransportStreamId { get; set; }
        public ushort ServiceId { get; set; }
        public byte ComponentTag { get; set; }
    }

    internal class AnnouncementSupportDescriptor : Descriptor
	{

		protected override void Dispose(bool disposing)
        {
			Announcements = null;
			base.Dispose(disposing);
        }

		public AnnouncementSupportDescriptor(IReadOnlyList<byte> buffer, int index) : base(buffer, index)
        {
            Announcement a;
            var headerLength = 2;
            ASSERT_MIN_DLEN(headerLength);

			AnnouncementSupportIndicator = UINT16(buffer, index+2);

            Announcements = new List<Announcement>();

            for (var i = 0; i < DescriptorLength - 2; ++i)
            {
                headerLength++;
                ASSERT_MIN_DLEN(headerLength);

				a = new Announcement(buffer, index+i+4);
                Announcements.Add(a);
                switch (a.ReferenceType)
                {
                    case 0x01:
                    case 0x02:
                    case 0x03:
                        // FIXME: might already have parsed beyond end
                        // of memory in Announcement()
                        headerLength += 7;
                        ASSERT_MIN_DLEN(headerLength);
                        i += 7;
                        break;
                    default:
                        break;
                }
            }
        }

        public List<Announcement> Announcements { get; private set; }
        public ushort AnnouncementSupportIndicator { get; private set; }
    }
}

