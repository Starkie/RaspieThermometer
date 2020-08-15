namespace Starkie.RaspieThermometer.Thermometers.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Starkie.RaspieThermometer.Thermometers.Contracts;

    /// <summary> Class for the DS18B20 thermometer sensor. </summary>
    /// <remarks> Implementation based on the sensor working connected to a Raspberry PI. </remarks>
    public class DS18B20Thermometer : IThermometer
    {
        private readonly string sensorReadingPath;

        /// <summary>
        ///     Initializes a new instance of the <see cref="DS18B20Thermometer"/> class.
        /// </summary>
        /// <param name="devicesPath"> The path that contains the thermometer devices. </param>
        public DS18B20Thermometer(string id, string devicesPath)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            this.Id = id;

            if (string.IsNullOrEmpty(devicesPath))
            {
                throw new ArgumentNullException(nameof(devicesPath));
            }

            this.sensorReadingPath = GetThermometerReadingFile(id, devicesPath);

            if (string.IsNullOrEmpty(this.sensorReadingPath))
            {
                throw new FileNotFoundException($"The given {nameof(devicesPath)} does not contain a compatible thermometer sensor.");
            }
        }

        /// <inheritdoc/>
        public TemperatureMeasurement Temperature => this.GetTemperature();

        /// <inheritdoc/>
        public string Id { get; }

        /// <summary>
        ///     Gets the thermometer's reading file, from where the temperatures will be read.
        /// </summary>
        /// <param name="devicesPath"> The devices path, where the thermometer reading folder is located. </param>
        /// <returns>
        ///     If the file exists in the given folder, the path to the reading's file.
        ///     Otherwise, returns null.
        /// </returns>
        private static string GetThermometerReadingFile(string id, string devicesPath)
        {
            // The actual readings are in the 'w1_slave' file.
            string sensorReadingPath = Path.Combine(devicesPath, id, "w1_slave");

            if (!File.Exists(sensorReadingPath))
            {
                return null;
            }

            return sensorReadingPath;
        }

        /// <summary>
        ///     Reads the current temperature from the thermometer. The value is in celsius degrees (ºc).
        /// </summary>
        /// <returns> The current temperature. </returns>
        private TemperatureMeasurement GetTemperature()
        {
            // Based on the python implementation by Kuman.
            string[] sensorReading = File.ReadAllLines(this.sensorReadingPath);

            if (sensorReading.Length < 2)
            {
                return new TemperatureMeasurement(this.Id, MeasurementStatus.Failed, DateTime.Now, default);
            }

            string temperatureLine = sensorReading[1];
            string temperatureReading = temperatureLine.Substring(temperatureLine.IndexOf("t=") + 2);

            bool temperatureParsed = int.TryParse(temperatureReading, out int temperatureValue);

            if (!temperatureParsed)
            {
                return new TemperatureMeasurement(this.Id, MeasurementStatus.Failed, DateTime.Now, default);
            }

            return new TemperatureMeasurement(this.Id, MeasurementStatus.Ok, DateTime.Now, temperatureValue / 1000.0);
        }
    }
}