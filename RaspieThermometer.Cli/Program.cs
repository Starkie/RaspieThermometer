namespace Starkie.RaspieThermometer.Cli
{
    using System;

    class Program
    {
        // The default path for the sensors device in the Raspberry Pi.
        private const string DevicesDirectory = "/sys/bus/w1/devices/";

        static void Main(string[] args)
        {
            Thermometer thermometer = new Thermometer(DevicesDirectory);
            Console.WriteLine(thermometer.Temperature);
        }
    }
}