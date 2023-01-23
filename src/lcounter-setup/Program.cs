using System;

namespace setup
{
    class Program
    {
        static void Main(string[] args)
        {
            Environment.SetEnvironmentVariable("PATH", Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Machine) + @";C:\Program Files\KuByX Softworks\lcounter", EnvironmentVariableTarget.Machine);
        }
    }
}