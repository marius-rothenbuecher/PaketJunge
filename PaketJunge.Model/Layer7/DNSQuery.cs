namespace PaketJunge.Model.Layer7
{
    public class DNSQuery
    {
        public DNSQuery(string dnsType, string dnsClass, string domain)
        {
            this.DNSType = dnsType;
            this.DNSClass = dnsClass;
            this.Domain = domain;
        }

        public string DNSType { get; set; }

        public string DNSClass { get; set; }

        public string Domain { get; set; }
    }
}
