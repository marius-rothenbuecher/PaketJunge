using System;
using System.Collections.Generic;
using PcapDotNet.Packets.Icmp;

namespace PaketJunge.Model.Layer4
{
    public class ICMPModel
    {
        public static List<string> GetTypesAndCodes()
        {
            Array values = Enum.GetValues(typeof(IcmpMessageTypeAndCode));
            var protocolList = new List<string>();

            foreach (var typeAndCode in values)
                protocolList.Add(typeAndCode.ToString());

            protocolList.Sort();

            return protocolList;
        }
    }
}
