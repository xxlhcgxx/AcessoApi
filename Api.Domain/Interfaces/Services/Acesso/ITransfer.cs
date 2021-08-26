using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Services.Acesso
{
    public interface ITransfer
    {
        Task<TransferSearchEntity> GetTransfer(Guid id);

        Task<TransferLogEntity> Execute(TransferEntity transfer);

        Task<TransferLogEntity> Transaction(TransferEntity transfer);
    }
}
