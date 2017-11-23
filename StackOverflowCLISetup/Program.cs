using System;
using System.IO;
using System.Security.Principal;

namespace StackOverflowCLISetup
{
    public class Program
    {
        public static void Main(string[] args)
        {
            bool isElevated;
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                isElevated = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            if(!isElevated)
            {
                Console.WriteLine("Please run this setup as an Administrator");
                return;
            }
            var currentPath = Environment.GetEnvironmentVariable("Path", EnvironmentVariableTarget.Machine);
            var currentDirectory = Directory.GetCurrentDirectory();
            if(currentPath.Contains(currentDirectory))
            {
                Console.WriteLine("StackOverflowCLI is already installed on this machine");
                return;
            }
            Environment.SetEnvironmentVariable("Path", currentPath + currentDirectory + ";", EnvironmentVariableTarget.Machine);
            Console.WriteLine("Successfully Installed StackOverflowCLI");
        }
    }
}
