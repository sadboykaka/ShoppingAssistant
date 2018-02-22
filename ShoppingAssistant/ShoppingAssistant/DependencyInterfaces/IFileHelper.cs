namespace ShoppingAssistant
{
    /// <summary>
    /// Interface for the FileHelper dependencies
    /// </summary>
    public interface IFileHelper
    {
        /// <summary>
        /// Method that returns a device specific file path for the file with the given name
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        string GetLocalFilePath(string filename);
    }
}
