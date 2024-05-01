namespace FavoriteHttp
{
    using FavoriteHttp.helpers;
    using FavoriteHttp.interfaces;
    using FavoriteHttp.models;
    using Newtonsoft.Json;
    using System.Text;

    public class FavoriteHttpClient<TResponse> : IFavoriteHttpClient<TResponse>
    {
        private HttpClient _client;
        private IFavoriteHttpConfig<TResponse> _config;
        private string _address;
        private Action<HttpResponseMessage, TResponse> _onSuccess;
        private Action<string> _onFailure;
        private object _body;
        public FavoriteHttpClient(string address)
        {
            _client = new HttpClient();
            _address = RequestHelper.FormatURL(address);
            _config = new FavoriteHttpConfig<TResponse>();
            _config.SetHttpClient(_client);
            _config.SetUrl(_address);

        }
        public IFavoriteHttpClient<TResponse> WithSettings(IFavoriteHttpConfig<TResponse> config)
        {
            _config = config;
            _config.SetHttpClient(_client);
            _config.SetUrl(_address);
            return this;
        }

        public IFavoriteHttpClient<TResponse> WithBody(object body)
        {
            _body = body;
            return this;
        }

        public IFavoriteHttpClient<TResponse> WithSuccess(Action<HttpResponseMessage, TResponse> onSuccess)
        {
            _onSuccess = onSuccess;
            return this;
        }

        public IFavoriteHttpClient<TResponse> WithFailure(Action<string> onFailure)
        {
            _onFailure = onFailure;
            return this;
        }
        public IFavoriteHttpClient<TResponse> WhenFinished()
        {
            return this;
        }
        public async Task<IFavoriteHttpClient<TResponse>> SendRequestAsync()
        {
            try
            {
                var body = JsonConvert.SerializeObject(_body);
                var request = new HttpRequestMessage()
                {
                    Method = new HttpMethod(RequestHelper.ToHttpMethod(_config.MethodType)),
                    RequestUri = _config.Address,
                    Content = new StringContent(body, Encoding.UTF8, RequestHelper.ToPayloadType(_config.PayloadType))
                };

                foreach (var header in _config.Headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }

                HttpResponseMessage response = await _client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    if(typeof(TResponse) == typeof(string))
                    {
                        TResponse result = (TResponse)Convert.ChangeType(responseData, typeof(TResponse));
                        _onSuccess?.Invoke(response, result);
                    }
                    else
                    {
                        TResponse model = JsonConvert.DeserializeObject<TResponse>(responseData);
                        _onSuccess?.Invoke(response, model);
                    }
                    
                }
                else
                {
                    _onFailure?.Invoke($"Request failed with status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _onFailure?.Invoke(ex.Message);
            }
            return this;
        }
    }
}