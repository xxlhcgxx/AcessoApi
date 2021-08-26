using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Services.Acesso
{
    public interface IAccount
    {
        AccountEntity GetAccount(string account);

        Task<AccountEntity> ValidateTransferAsync(BalanceAdjustmentEntity account);
    }
}
