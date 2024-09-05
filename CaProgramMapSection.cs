using System;
using System.Collections.Generic;

namespace dvbsi
{
	public class CaProgramMapSection : DescriptorContainer
		{
		protected UInt32 length;
		protected UInt32 caPmtTag;
		protected Byte  caPmtListManagement;
		protected UInt16 programNumber;
		protected Byte versionNumber;
		protected Byte currentNextIndicator;
		protected UInt16 programInfoLength;
		protected Byte caPmtCmdId;
			
		List<CaProgramMapSection> esInfo;
		List<UInt16> caids;

		public CaProgramMapSection(ref ProgramMapSection pmt, byte listManagement, byte cmdId, List<UInt16> pcaids)
		{
			programInfoLength = 0;
			caids = pcaids;
			length = 6;

			caPmtTag = 0x9f8032;
			caPmtListManagement = listManagement;
			caPmtCmdId = cmdId;

//			programNumber = pmt.tableIdExtension;
//			versionNumber = pmt->versionNumber;
//			currentNextIndicator = pmt->currentNextIndicator;
//
//			append(pmt);
//			return 0;

		}

		bool append(ProgramMapSection pmt)
		{
			return true;
		}

		void injectDescriptor(ref byte descriptor, bool back=true)
		{
		}

		void setListManagement(byte listmanagement)
		{
		}

		ulong writeToBuffer(ref byte buffer)
		{
			return 0;
		}

		ulong writeToFile(int fd)
		{
			return 0;
		}
	}
}

