using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace dvbsi
{
	public enum SiDescriptorTag : byte
	{
        // ReSharper disable InconsistentNaming
        /* 0x00 - 0x3F: ITU-T Rec. H.222.0 | ISO/IEC 13818-1 */
        VIDEO_STREAM_DESCRIPTOR = 0x02,
		AUDIO_STREAM_DESCRIPTOR				= 0x03,
		HIERARCHY_DESCRIPTOR				= 0x04,
		REGISTRATION_DESCRIPTOR				= 0x05,
		DATA_STREAM_ALIGNMENT_DESCRIPTOR		= 0x06,
		TARGET_BACKGROUND_GRID_DESCRIPTOR		= 0x07,
		VIDEO_WINDOW_DESCRIPTOR				= 0x08,
		CA_DESCRIPTOR					= 0x09,
		LANGUAGE_DESCRIPTOR			= 0x0A,
		SYSTEM_CLOCK_DESCRIPTOR				= 0x0B,
		MULTIPLEX_BUFFER_UTILIZATION_DESCRIPTOR		= 0x0C,
		COPYRIGHT_DESCRIPTOR				= 0x0D,
		MAXIMUM_BITRATE_DESCRIPTOR			= 0x0E,
		PRIVATE_DATA_INDICATOR_DESCRIPTOR		= 0x0F,
		SMOOTHING_BUFFER_DESCRIPTOR			= 0x10,
		STD_DESCRIPTOR					= 0x11,
		IBP_DESCRIPTOR					= 0x12,
		CAROUSEL_IDENTIFIER_DESCRIPTOR			= 0x13,
		/* 0x40 - 0x7F: ETSI EN 300 468 V1.9.1 (2009-03) */
		NETWORK_NAME_DESCRIPTOR				= 0x40,
		SERVICE_LIST_DESCRIPTOR				= 0x41,
		STUFFING_DESCRIPTOR				= 0x42,
		SATELLITE_DELIVERY_SYSTEM_DESCRIPTOR		= 0x43,
		CABLE_DELIVERY_SYSTEM_DESCRIPTOR		= 0x44,
		VBI_DATA_DESCRIPTOR				= 0x45,
		VBI_TELETEXT_DESCRIPTOR				= 0x46,
		BOUQUET_NAME_DESCRIPTOR				= 0x47,
		SERVICE_DESCRIPTOR				= 0x48,
		COUNTRY_AVAILABILITY_DESCRIPTOR			= 0x49,
		LINKAGE_DESCRIPTOR				= 0x4A,
		NVOD_REFERENCE_DESCRIPTOR			= 0x4B,
		TIME_SHIFTED_SERVICE_DESCRIPTOR			= 0x4C,
		SHORT_EVENT_DESCRIPTOR				= 0x4D,
		EXTENDED_EVENT_DESCRIPTOR			= 0x4E,
		TIME_SHIFTED_EVENT_DESCRIPTOR			= 0x4F,
		COMPONENT_DESCRIPTOR				= 0x50,
		MOSAIC_DESCRIPTOR				= 0x51,
		STREAM_IDENTIFIER_DESCRIPTOR			= 0x52,
		CA_IDENTIFIER_DESCRIPTOR			= 0x53,
		CONTENT_DESCRIPTOR				= 0x54,
		PARENTAL_RATING_DESCRIPTOR			= 0x55,
		TELETEXT_DESCRIPTOR				= 0x56,
		TELEPHONE_DESCRIPTOR				= 0x57,
		LOCAL_TIME_OFFSET_DESCRIPTOR			= 0x58,
		SUBTITLING_DESCRIPTOR				= 0x59,
		TERRESTRIAL_DELIVERY_SYSTEM_DESCRIPTOR		= 0x5A,
		MULTILINGUAL_NETWORK_NAME_DESCRIPTOR		= 0x5B,
		MULTILINGUAL_BOUQUET_NAME_DESCRIPTOR		= 0x5C,
		MULTILINGUAL_SERVICE_NAME_DESCRIPTOR		= 0x5D,
		MULTILINGUAL_COMPONENT_DESCRIPTOR		= 0x5E,
		PRIVATE_DATA_SPECIFIER_DESCRIPTOR		= 0x5F,
		SERVICE_MOVE_DESCRIPTOR				= 0x60,
		SHORT_SMOOTHING_BUFFER_DESCRIPTOR		= 0x61,
		FREQUENCY_LIST_DESCRIPTOR			= 0x62,
		PARTIAL_TRANSPORT_STREAM_DESCRIPTOR		= 0x63,
		DATA_BROADCAST_DESCRIPTOR			= 0x64,
		SCRAMBLING_DESCRIPTOR				= 0x65,
		DATA_BROADCAST_ID_DESCRIPTOR			= 0x66,
		TRANSPORT_STREAM_DESCRIPTOR			= 0x67,
		DSNG_DESCRIPTOR					= 0x68,
		PDC_DESCRIPTOR					= 0x69,
		AC3_DESCRIPTOR					= 0x6A,
		ANCILLARY_DATA_DESCRIPTOR			= 0x6B,
		CELL_LIST_DESCRIPTOR				= 0x6C,
		CELL_FREQUENCY_LINK_DESCRIPTOR			= 0x6D,
		ANNOUNCEMENT_SUPPORT_DESCRIPTOR			= 0x6E,
		APPLICATION_SIGNALLING_DESCRIPTOR		= 0x6F,
		ADAPTATION_FIELD_DATA_DESCRIPTOR		= 0x70,
		SERVICE_IDENTIFIER_DESCRIPTOR			= 0x71,
		SERVICE_AVAILABILITY_DESCRIPTOR			= 0x72,
		DEFAULT_AUTHORITY_DESCRIPTOR			= 0x73,	/* TS 102 323 */
		RELATED_CONTENT_DESCRIPTOR			= 0x74,	/* TS 102 323 */
		TVA_ID_DESCRIPTOR				= 0x75,	/* TS 102 323 */
		CONTENT_IDENTIFIER_DESCRIPTOR			= 0x76,	/* TS 102 323 */
		TIME_SLICE_FEC_IDENTIFIER_DESCRIPTOR		= 0x77,	/* EN 301 192 */
		ECM_REPETITION_RATE_DESCRIPTOR			= 0x78,	/* EN 301 192 */
		S2_SATELLITE_DELIVERY_SYSTEM_DESCRIPTOR		= 0x79,
		ENHANCED_AC3_DESCRIPTOR				= 0x7A,
		DTS_DESCRIPTOR					= 0x7B,
		AAC_DESCRIPTOR					= 0x7C,
		XAIT_LOCATION_DESCRIPTOR			= 0x7D,	/* TS 102 590 aka A107.MHP 1.2 Spec */
		FTA_CONTENT_MANAGEMENT_DESCRIPTOR		= 0x7E,
		EXTENSION_DESCRIPTOR				= 0x7F,
		/* 0x80 - 0xFE: User defined */
		LOGICAL_CHANNEL_DESCRIPTOR				= 0x83,	/* IEC 62216-1 */
		HD_SIMULCAST_LOGICAL_CHANNEL_DESCRIPTOR		= 0x88,	/* DIGITALEUROPE (former EICTA) extension to IEC 62216-1 */
		DAYLIGHT_SAVINGS_TIME = 0x96, 
		EXTENDED_CHANNEL_NAME_DESCRIPTION = 0xA0, 
		SERVICE_LOCATION = 0xA1, 
		/* 0xFF: Forbidden */
		FORBIDDEN_DESCRIPTOR				= 0xFF
	}

	public enum SiDescriptorTagExtension 
	{
		/* 0x00 - 0x7F: ETSI EN 300 468 V1.9.1 (2009-03) */
		IMAGE_ICON_DESCRIPTOR				= 0x00,
		CPCM_DELIVERY_SIGNALLING_DESCRIPTOR		= 0x01,	/* TS/TR 102 825 */
		CP_DESCRIPTOR					= 0x02,	/* TS/TR 102 825 */
		CP_IDENTIFIER_DESCRIPTOR			= 0x03, /* TS/TR 102 825 */
		/* 0x80 - 0xFF: User defined */
	}

	public enum CarouselDescriptorTag 
	{
		/* ETSI EN 301 192 V1.3.1 (2003-05) */
		/* 0x00: Reserved */
		TYPE_DESCRIPTOR					= 0x01,
		NAME_DESCRIPTOR					= 0x02,
		INFO_DESCRIPTOR					= 0x03,
		MODULE_LINK_DESCRIPTOR				= 0x04,
		CRC32_DESCRIPTOR				= 0x05,
		LOCATION_DESCRIPTOR				= 0x06,
		EST_DOWNLOAD_TIME_DESCRIPTOR			= 0x07,
		GROUP_LINK_DESCRIPTOR				= 0x08,
		COMPRESSED_MODULE_DESCRIPTOR			= 0x09,
		SUBGROUP_ASSOCIATION_DESCRIPTOR			= 0x0A,
		/* 0x0B - 0x6F: Reserved */
		/* ETSI TS 101 812 V1.2.1 (2003-06) */
		LABEL_DESCRIPTOR				= 0x70,
		CACHING_PRIORITY_DESCRIPTOR			= 0x71,
		CONTENT_TYPE_DESCRIPTOR				= 0x72,
		/* 0x5F: PRIVATE_DATA_SPECIFIER_DESCRIPTOR */
		/* 0x73 - 0x7F: Reserved */
		/* 0x80 - 0xFE: User defined */
		/* 0xFF: FORBIDDEN_DESCRIPTOR */
	}

	public enum MhpDescriptorTag 
	{
		/* ETSI TS 101 812 V1.2.1 (2003-06) */
		APPLICATION_DESCRIPTOR				= 0x00,
		APPLICATION_NAME_DESCRIPTOR			= 0x01,
		TRANSPORT_PROTOCOL_DESCRIPTOR			= 0x02,
		DVB_J_APPLICATION_DESCRIPTOR			= 0x03,
		DVB_J_APPLICATION_LOCATION_DESCRIPTOR		= 0x04,
		EXTERNAL_APPLICATION_AUTHORISATION_DESCRIPTOR	= 0x05,
		/* 0x06 - 0x07: Reserved */
		DVB_HTML_APPLICATION_DESCRIPTOR			= 0x08,
		DVB_HTML_APPLICATION_LOCATION_DESCRIPTOR	= 0x09,
		DVB_HTML_APPLICATION_BOUNDARY_DESCRIPTOR	= 0x0A,
		APPLICATION_ICONS_DESCRIPTOR			= 0x0B,
		PREFETCH_DESCRIPTOR				= 0x0C,
		DII_LOCATION_DESCRIPTOR				= 0x0D,
		DELEGATED_APPLICATION_DESCRIPTOR		= 0x0E,
		PLUGIN_DESCRIPTOR				= 0x0F,
		APPLICATION_STORAGE_DESCRIPTOR			= 0x10,
		IP_SIGNALING_DESCRIPTOR				= 0x11,
		/* ETSI TS 102 809 V1.1.1 (2010-01) */
		GRAPHICS_CONSTRAINTS_DESCRIPTOR			= 0x14,
		SIMPLE_APPLICATION_LOCATION_DESCRIPTOR		= 0x15,
		APPLICATION_USAGE_DESCRIPTOR			= 0x16,
		SIMPLE_APPLICATION_BOUNDARY_DESCRIPTOR		= 0x17,
		/* 0x12 - 0x5E: Reserved */
		/* 0x5F: PRIVATE_DATA_SPECIFIER_DESCRIPTOR */
		/* 0x80 - 0xFE: User defined */
		/* 0xFF: FORBIDDEN_DESCRIPTOR */
	}
    // ReSharper restore InconsistentNaming

    public class Descriptor : IDisposable
	{
		byte[] _dataBytes;

		public static uint UINT32 (IReadOnlyList<byte> buffer, int index)
		{
			//return BitConverter.ToUInt32(buffer, index);
			return (uint)((ushort)(buffer[1] << 8 | buffer[0]) << 16)  |  (ushort)(buffer[2] << 8 | buffer[3]);
		}

		public static ushort UINT16 (IReadOnlyList<byte> buffer, int index)
		{
			return (ushort)(buffer[index] << 8 | buffer[index+1]);
		}

		public static ushort DVB_PID(IReadOnlyList<byte> buffer, int index)
		{
			return (ushort)(UINT16 (buffer, index) & 0x1fff);
		}

		public static ushort DVB_LENGTH(IReadOnlyList<byte> buffer, int index)
		{
			return (ushort)(((buffer [index] & 0x0F) << 8) | buffer [index + 1]);
		}

		protected void ASSERT_MIN_DLEN(int length)
		{
            if (DescriptorLength < length) 
			{
				Valid = false;
                Debug.Assert(Valid);
			}
		}

		#region IDisposable implementation
		public void Dispose ()
		{
			Dispose (true);
			GC.SuppressFinalize (this);
		}

		protected virtual void Dispose (bool disposing)
		{
			// free managed resources
			if (disposing) {
				_dataBytes = null;
			}
			// free native resources if there are any.
		}
		#endregion

		public Descriptor (IReadOnlyList<byte> buffer, int index)
		{
			Tag = (SiDescriptorTag)buffer[index+0];
            DescriptorLength = buffer[index + 1];
            _dataBytes = new byte[DescriptorLength + 2];
			Buffer.BlockCopy(buffer.ToArray(), 0, _dataBytes, 2, DescriptorLength);
			Valid = false;
		}
		
		public SiDescriptorTag Tag { get; private set; }
		public byte DescriptorLength { get; private set; }
		public bool Valid { get; protected set; }

		public int WriteToBuffer(byte[] buffer)
		{
			buffer[0] = (byte)Tag;
            buffer[1] = DescriptorLength;
            Array.Copy(_dataBytes, 0, buffer, 2, DescriptorLength);
            return DescriptorLength + 2;
		}
	}
}

