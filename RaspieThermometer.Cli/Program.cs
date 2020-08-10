namespace Starkie.RaspieThermometer.Cli
{
    using System;

    class Program
    {
        private const string DevicesDirectory = "/sys/bus/w1/devices/";

        static void Main(string[] args)
        {
            Thermometer thermometer = new Thermometer(DevicesDirectory);
            Console.WriteLine(thermometer.Temperature);
        }
    }
}