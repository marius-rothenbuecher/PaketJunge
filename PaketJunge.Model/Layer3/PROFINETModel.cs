using System;
using System.Collections.Generic;
using PaketJunge.Packets.PROFINET;

namespace PaketJunge.Model
{
	public class PROFINETModel
	{
        public static List<string> GetDCPServiceIds()
        {
            var ids = Enum.GetValues(typeof(DCPServiceId));
            var idList = new List<string>();

            foreach (var id in ids)
                idList.Add(id.ToString());

            idList.Sort();

            return idList;
        }

        public static List<string> GetDCPServiceTypes()
        {
            var types = Enum.GetValues(typeof(DCPServiceType));
            var typeList = new List<string>();

            foreach (var type in types)
                typeList.Add(type.ToString());

            typeList.Sort();

            return typeList;
        }

        public static List<string> GetDCPTypes()
        {
            var types = Enum.GetValues(typeof(DCPType));
            var typeList = new List<string>();

            foreach (var type in types)
                typeList.Add(type.ToString());

            typeList.Sort();

            return typeList;
        }
    }
}
