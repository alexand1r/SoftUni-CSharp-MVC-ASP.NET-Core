namespace CarDealer.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CarDealer.Data;
    using CarDealer.Data.Models;
    using CarDealer.Services.Contracts;
    using CarDealer.Services.Models;

    public class LogService : ILogService
    {
        private readonly CarDealerDbContext db;

        public LogService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<LogModel> All(string search, int count, int page = 1)
        {
            var logs = this.db.Logs
                .OrderByDescending(l => l.Id)
                .Skip((page - 1) * count)
                .Take(count)
                .Select(l => new LogModel
                {
                    User = l.User,
                    ModifiedTable = l.TableName,
                    OperationType = l.OperationType,
                    Time = l.Date
                })
                .ToList();

            if (!string.IsNullOrEmpty(search))
            {
                logs = logs.FindAll(l => l.User.Contains(search)).ToList();
            }

            return logs;
        }

        public void Log(string user, OperationType type, string table, DateTime date)
        {
            var log = new Log
            {
                User = user,
                OperationType = type,
                TableName = table,
                Date = date
            };

            this.db.Add(log);
            this.db.SaveChanges();
        }

        public double Total(string search)
        {
            if (string.IsNullOrEmpty(search))
                return this.db.Logs.Count();

            return this.db.Logs.Count(l => l.User.Contains(search));
        }

        public void DeleteAll()
        {
            var logs = this.db.Logs;

            foreach (var log in logs)
            {
                this.db.Logs.Remove(log);
            }

            this.db.SaveChanges();
        }
    }
}
