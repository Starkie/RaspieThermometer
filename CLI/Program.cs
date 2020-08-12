namespace Starkie.RaspieThermometer.Cli
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using Starkie.RaspieThermometer.Thermometers;
    using Starkie.RaspieThermometer.Thermometers.Contracts;
    using Starkie.RaspieThermometer.Thermometers.Implementations;

    class Program
    {
        // The default path for the sensors device in the Raspberry Pi.
        private const string DevicesDirectory = "/sys/bus/w1/devices/";

        static void Main(string[] args)
        {
            IEnumerable<IThermometer> thermometers = DS18B20ThermometerLoader.GetThermometers(DevicesDirectory);

            while (true)
            {
                foreach (IThermometer thermometer in thermometers)
                {
                    Console.WriteLine($"{thermometer.Id} - The current temperature is: {thermometer.Temperature:0.00}ºc");
                }

                Thread.Sleep(TimeSpan.FromSeconds(60));
            }
        }
    }
}