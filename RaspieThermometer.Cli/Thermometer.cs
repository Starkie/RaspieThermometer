namespace Starkie.RaspieThermometer.Cli
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class Thermometer
    {
        private readonly string sensorReadingPath;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Thermometer"/> class.
        /// </summary>
        /// <param name="devicesPath"> The path that contains the thermometer devices. </param>
        public Thermometer(string devicesPath)
        {
            if (devicesPath is null)
            {
                throw new ArgumentNullException(nameof(devicesPath));
            }

            this.sensorReadingPath = GetThermometerFolder(devicesPath);

            if (this.sensorReadingPath is null)
            {
                throw new FileNotFoundException($"The given {nameof(devicesPath)} does not contain a compatible thermometer sensor.");
            }
        }

        private static string GetThermometerFolder(string devicesPath)
        {
            List<string> detectedThermometerSensors = Directory.EnumerateDirectories(devicesPath, "28-*").ToList();

            foreach (string sensorPath in detectedThermometerSensors)
            {
                string sensorReadingPath = $"{sensorPath}/w1_slave";

                if (!File.Exists(sensorReadingPath))
                {
                    continue;
                }

                return sensorReadingPath;
            }

            return null;
        }

        public double Temperature => this.GetTemperature();

        private double GetTemperature()
        {
            // Based on the python implementation by Kuman.
            string[] sensorReading = File.ReadAllLines(this.sensorReadingPath);
            string temperatureLine = sensorReading[1];
            temperatureLine.IndexOf("t=");

            int temperatureValue = int.Parse(temperatureLine.Substring(temperatureLine.IndexOf("t=") + 2));

            return temperatureValue / 1000.0;
        }
    }
}