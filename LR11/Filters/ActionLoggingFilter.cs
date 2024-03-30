using Microsoft.AspNetCore.Mvc.Filters;

namespace LR11.Filters
{
    public class ActionLoggingFilter : IActionFilter
    {
        private readonly string _logFilePath;

        public ActionLoggingFilter(string logFilePath)
        {
            _logFilePath = logFilePath;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var actionName = context.ActionDescriptor.DisplayName;
            var logMessage = $"{DateTime.Now} - Action '{actionName}' started.";
            File.AppendAllText(_logFilePath, logMessage + Environment.NewLine);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var actionName = context.ActionDescriptor.DisplayName;
            var logMessage = $"{DateTime.Now} - Action '{actionName}' completed.";
            File.AppendAllText(_logFilePath, logMessage + Environment.NewLine);
        }
    }
}