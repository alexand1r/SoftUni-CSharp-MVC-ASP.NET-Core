namespace CameraBazaar.Web.Infrastructure.Filters
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class MeasureTimeAttribute : ActionFilterAttribute
    {
        private Stopwatch stopWatch;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            this.stopWatch = Stopwatch.StartNew();
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            this.stopWatch.Stop();

            using (var writer = new StreamWriter("action-times.txt", true))
            {
                var dateTime = DateTime.UtcNow;
                var controller = context.Controller.GetType().Name;
                var action = context.RouteData.Values["action"];
                var elapsedTime = this.stopWatch.Elapsed;

                var logMessage = $"{dateTime} - {controller}.{action} - {elapsedTime}";

                writer.WriteLine(logMessage);
            }
        }
    }
}
