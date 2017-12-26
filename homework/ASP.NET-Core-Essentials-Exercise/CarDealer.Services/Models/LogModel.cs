namespace CarDealer.Services.Models
{
    using System;
    using CarDealer.Data.Models;

    public class LogModel
    {
        public string User { get; set; }

        public OperationType OperationType { get; set; }

        public string ModifiedTable { get; set; }

        public DateTime Time { get; set; }
    }
}
