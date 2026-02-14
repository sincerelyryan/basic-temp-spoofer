using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace spoofer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Ryans Free Temp Spoofer";
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[+] ");
            Console.ResetColor();
            Console.Write("Ryan's Temp Spoofer! \n\n");
            Console.WriteLine("[1] Spoof (Randomize)");
            Console.WriteLine("[2] Clean");
            string input = Console.ReadLine();
            if (input == "1")
            {
                string mainpath = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);
                string driverpath = Path.Combine(mainpath, "tempspoofdriver.sys");
                string mapperpath = Path.Combine(mainpath, "mapper.exe");
                string cleanerpath = Path.Combine(mainpath, "cleaner.exe");
                WebClient wc = new WebClient();
                wc.DownloadFile("https://github.com/sincerelyryan/spoofstuff/raw/refs/heads/main/driver_temp.bin", driverpath);
                wc.DownloadFile("https://github.com/sincerelyryan/spoofstuff/raw/refs/heads/main/saturn.bin", mapperpath);      
                //  Console.WriteLine(driverpath);
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/c {mapperpath} {driverpath}",
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                ProcessStartInfo startInfo2 = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/c net stop winmgmt /y",
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                ProcessStartInfo startInfo3 = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/c net start winmgmt /y",
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                Process.Start(startInfo);

                Console.WriteLine("Spoofing Done!");
                Process.Start(startInfo2);
                Console.WriteLine("Please wait...");
                Thread.Sleep(500);
                Process.Start(startInfo3);
                Console.WriteLine("Almost There!");
                Thread.Sleep(500);
                Console.WriteLine("Done Spoofing! Check Serials! press enter to exit.");
                Console.ReadKey();


            }
            else if (input == "2")
            {
                string mainpath = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);
                string cleanerpath = Path.Combine(mainpath, "cleaner.bat");
                ProcessStartInfo cleanerprocess = new ProcessStartInfo
                {
                    FileName = cleanerpath,
                    UseShellExecute = false,
                    Verb = "runas",
                };
                
                
                Console.WriteLine("Cleaning!");
                WebClient wc = new WebClient();
                wc.DownloadFile("https://raw.githubusercontent.com/sincerelyryan/spoofstuff/refs/heads/main/clean.bat", cleanerpath);
                Console.WriteLine("Press Enter to start cleaning...");
                Console.ReadKey();             
                Process.Start(cleanerprocess);
                Console.ReadKey();
            }
        }
    }
}
