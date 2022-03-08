using CartAPI.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CartTest
{
    public class CartApiTest
    {
        [Fact]
        public async void CreateTokenTest()
        {
            var application = new WebApplicationFactory<Program>();

            HttpClient client = application.CreateClient();

            HttpResponseMessage response = await GetToken(client);

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void GetCartTest()
        {
            var application = new WebApplicationFactory<Program>();

            HttpClient client = application.CreateClient();

            HttpResponseMessage tokenResponse = await GetToken(client);
            string tokenResponseString = await tokenResponse.Content.ReadAsStringAsync();
            Token tokenResult = JsonConvert.DeserializeObject<Token>(tokenResponseString);

            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Authorization
                         = new AuthenticationHeaderValue("Bearer", tokenResult.AccessToken);

            HttpResponseMessage cartResponse = await client.GetAsync("api/Cart/Get");
            string cartResponseString = await cartResponse.Content.ReadAsStringAsync();
            CartOutputModel cartResult = JsonConvert.DeserializeObject<CartOutputModel>(cartResponseString);

            cartResponse.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, cartResponse.StatusCode);
        }

        public static async Task<HttpResponseMessage> GetToken(HttpClient client)
        {
            var payload = "{\"UserName\": \"ciceksepeti\" ,\"Password\": \"ciceksepeti\"}";
            HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("api/Token/Create", content);
            return response;
        }
    }
}