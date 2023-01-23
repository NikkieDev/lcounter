using System;
using System.IO;
using System.Security.Principal;

#pragma warning disable

namespace counter
{
    class ErrorHandler
    {
        private static Config config = new Config();

        internal bool PrivHandler() // unactivated
        {
            bool isPrived = false;

            WindowsIdentity id = WindowsIdentity.GetCurrent();
            WindowsPrincipal principe = new WindowsPrincipal(id);

            if (principe.IsInRole(WindowsBuiltInRole.Administrator)) isPrived = true;

            return isPrived;
        }
        internal void handleArgs(int argsLength)
        {
            if (argsLength < 1)
                throw new Exception("Not enough arguments!");
            else if (argsLength > 1)
                throw new Exception("Too many arguments!");

            return;
        }
        internal void handleDir(string dir)
        {
            if (!Directory.Exists(dir))
                throw new DirectoryNotFoundException($"Could not find directory '{dir}'");
            else if (Directory.GetFiles(dir).Length < 1)
                throw new InvalidDataException("This directory is empty!");

            return;
        }
        internal bool writeError(string err)
        {
            bool status = false;
            try
            {
                if (File.ReadAllBytes(config.errFile).Length < 10) 
                {
                    File.AppendAllText(config.errFile, $"{err}"); 
                    status = true;
                }
                else
                {
                    File.AppendAllText(config.errFile, $"\n{err}");
                    status = true;
                }

                Console.WriteLine(err);
            }
            catch (Exception e) {Console.WriteLine($"Couldn't write to errorfile.\n{e.StackTrace}"); status = false;}

            return status;
        }
    }
}