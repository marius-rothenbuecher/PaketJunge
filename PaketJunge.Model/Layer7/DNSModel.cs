using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public static List<string> GetDNSTypes()
        {
            var dnsTypes = Enum.GetValues(typeof(DnsType));
            var dnsTypeList = new List<string>();

            foreach (var dnsClass in dnsTypes)
                dnsTypeList.Add(dnsClass.ToString());

            dnsTypeList.Sort();

            return dnsTypeList;
        }

        public static List<string> GetDNSClasses()
        {
            var dnsClasses = Enum.GetValues(typeof(DnsClass));
            var dnsClassList = new List<string>();

            foreach (var dnsClass in dnsClasses)
                dnsClassList.Add(dnsClass.ToString());

            dnsClassList.Sort();

            return dnsClassList;
        }

        public static IList<DnsQueryResourceRecord> GetDnsQueryRecords(ObservableCollection<DNSQuery> queries)
        {
            var records = new List<DnsQueryResourceRecord>();

            foreach (var query in queries)
            {
                var dnsType = (DnsType)Enum.Parse(typeof(DnsType), query.DNSType);
                var dnsClass = (DnsClass)Enum.Parse(typeof(DnsClass), query.DNSClass);

                records.Add(new DnsQueryResourceRecord(new DnsDomainName(query.Domain), dnsType, dnsClass));
            }

            return records;
        }
    }
}
