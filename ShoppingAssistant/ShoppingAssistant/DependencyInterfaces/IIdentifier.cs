namespace ShoppingAssistant
{
    /// <summary>
    /// Interface for the Identifier dependencies
    /// </summary>
    public interface IIdentifier
    {
        /// <summary>
        /// Method that returns a device specific identifier
        /// </summary>
        /// <returns></returns>
        string GetIdentifier();
    }
}
