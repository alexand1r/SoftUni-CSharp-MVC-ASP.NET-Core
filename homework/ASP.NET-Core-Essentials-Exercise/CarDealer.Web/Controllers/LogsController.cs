namespace CarDealer.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using CarDealer.Services.Contracts;
    using CarDealer.Services.Models;
    using CarDealer.Web.Models.Logs;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class LogsController : Controller
    {
        private readonly int PageSize = 3;
        private readonly ILogService logs;

        public LogsController(ILogService logs)
        {
            this.logs = logs;
        }

        [Authorize]
        public IActionResult All(string search, int page = 1)
        {
            var totalPages = this.logs.Total(search);

            return this.View(new LogsPaginationModel
            {
                Logs = this.logs.All(search, this.PageSize, page),
                CurrentPage = page,
                TotalPages = (int) Math.Ceiling(totalPages / (double) this.PageSize),
                Search = (string.IsNullOrEmpty(search) ? "" : search)
            });
        }

        [Authorize]
        public IActionResult Delete() => this.View();

        [Authorize]
        public IActionResult Destroy()
        {
            this.logs.DeleteAll();

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
