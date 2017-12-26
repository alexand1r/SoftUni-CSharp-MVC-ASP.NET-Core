namespace CarDealer.Services.Contracts
{
    using CarDealer.Services.Models;
    using System.Collections.Generic;
    using CarDealer.Data.Models;
    using CarDealer.Services.Models.Cars;

    public interface ICarService
    {
        IEnumerable<CarModel> ByMake(string make);

        IEnumerable<CarWithPartsModel> WithParts();

        IEnumerable<CarModel> All();

        void Create(
            string make, 
            string model, 
            long travelledDistance,
            IEnumerable<int> parts);

        Car FindCar(int id);
    }
}
