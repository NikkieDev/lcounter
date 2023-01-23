using System;
using System.IO;

namespace counter 
{
    class Program
    {
        private static ErrorHandler errorHandler = new ErrorHandler();
        private static Config config = new Config();
        
        private static void Initialize()
        {
            // if (errorHandler.PrivHandler() == false) {Console.WriteLine("Run this program as administrator!"); Environment.Exit(1);}
            if (!Directory.Exists(config.dir)) Directory.CreateDirectory(config.dir);
            if (!File.Exists(config.errFile))
            {
                FileStream _file = File.Create(config.errFile);
                _file.Close();
            }
        }

        private static int countLines(string dir)
        {
            int counted = 0;

            foreach(dynamic file in Directory.GetFiles(dir))
            {
                foreach(string line in File.ReadAllLines(file))
                    counted++;
            }

            return counted;
        }
        static void Main(string[] args)
        {
            int? counted = new Int32();
            #nullable enable
            string? dirName;
            #nullable disable

            try {Initialize();errorHandler.handleArgs(args.Length);}
            catch (Exception e) {errorHandler.writeError($"[ArgumentError] An error has occured: {e.Message}"); Environment.Exit(231);}

            try 
            {
                dirName = args[0];
                errorHandler.handleDir(dirName);
                counted = countLines(dirName);
            }
            catch (DirectoryNotFoundException e) {errorHandler.writeError($"[DirectoryError] An error has occured:  {e.Message}"); Environment.Exit(232);}
            catch (InvalidDataException e) {errorHandler.writeError($"[DirectoryError] An error has occured:  {e.Message}"); Environment.Exit(233);}
            catch (Exception e) {errorHandler.writeError($"[DirectoryError] An error has occured: {e.Message}"); Environment.Exit(234);}
            
            Console.WriteLine($"Total amount of lines: {counted}");
        }
    }
}