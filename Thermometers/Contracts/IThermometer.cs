namespace Starkie.RaspieThermometer.Thermometers.Contracts
{
    /// <summary>
    ///     Represents a thermometer, a device that allows obtaining the current temperature from a room.
    /// </summary>
    public interface IThermometer
    {
        /// <summary>
        ///     Gets the identifier of the sensor.
        /// </summary>
        string Id { get; }

        /// <summary>
        ///     Gets the current temperature from the thermometer. The value is in celsius degrees (ºc).
        /// </summary>
        TemperatureMeasurement Temperature { get; }
    }
}