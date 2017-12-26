namespace CarDealer.Data.Models
{
    using System;

    public class Log
    {
        public int Id { get; set; }

        public OperationType OperationType { get; set; }

        public string TableName { get; set; }

        public DateTime Date { get; set; }

        public string User { get; set; }
    }
}
