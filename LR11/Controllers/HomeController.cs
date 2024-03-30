using Microsoft.AspNetCore.Mvc;
using LR11.Filters;
using LR11.Models;

namespace LR11.Controllers
{
    [ServiceFilter(typeof(ActionLoggingFilter))]
    [ServiceFilter(typeof(UniqueUserFilter))]
    public class HomeController : Controller
    {
        private readonly string _logFilePath;

        public HomeController()
        {
            _logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Logs", "log.txt");
        }

        public IActionResult Index()
        {
            var uniqueUsersCount = GetCurrentUniqueUsersCount();
            ViewBag.UniqueUsersCount = uniqueUsersCount;

            var logEntries = GetLogEntries();
            ViewBag.LogEntries = logEntries;

            return View();
        }

        private List<ActionLogEntry> GetLogEntries()
        {
            List<ActionLogEntry> logEntries = new List<ActionLogEntry>();

            if (System.IO.File.Exists(_logFilePath))
            {
                logEntries = System.IO.File.ReadAllLines(_logFilePath)
                    .Select(line =>
                    {
                        var parts = line.Split('-');
                        return new ActionLogEntry
                        {
                            Time = DateTime.Parse(parts[0].Trim()),
                            MethodName = parts[1].Trim()
                        };
                    })
                    .ToList();
            }

            return logEntries;
        }

        private int GetCurrentUniqueUsersCount()
        {
            int uniqueUsersCount = 0;

            if (System.IO.File.Exists(_logFilePath))
            {
                var content = System.IO.File.ReadAllText(_logFilePath);
                if (!string.IsNullOrEmpty(content))
                {
                    int.TryParse(content, out uniqueUsersCount);
                }
            }

            return uniqueUsersCount;
        }
    }
}