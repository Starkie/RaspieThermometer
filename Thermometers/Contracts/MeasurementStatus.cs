namespace Starkie.RaspieThermometer.Thermometers.Contracts
{
    /// <summary> Represents whether a measurement could be made successfully. </summary>
    public enum MeasurementStatus
    {
        /// <summary> The measurement could be made successfully. </summary>
        Ok = 0,

        /// <summary> The measurement failed.  </summary>
        Failed = 1,
    }
}