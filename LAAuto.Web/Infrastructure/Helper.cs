using LAAuto.Services;

namespace LAAuto.Web.Infrastructure
{
    public static class Helper
    {
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
