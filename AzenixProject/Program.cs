using AzenixProject.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace AzenixProject
{
    public class Program
    {

        const string incorrectFileFormatMessage = "Incorrect file format";
        const string emptyFileMessage = "Empty File";
        static void Main(string[] args)
        {
            Program Program = new Program();
            Program.ReadFiles("programming-task-example-data.log");
        }

        const string uniqueIPAddressMessage = "Unique IP addresses:";
        const string topThreeIpAddressesMessage = "The top 3 IP Addresses are:";
        const string topThreeWebsitesMessage = "The top 3 URLs are:";
        public void ReadFiles(string file)
        {
            if (CheckFileName(file))
            {
                var filepath = $"LogFiles\\{file}";
                var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filepath);

                string[] records = File.ReadAllLines(path);
                if (records != null || records.Count() > 1)
                {

                    List<LogData> log = new List<LogData>();
                    foreach (string record in records)
                    {
                        var logData = new LogData();
                        var split = record.Split(" ");
                        if (split.Count() == 23)
                        {
                            var ipAddress = split[0];
                            var url = split[6];
                            logData.IPAddress = ipAddress;
                            logData.URL = url;
                            log.Add(logData);
                        }
                    }

                    if (log != null && log.Count() > 0)
                    {

                        WriteUniqueIpCount(log);
                        WriteTopThreeIPAddresses(log);
                        WriteTopThreeURLs(log);
                    }
                }
            }
        }


        private void WriteTopThreeIPAddresses(List<LogData> logData)
        {


           var ips = logData.GroupBy(q => q.IPAddress)
           .OrderByDescending(gp => gp.Count())
           .Take(3)
           .Select(g => g.Key).ToList();

            Console.WriteLine(topThreeIpAddressesMessage);
            ips.ForEach(x => Console.WriteLine(x));
        }

        public bool CheckFileName(string file)
        {
            return !string.IsNullOrEmpty(file) && file.Contains(".log");
        }

        private void  WriteTopThreeURLs(List<LogData> logData)
        {


            var urls = logData.GroupBy(q => q.URL)
            .OrderByDescending(gp => gp.Count())
            .Take(3)
            .Select(g => g.Key).ToList();

            Console.WriteLine(topThreeWebsitesMessage);
            urls.ForEach(x => Console.WriteLine(x));
        }

        private void WriteUniqueIpCount(List<LogData> logData)
        {
            var uniqueIpCount = logData.Select(a => a.IPAddress).Distinct().Count();
            Console.WriteLine($"{uniqueIPAddressMessage} - {uniqueIpCount.ToString()}");
        }

    }
}
