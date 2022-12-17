using LAAuto.Services;

namespace LAAuto.Web.Infrastructure
{
    /// <summary>
    /// Represents a helper class containing helpful methods. 
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// Processes the exception.
        /// </summary>
        /// <param name="ex">the exception</param>
        /// <returns>the status code</returns>
        public static int ProcessException(Exception ex)
        {
            return ex switch
            {
                ObjectNotFoundException => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError,
            };
        }
    }
}
