using MeuLivroDeReceitas.Comunication.Request;
using MeuLivroDeReceitas.Exceptions;
using Newtonsoft.Json;
using System.Globalization;
using System.Net;
using System.Text;
using System.Text.Json;
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

        protected async Task<HttpResponseMessage> PutRequest(string metodo, object body, string token = "")
        {
            AuthorizationRequest(token);

            var jsonString = JsonConvert.SerializeObject(body);

            return await _httpClient.PutAsync(metodo, new StringContent(jsonString, Encoding.UTF8, "application/json"));
        }

        protected async Task<string> Login(LoginRequestJson request)
        {

            var loginResponse = await PostRequest("login", request);

            await using var data = await loginResponse.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(data);

            return responseData.RootElement.GetProperty("token").GetString();
        }

        private void AuthorizationRequest(string token)
        {
            if(!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            }
        } 
    }
}
