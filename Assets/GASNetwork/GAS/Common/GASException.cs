using System;

namespace GAS.Common
{
    public class GASException : Exception
    {
        public int Code { get; }

        public GASException(string msg) : base(msg) { Code = -500; }
        public GASException(int code, string msg) : base(msg) { Code = code; }
    }

    public class GASNetworkException : GASException
    {
        public string RawText { get; }
        public GASNetworkException(int code, string msg, string raw = null) : base(code, msg) { RawText = raw; }
    }

    public class GASParseException : GASException
    {
        public GASParseException(string msg) : base(-1, msg) { }
    }

}