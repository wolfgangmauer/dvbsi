using System;
using System.Collections.Generic;
using System.Text;

namespace dvbsi
{
    class ApplicationName
    {
        public ApplicationName(byte[] buffer)
        {
			LanguageCode = new ().GetString(buffer, 0, 3);
            var applicationNameLength = buffer[3];
			Name = new ISO6937Encoding().GetString(buffer, 4, applicationNameLength);
        }

        public string Name { get; private set; }
        public string LanguageCode { get; private set; }
    }

	class ApplicationNameDescriptor : Descriptor
	{
        public ApplicationNameDescriptor(byte[] buffer) : base(buffer)
        {
            ApplicationNames = new List<ApplicationName>();
            for (var i = 0; i < DescriptorLength; i += buffer[i + 5] + 4)
            {
                ASSERT_MIN_DLEN(i + buffer[i + 5] + 4);
                var tmpBuffer = new byte[buffer.Length - (i + 2)];
                Array.Copy(buffer, i + 2, tmpBuffer, 0, tmpBuffer.Length);
                ApplicationNames.Add(new ApplicationName(tmpBuffer));
                tmpBuffer = null;
            }
        }

        public List<ApplicationName> ApplicationNames { get; private set; }
	}
}

