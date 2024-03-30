using Microsoft.AspNetCore.Mvc.Filters;

namespace LR11.Filters
{
    public class UniqueUserFilter : IActionFilter
    {
        private readonly string _logFilePath;
        private readonly HashSet<string> _uniqueUsers = new HashSet<string>();

        public UniqueUserFilter(string logFilePath)
        {
            _logFilePath = logFilePath;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var userId = context.HttpContext.User.Identity.Name;
            if (!string.IsNullOrEmpty(userId))
            {
                _uniqueUsers.Add(userId);
                var logMessage = $"Unique users count: {_uniqueUsers.Count}";
                File.WriteAllText(_logFilePath, logMessage);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}