// ============================================
// 
// FlexImage.Core
// Network.cs
// 26/08/2012
// cflavio
// 
// ============================================

#region

using System;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

#endregion

namespace FlexImage.Core
{
    public static class Network
    {
        public static string GetFQDN()
        {
            string domainName = IPGlobalProperties.GetIPGlobalProperties().DomainName;
            string hostName = Dns.GetHostName();
            string fqdn = "";
            if (!hostName.Contains(domainName))
                fqdn = hostName + "." + domainName;
            else
                fqdn = hostName;
            return fqdn;
        }

        public static double GetDiskSpaceAvaiable()
        {
            string drive = Directory.GetCurrentDirectory().Substring(0, 1);
            var di = new DriveInfo(drive + ":\\");

            //return the free space amount
            double freeSpace = Convert.ToDouble(di.TotalFreeSpace);
            return freeSpace;
        }

        public static string GetDomainName()
        {
            string domainName = IPGlobalProperties.GetIPGlobalProperties().DomainName;
            return domainName;
        }

        public static string GetComputerName()
        {
            return Dns.GetHostName().ToUpper();
        }

        public static string GetLocalIP()
        {
            string IP = "";
            IPHostEntry ipEntry = Dns.GetHostEntry(GetComputerName());
            IPAddress[] addr = ipEntry.AddressList;

            for (int i = 0; i < addr.Length; i++)
            {
                string address = addr[i].ToString();
                if (addr[i].AddressFamily != AddressFamily.InterNetworkV6)
                {
                    if (address.Length >= 8)
                    {
                        if (address.Substring(0, 8) != "169.254.")
                        {
                            IP = IP + addr[i];
                            break;
                        }
                    }
                }
            }

            return IP;
        }

        public static string GetCurrentUserName()
        {
            return Environment.UserName;
        }

        public static string GetOSVersion()
        {
            return Environment.OSVersion.ToString();
        }
    }
}