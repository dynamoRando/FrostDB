using System;
using System.Collections.Generic;
using System.Text;

namespace FrostCommon.Net
{
    public class FrostPromptResponse
    {
        public bool IsSuccessful = false;
        public string Message = string.Empty;
        public int NumberOfRowsAffected = 0;
        public string JsonData = string.Empty;
    }
}
