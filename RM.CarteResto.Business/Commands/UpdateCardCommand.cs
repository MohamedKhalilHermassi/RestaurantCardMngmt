﻿using Abstraction;
using Model;

namespace RM.CarteResto.Business.Commands
{
    public class UpdateCardCommand
    {
        private readonly ICarteRestoRepository _carteRestoRepository;

        public UpdateCardCommand(ICarteRestoRepository carteRestoRepository)
        {
            _carteRestoRepository = carteRestoRepository;
        }
        public async Task ExecuteAsync(string partitionKey, CarteRestaurant card)
        {
            await _carteRestoRepository.UpdateCard(partitionKey, card); 
        }
    }
}