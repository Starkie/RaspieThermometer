namespace Starkie.RaspieThermometer.Thermometers.Contracts
{
    using System;

    /// <summary> Represents a temperature measurement from an <see cref="ITermometer"/>. </summary>
    public class TemperatureMeasurement
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="TemperatureMeasurement"/> class.
        /// </summary>
        /// <param name="sensorId"> The identifier of the sensor that registered the measurement. </param>
        /// <param name="dateTime"> The date and time of the measurement. </param>
        /// <param name="temperature"> The registered temperature at the time of the measurement. </param>
        public TemperatureMeasurement(string sensorId, DateTime dateTime, double temperature)
        {
            this.SensorId = sensorId;
            this.DateTime = dateTime;
            this.Temperature = temperature;
        }

        /// <summary>
        ///     Gets the identifier of the sensor that registered the measurement.
        /// </summary>
        /// <value> The identifier of the sensor that registered the measurement. </value>
        public string SensorId { get; }

        /// <summary>
        ///     Gets the date and time of the measurement.
        /// </summary>
        /// <value> The date and time of the measurement. </value>
        public DateTime DateTime { get; }

        /// <summary>
        ///     Gets the registered temperature at the time of the measurement.
        /// </summary>
        /// <value> The registered temperature at the time of the measurement. </value>
        public double Temperature { get; }
    }
}