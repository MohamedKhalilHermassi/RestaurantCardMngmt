﻿using RM.CarteResto.Model.Entitiy;


namespace RM.CarteResto.Abstraction.Repositories
{
    public interface ICarteRestoRepository
    {
        Task<CarteRestaurant> AddCard(CarteRestaurant carteResto);
        Task<CarteRestaurant> GetCardByUserId(string UserId);
        Task<CarteRestaurant> GetCard(string partitionKey);
        Task<IEnumerable<CarteRestaurant>> GetAllCards();
        Task RemoveCard(string partitionkey);
        Task ChargeCard(string partitionkey, float montant, string IdTransaction);
        Task DischargeCard(string partitionkey, float montant, string IdTransaction);

        Task UpdateCard(string partitionkey, CarteRestaurant card);
        Task DecrementCardSolde(string partitionkey, float montant);
    }
}
