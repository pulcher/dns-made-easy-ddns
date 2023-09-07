// See https://aka.ms/new-console-template for more information
using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;

namespace dynamicDNSTest
{
    public class program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            string hostName = Dns.GetHostName();

            DoGetHostAddresses(hostName);

            DisplayDnsConfiguration();
        }

        public static void DoGetHostAddresses(string hostname)
        {
            var addresses = Dns.GetHostAddresses(hostname);

            Console.WriteLine($"GetHostAddresses({hostname}) returns:");

            foreach (IPAddress address in addresses)
            {
                Console.WriteLine($"    {address}");
            }
        }

        public static void DisplayDnsConfiguration()
        {
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in adapters)
            {
                IPInterfaceProperties properties = adapter.GetIPProperties();
                var ipAddresses = properties.DnsAddresses.ToList();

                Console.WriteLine(adapter.Description);
                Console.WriteLine("  DNS suffix .............................. : {0}",
                    properties.DnsSuffix);
                Console.WriteLine("  DNS enabled ............................. : {0}",
                    properties.IsDnsEnabled);
                Console.WriteLine("  Dynamically configured DNS .............. : {0}",
                    properties.IsDynamicDnsEnabled);
                Console.WriteLine("  IP Address .............. : {0}",
                 string.Join(',', ipAddresses));
            }
            Console.WriteLine();
        }

        private static string bytesToString(IPAddress a)
        {
            var result = new StringBuilder();

            foreach(byte b in a.GetAddressBytes())
            {
                result.Append(b.ToString());
                result.Append('.');
            }

            return result.ToString();
        }
    }
}
