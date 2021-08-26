using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.Acesso;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Service.Services
{
    public class AccountService : IAccount
    {
        private const string urlApi = "http://localhost:5000/api/Account/";

        public AccountEntity GetAccount(string account)
        {
            try
            {
                var client = new HttpClient();
                var jsonOptions = new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                };

                while (true)
                {
                    HttpResponseMessage response = client.GetAsync(urlApi + account).Result;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        response.EnsureSuccessStatusCode();
                        string content = response.Content.ReadAsStringAsync().Result;

                        AccountEntity resultado = JsonConvert.DeserializeObject<AccountEntity>(content);

                        return resultado;
                    }

                    return new AccountEntity();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<AccountEntity> ValidateTransferAsync(BalanceAdjustmentEntity account)
        {
            try
            {
                var client = new HttpClient();
                var jsonOptions = new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                };

                using (var request = new HttpRequestMessage(HttpMethod.Post, urlApi))
                {
                    var json = JsonConvert.SerializeObject(account);
                    using (var stringContent = new StringContent(json, Encoding.UTF8, "application/json"))
                    {
                        request.Content = stringContent;

                        using (var response = await client
                            .SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                            .ConfigureAwait(false))
                        {
                            response.EnsureSuccessStatusCode();
                            return new AccountEntity();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
