namespace Starkie.RaspieThermometer.Thermometers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Starkie.RaspieThermometer.Thermometers.Contracts;
    using Starkie.RaspieThermometer.Thermometers.Implementations;

    /// <summary> Class for the DS18B20 thermometer sensor. </summary>
    /// <remarks> Implementation based on the sensor working connected to a Raspberry PI. </remarks>
    public static class DS18B20ThermometerLoader
    {
        public static IEnumerable<IThermometer> GetThermometers(string devicesPath)
        {
            List<IThermometer> thermometers = new List<IThermometer>();

            // The 'DS18B20' sensor's readings appear in folders starting with the pattern '28-*device_id*.
            // For example: '28-3c01a816c730'.
            List<string> detectedThermometerSensors = Directory.EnumerateDirectories(devicesPath, "28-*").ToList();

            foreach (string sensorPath in detectedThermometerSensors)
            {
                string sensorId = Path.GetFileName(sensorPath);

                thermometers.Add(new DS18B20Thermometer(sensorId, devicesPath));
            }

            return thermometers;
        }
    }
}