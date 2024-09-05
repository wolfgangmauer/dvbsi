using System.Collections.Generic;

namespace dvbsi
{
	public class LanguageDescriptor : Descriptor
	{
		public List<Language> Languages { get; private set; }
		public LanguageDescriptor (IReadOnlyList<byte> buffer, int index) : base(buffer, index)
		{
			Languages = new List<Language> ();
			for (var i = 0; i < DescriptorLength; i += 4) 
			{
				ASSERT_MIN_DLEN(i + 4);
				Languages.Add(new Language(buffer, index+i+2));
			}
		    Valid = true;
		}
        protected override void Dispose(bool disposing)
        {
            // free managed resources
            if (disposing)
            {
                Languages = null;
            }
            // free native resources if there are any.
            base.Dispose(disposing);
        }
    }

	public class Language
	{
		public string LanguageCode {
			get;
			private set;
		}

		public byte AudioType {
			get;
			private set;
		}

		public Language(IReadOnlyList<byte> buffer, int index)
		{
			LanguageCode = new DVBString(buffer, index, 3).Content;
			AudioType = buffer [index+3];
		}
	}
}

