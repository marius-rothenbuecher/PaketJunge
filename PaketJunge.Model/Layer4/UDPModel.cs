using System;
using System.Collections.Generic;

namespace PaketJunge.Model.Layer4
{
    public class UDPModel
    {
        public static List<string> GetProtocols()
        {
            var protocols = Enum.GetValues(typeof(UDPProtocol));
            var protocolList = new List<string>();

            foreach (var protocol in protocols)
                protocolList.Add(protocol.ToString());

            protocolList.Sort();

            return protocolList;
        }
    }
}
