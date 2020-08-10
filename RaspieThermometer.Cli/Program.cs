namespace Starkie.RaspieThermometer.Cli
{
    using System;
    using System.Threading.Tasks;

    class Program
    {
        // The default path for the sensors device in the Raspberry Pi.
        private const string DevicesDirectory = "/sys/bus/w1/devices/";

        static void Main(string[] args)
        {
            Thermometer thermometer = new Thermometer(DevicesDirectory);

            while (true)
            {
                Console.WriteLine($"The current temperature is: {thermometer.Temperature}ºc");
                Task.Delay(5000);
            };
        }
    }
}