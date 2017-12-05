using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Serialization;

namespace ShoppingAssistant.Logging
{
    public interface ILog
    {
        void Error(string tag, string message);

        void Warning(string tag, string message);

        void Debug(string tag, string message);

        void Info(string tag, string message);
    }
}
