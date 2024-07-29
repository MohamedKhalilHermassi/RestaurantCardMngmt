﻿using Abstraction;
using Model;


namespace Business
{
    public class UpdateTransactionCommand
    {
        private readonly ITransactionRepository _transactionRepository;

        public UpdateTransactionCommand(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task ExecuteAsync(string partitionKey,Transactions transaction)
        {

            await _transactionRepository.UpdateTransaction(partitionKey,transaction);
        }
    }

}
