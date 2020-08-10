using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Starkie.RaspieThermometer.Cli
{
    public class Thermometer
    {
        private const string deviceDirectory = "/sys/bus/w1/devices/";

        public double Temperature => this.GetTemperature();

        private double GetTemperature()
        {
            // Based on the python implementation by Kuman.
            List<string> detectedThermometerSensors = Directory.EnumerateDirectories(dir, "28-*").ToList();

            foreach (string sensorPath in detectedThermometerSensors)
            {
                string sensorReadingPath = $"{sensorPath}/w1_slave";

                if (!File.Exists(sensorReadingPath))
                {
                    continue;
                }

                string[] sensorReading = File.ReadAllLines(sensorReadingPath);
                string temperatureLine = sensorReading[1];
                temperatureLine.IndexOf("t=");

                int temperatureValue = int.Parse(temperatureLine.Substring(temperatureLine.IndexOf("t=") + 2));

                return temperatureValue / 1000.0;
            }

            return 0;
        }
    }
}