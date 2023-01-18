using System.Text;
using DTOs;
using Newtonsoft.Json;

namespace Services;

public class AnbimaService
{
    const string ACCESS_TOKEN_POST = "oauth/access-token";
    const string FUNDOS_LISTA = "feed/fundos/v1/fundos";
    private readonly string _baseUrl;
    private readonly string _clientId;
    private readonly string _clientSecret;

    public AnbimaService(string baseUrl, string clientId, string clientSecret)
    {
        _baseUrl = baseUrl;
        _clientId = clientId;
        _clientSecret = clientSecret;
    }

    public async Task<AccessTokenResponse> Authenticate()
    {
        var url = "https://api.anbima.com.br/oauth/access-token";

        using(var client = new HttpClient())
        {
            var postObject = new StringContent(JsonConvert.SerializeObject(new { grant_type = "client_credentials"}), Encoding.UTF8, "application/json");

            client.DefaultRequestHeaders.Authorization 
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", GenerateAuthenticationHeaderValue(_clientId, _clientSecret));
            
            var result = await client.PostAsync(url, postObject);
            var content = await result.Content.ReadAsStringAsync();

            if(content is not null) return JsonConvert.DeserializeObject<AccessTokenResponse>(content);

            throw new Exception("login failed");
        }
    }

    public async Task<BaseResponse<List<FundoResponse>>> ConsultaFundos(string accessToken){
        string url = "https://api-sandbox.anbima.com.br/feed/fundos/v1/fundos";

        using(var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("client_id",_clientId);
            client.DefaultRequestHeaders.Add("access_token",accessToken);

            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            
            return JsonConvert.DeserializeObject<BaseResponse<List<FundoResponse>>>(content);
        }

        throw new Exception();
    }

    string GenerateAuthenticationHeaderValue(string clientId, string clientSecret)
    {
        if(string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(clientSecret))
            throw new NullReferenceException("necessario clientid/clientsecret");

        var textAsbytes =  Encoding.ASCII.GetBytes($"{clientId}:{clientSecret}");
        var base64 = System.Convert.ToBase64String(textAsbytes);    
        return base64;
    }
}