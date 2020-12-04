using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace csharp_webserver
{
    public class PublicHostAdressService
    {
        private String ipAdress;

        public String getIpAdress() { return ipAdress; }

        public PublicHostAdressService()
        {
            getPublicIpAdress();
        }

        private void getPublicIpAdress()
        {

            Process p = new Process();

            p.StartInfo.FileName = "IPCONFIG";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.Arguments = "";
            p.StartInfo.RedirectStandardOutput = true;
            p.Start();

            try {

                String pattern = @"IPv4-Adresse  . . . . . . . . . . : ([0-9.]*)";
                String output = p.StandardOutput.ReadToEnd();
                Match match = Regex.Match(output, pattern, RegexOptions.IgnoreCase);
                ipAdress = match.Groups[1].Value;

                if (string.IsNullOrEmpty(ipAdress))
                {
                    ipAdress = "127.0.0.1";
                }

            } catch (Exception e)
            {

                ipAdress = "127.0.0.1";
            }

        }
    }
}