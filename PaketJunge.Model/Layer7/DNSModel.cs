using System;
using System.Collections.Generic;
using PcapDotNet.Packets.Dns;

namespace PaketJunge.Model.Layer7
{
    public class DNSModel
    {
        public static List<string> GetCompressionModes()
        {
            var modes = Enum.GetValues(typeof(DnsDomainNameCompressionMode));
            var modeList = new List<string>();

            foreach (var mode in modes)
                modeList.Add(mode.ToString());

            modeList.Sort();

            return modeList;
        }

        public static List<string> GetOpCodes()
        {
            var opCodes = Enum.GetValues(typeof(DnsOpCode));
            var opCodeList = new List<string>();

            foreach (var mode in opCodes)
                opCodeList.Add(mode.ToString());

            opCodeList.Sort();

            return opCodeList;
        }

        public static List<string> GetResponseCodes()
        {
            var responseCodes = Enum.GetValues(typeof(DnsResponseCode));
            var responseCodeList = new List<string>();

            foreach (var responseCode in responseCodes)
                responseCodeList.Add(responseCode.ToString());

            responseCodeList.Sort();

            return responseCodeList;
        }
    }
}
