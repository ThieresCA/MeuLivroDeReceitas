using MeuLivroDeReceitas.Exceptions;
using Newtonsoft.Json;
using System.Globalization;
using System.Text;
using Xunit;

namespace WebApi.Test.V1.User
{
    public class ControllerBase : IClassFixture<MeuLivroDeReceitasWebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;

        public ControllerBase(MeuLivroDeReceitasWebApplicationFactory<Program> factory)
        {
            _httpClient = factory.CreateClient();
            ResourceErrorMessage.Culture = CultureInfo.CurrentCulture;
        }

        protected async Task<HttpResponseMessage> PostRequest(string metodo, object body)
        {
            var jsonString = JsonConvert.SerializeObject(body);

            return await _httpClient.PostAsync(metodo, new StringContent(jsonString, Encoding.UTF8, "application/json"));
        }
    }
}
