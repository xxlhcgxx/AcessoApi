using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Services.Acesso;
using System;
using System.Threading.Tasks;

namespace Api.Service.Services
{
    public class TransferService : ITransfer
    {
        private IRepository<TransferEntity> _repositoryTransfer;
        private readonly AccountService _accountServices = new AccountService();

        private const string search = "Search";
        private const string searchTransfer = "Transfer search!";
        private const string error = "Error";
        private const string searchTransferError = "Transfer not search!";
        private const string soucerAccounNotFound = "Source account not found!";
        private const string insufficienteBalance = "Insufficient balance!";
        private const string DestinationAccountNotFound = "Destination account not found!";
        private const string confirmed = "Confirmed";

        public TransferService(
            IRepository<TransferEntity> repositoryTransfer)
        {
            _repositoryTransfer = repositoryTransfer;
        }

        public async Task<TransferSearchEntity> GetTransfer(Guid id)
        {
            try
            {
                var logSearch = new TransferSearchEntity();

                var transfer = await _repositoryTransfer.SelecAsync(id);
                if (transfer != null)
                {
                    logSearch.Status = transfer.Status;
                    logSearch.Message = transfer.Message;

                    transfer.Id = new Guid();
                    transfer.Status = search;
                    transfer.Message = searchTransfer;
                    await LogTransaction(transfer);
                }
                else
                {
                    var transferLog = new TransferEntity()
                    {
                        Status = error,
                        Message = searchTransferError,
                        AccountDestination = "",
                        AccountOrigin = "",
                        Value = 0
                    };
                    await LogTransaction(transferLog);

                    logSearch.Status = transferLog.Status;
                    logSearch.Message = transferLog.Message;
                }

                return logSearch;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TransferLogEntity> Transaction(TransferEntity transfer)
        {
            try
            {
                //Busca conta de origem
                var accountOrigin = _accountServices.GetAccount(transfer.AccountOrigin);

                //Conta origin não encontrada
                if (accountOrigin.id == 0)
                {
                    transfer.Status = error;
                    transfer.Message = soucerAccounNotFound;
                    return (await LogTransaction(transfer));
                }

                //Saldo menor do que valor a ser transferido
                if (accountOrigin.balance < transfer.Value)
                {
                    transfer.Status = error;
                    transfer.Message = insufficienteBalance;
                    return (await LogTransaction(transfer));
                }

                //Busca conta de destino
                var accountDestination = _accountServices.GetAccount(transfer.AccountDestination);

                if (accountDestination.id == 0)
                {
                    transfer.Status = error;
                    transfer.Message = DestinationAccountNotFound;
                    return (await LogTransaction(transfer));
                }

                //Realizar transferência
                return (await Execute(transfer));
            }
            catch (Exception ex)
            {
                transfer.Status = error;
                transfer.Message = ex.InnerException.ToString();
                return (await LogTransaction(transfer));
            }
        }

        public async Task<TransferLogEntity> Execute(TransferEntity transfer)
        {
            try
            {
                //Transferencia de debito
                var countOrigin = new BalanceAdjustmentEntity()
                {
                    accountNumber = transfer.AccountOrigin,
                    value = transfer.Value,
                    type = "Debit"
                };
                var debit = _accountServices.ValidateTransferAsync(countOrigin);

                //Transferencia de credito
                var countDestination = new BalanceAdjustmentEntity()
                {
                    accountNumber = transfer.AccountDestination,
                    value = transfer.Value,
                    type = "Credit"
                };
                var credit = _accountServices.ValidateTransferAsync(countDestination);

                //Salva transação
                transfer.Status = confirmed;
                transfer.Message = "";

                return (await LogTransaction(transfer));
            }
            catch (Exception ex)
            {
                transfer.Status = error;
                transfer.Message = ex.InnerException.ToString();
                return (await LogTransaction(transfer));
            }
        }

        public async Task<TransferLogEntity> LogTransaction(TransferEntity transfer)
        {
            try
            {
                var logReturn = new TransferLogEntity();
                var logTransact = await _repositoryTransfer.InsertAsync(transfer);
                if (logTransact != null)
                {
                    logReturn.transactionId = logTransact.Id;
                }

                return logReturn;
            }
            catch (Exception ex)
            {
                transfer.Status = error;
                transfer.Message = ex.InnerException.ToString();
                return (await LogTransaction(transfer));
            }
        }
    }
}