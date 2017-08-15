using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WebAppDataLayer.Repository.ApiRepository.ApiRepositoryExceptions;

namespace WebAppDataLayer.Repository.ApiRepository
{
    public abstract class ApiRepository<T, TU> : IRepository<T, TU> where TU : IComparable where T:class
    {
        private readonly HttpClient Client;

        public ApiRepository(TokenAccessor tokenAccessor, IUriBuilder<T> uriBuilder)
        {
            if (uriBuilder == null) throw new ArgumentNullException(nameof(uriBuilder));
            if (tokenAccessor == null) throw new ArgumentNullException(nameof(tokenAccessor));

            Client = new HttpClient()
            {
                BaseAddress = uriBuilder.GetBaseUri()
            };
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", tokenAccessor.Token);
        }

        public async Task<T> AddAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var result = await Client.PostAsync("", content);
            return await ValidateResponse<T>(result);
        }

        public async Task DeleteAsync(TU id)
        {
            var result = await Client.DeleteAsync(id.ToString());
            if (!result.IsSuccessStatusCode) throw new ApiRepositoryException();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var result = await Client.GetAsync("");
            return await ValidateResponse<IEnumerable<T>>(result);
        }

        public async Task<T> GetAsync(TU id)
        {
            var result = await Client.GetAsync(id.ToString());
            return await ValidateResponse<T>(result);
        }

        public async Task UpdateAsync(T entity)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var result = await Client.PutAsync("", content);
            if (!result.IsSuccessStatusCode)
            {
                throw new UnexpectedServerResponse(result.StatusCode);
            }
        }

        private async Task<U> ValidateResponse<U>(HttpResponseMessage response) where U:class
        {
            if (response == null) throw new ArgumentNullException(nameof(response));

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<U>(await response.Content.ReadAsStringAsync());
            }
            else if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                throw new ForbiddenException();
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                return null;
            }
            else if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new ServerErrorException();
            }

            throw new UnexpectedServerResponse(response.StatusCode);
        }
    }
}
