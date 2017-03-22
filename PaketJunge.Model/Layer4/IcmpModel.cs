using System;
using System.Collections.Generic;
using PcapDotNet.Packets.Icmp;

namespace PaketJunge.Model.Layer4
{
    public class ICMPModel
    {
        public static List<string> GetTypesAndCodes()
        {
            var typesAndCodes = Enum.GetValues(typeof(IcmpMessageTypeAndCode));
            var typAndCodesList = new List<string>();

            foreach (var typeAndCode in typesAndCodes)
                typAndCodesList.Add(typeAndCode.ToString());

            typAndCodesList.Sort();

            return typAndCodesList;
        }
    }
}
