using System;

namespace Kopf.PacketPal
{
	public static class AddressUtil
	{
		public static long addressFromString(string address)
		{
		    string [] arrDec;
			int num = 0;
			if (address != "")
			{
				arrDec = address.Split('.');
				num = (int.Parse(arrDec[3]))+(int.Parse(arrDec[2])*256)+(int.Parse(arrDec[1])*65536)+(int.Parse(arrDec[0])*16777216));
			}
			return num;
		}
		
		public static string addressFromLong(long address)
		{
			
		}
	}
}
