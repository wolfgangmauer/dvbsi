using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvbsi
{
    class ConditionalAccessMessageSection : ShortSection
    {
        public static new UInt16 LENGTH = 256;
        public static new TableId TID = dvbsi.TableId.TID_CAMT_ECM_0;

        public ConditionalAccessMessageSection(byte[] buffer) : base(buffer)
        {
            if (SectionLength > 1)
            {
                CaDataByte = new List<byte>();
                for (var i = 8; i < SectionLength - 1; ++i)
                    CaDataByte.Add(buffer[i]);
            }
        }

        protected List<byte> CaDataByte { get; private set; }
    }
}
