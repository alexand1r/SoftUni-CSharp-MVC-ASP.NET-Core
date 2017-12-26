namespace CarDealer.Services.Contracts
{
    using System;
    using System.Collections.Generic;
    using CarDealer.Data.Models;
    using CarDealer.Services.Models;

    public interface ILogService
    {
        IEnumerable<LogModel> All(string search, int count, int page);

        void Log(string user, OperationType type, string table, DateTime date);

        double Total(string search);

        void DeleteAll();
    }
}
