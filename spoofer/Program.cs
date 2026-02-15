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

            ProcessStartInfo tpmstatus = new ProcessStartInfo
            {
                FileName = "powershell.exe",
                Arguments = $"get-tpm",
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
            };
            ProcessStartInfo securebootstatus = new ProcessStartInfo
            {
                FileName = "powershell.exe",
                Arguments = $"Confirm-SecureBootUEFI",
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
            };
            ProcessStartInfo fastbooststatus = new ProcessStartInfo
            {
                FileName = "powershell.exe",
                Arguments = $"(GP \"HKLM:\\SYSTEM\\CurrentControlSet\\Control\\Session Manager\\Power\").\"HiberbootEnabled\"   ",
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
            };
            ProcessStartInfo virtstatus = new ProcessStartInfo
            {
                FileName = "powershell.exe",
                Arguments = $"(GWMI Win32_Processor).VirtualizationFirmwareEnabled",
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
            };
            StreamReader reader = Process.Start(tpmstatus).StandardOutput;
            StreamReader fbreader = Process.Start(fastbooststatus).StandardOutput;
            StreamReader sreader = Process.Start(securebootstatus).StandardOutput;
            StreamReader vreader = Process.Start(virtstatus).StandardOutput;
            string output = reader.ReadToEnd();
            string outputs = sreader.ReadToEnd();
            string outputfb = fbreader.ReadToEnd();
            string outputv = vreader.ReadToEnd();
            Process.Start(tpmstatus);
            Process.Start(securebootstatus);
            Process.Start(virtstatus);
            Console.Title = "Ryans Free Temp Spoofer";
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[+] ");
            Console.ResetColor();
            Console.Write("Ryan's Temp Spoofer! \n\n");
            // TPM STUFF
            if (output.Contains("TpmEnabled                : True"))
            {
                Console.Write("TPM Status: ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Enabled \n");
                Console.ResetColor();
            }
            else if (output.Contains("TpmEnabled                : False"))
            {
                Console.Write("TPM Status: ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Disabled \n");
                Console.ResetColor();
            }
            else
            {
                Console.Write("TPM Status: ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Unknown \n");
                Console.ResetColor();
            }
            // TPM STUFF ENDS HERE

            // SECURE BOOT STUFF
            if (outputs.Contains("True"))
            {
                Console.Write("Secure Boot Status: ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Enabled \n");
                Console.ResetColor();
            }
            else if (outputs.Contains("False"))
            {
                Console.Write("Secure Boot Status: ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Disabled \n");
                Console.ResetColor();
            }
            else
            {
                Console.Write("Secure Boot Status: ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Unknown \n");
                Console.ResetColor();
            }

            // SECURE BOOT STUFF ENDS HERE

            // FAST BOOT STUFF
            if (outputfb.Contains("1"))
            {
                Console.Write("Fast Boot Status: ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Enabled \n");
                Console.ResetColor();
            }
            else
            {
                Console.Write("Fast Boot Status: ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Disabled \n");
                Console.ResetColor();
            }

            // fast boot stuff stop

            if (outputv.Contains("True"))
            {
                Console.Write("Virtualization Status: ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Enabled \n\n");
                Console.ResetColor();
            }
            else
            {
                Console.Write("Virtualization Status: ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Disabled \n\n");
                Console.ResetColor();
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[+] ");
            Console.ResetColor();
            Console.Write("All Of these requirements must be disabled to spoof correctly \n\n");                
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
                Console.WriteLine("Press Any key to exit.");
                Console.ReadKey();
            }
        }
    }
}
