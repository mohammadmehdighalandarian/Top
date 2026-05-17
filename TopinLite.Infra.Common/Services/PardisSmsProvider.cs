using Marvin.StreamExtensions;
using Microsoft.Extensions.Options;
using Polly;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using TopinLite.Domain.Configuration;
using TopinLite.Domain.Pardis;
using TopinLite.Domain.TopinApi;
using TopinLite.Services.Commons;

namespace TopinLite.Infra.Common.Services
{
    public class PardisSmsProvider : IPardisSmsProvider
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly PardisSmsConfigModel _pardisConfigs;

        public PardisSmsProvider(IHttpClientFactory httpClientFactory, IOptions<PardisSmsConfigModel> pardisConfig)
        {
            _httpClientFactory = httpClientFactory;
            _pardisConfigs = pardisConfig.Value;
        }

        private async Task<HttpContainer<PardisAuthResponseModel>> GetToken()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>
            {
                { "grant_type", "password" },
                { "client_id", "usr-mng" },
                { "username", _pardisConfigs.Username },
                { "password", _pardisConfigs.Password }
            };

            HttpClient httpClient = _httpClientFactory.CreateClient("PardisToken");

            try
            {
                using (HttpRequestMessage request = new(HttpMethod.Post, "/auth/realms/WebService/protocol/openid-connect/token"))
                {
                    request.Content = new FormUrlEncodedContent(dictionary);
                    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                    HttpResponseMessage response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);
                    response.EnsureSuccessStatusCode();
                    string createdContent = await response.Content.ReadAsStringAsync();
                    return new HttpContainer<PardisAuthResponseModel>
                    {
                        HttpResponse = JsonSerializer.Deserialize<PardisAuthResponseModel>(createdContent),
                        HttpStatusCode = 200
                    };
                }
            }
            catch (HttpRequestException Hex)
            {

                return new HttpContainer<PardisAuthResponseModel>
                {
                    HttpStatusCode = (int)Hex.StatusCode
                };
            }
            catch (Exception)
            {
                return new HttpContainer<PardisAuthResponseModel>
                {
                    HttpStatusCode = -9000404
                };
            }
        }

        public async Task<string> SendSms(string msisdn, string message)
        {
            try
            {
                HttpContainer<PardisAuthResponseModel> token = await GetToken();
                if (token.HttpStatusCode != 200)
                {
                    var tokenError = new ExecResult
                    {
                        ExecStatus = false,
                        ResultCode = token.HttpStatusCode,
                        ResultMessage = "Pardis authentication error."
                    };

                    return JsonSerializer.Serialize(tokenError);
                }

                var requestPayload = new PardisSingleSmsModel
                {
                    source = _pardisConfigs.Source.StartsWith("98") ? _pardisConfigs.Source : "98" + _pardisConfigs.Source,
                    destination = msisdn,
                    message = message
                };

                CancellationToken cancellationToken = CancellationToken.None;


                HttpClient httpClient = _httpClientFactory.CreateClient("Pardis");

                MemoryStream memoryStream = new MemoryStream();

                await memoryStream.SerializeToJsonAndWriteAsync(requestPayload, new UTF8Encoding(), 1024, true, cancellationToken);

                memoryStream.Seek(0, SeekOrigin.Begin);

                using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "/bulk-api/api/v2/send"))
                {
                    using (StreamContent streamContent = new StreamContent(memoryStream))
                    {
                        request.Content = streamContent;
                        request.Content.Headers.Add("Authorization", $"Bearer {token.HttpResponse.access_token}");
                        request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                        HttpResponseMessage response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken);

                        string createdContent = await response.Content.ReadAsStringAsync(cancellationToken);

                        Console.WriteLine($"Send response: {createdContent}");

                        response.EnsureSuccessStatusCode();


                        return createdContent;
                    }
                }


            }
            catch (HttpRequestException ex)
            {
                ExecResult execResponse;

                if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    execResponse = new ExecResult
                    {
                        ResultCode = -9000401,
                        ResultMessage = "Authentication Error : The Username Or Password Is Incorrect"
                    };
                }
                else
                {
                    execResponse = new ExecResult
                    {

                        ResultCode = -9000402,
                        ResultMessage = $"API Error : Http Status Code:{ex.StatusCode} , Message:{ex.Message}"
                    };

                }

                return JsonSerializer.Serialize(execResponse);
            }
            catch (Exception ex)
            {
                return JsonSerializer.Serialize(new ExecResult
                {
                    ResultCode = -9000403,
                    ResultMessage = $"General Exception. Message: {ex.Message}"
                });

            }
        }
    }
}
