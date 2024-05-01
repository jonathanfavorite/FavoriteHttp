namespace FavoriteHttp.CORE.models
{
    using FavoriteHttp.CORE.enums;
    using FavoriteHttp.CORE.helpers;
    using FavoriteHttp.CORE.interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection.PortableExecutable;
    using System.Text;
    using System.Threading.Tasks;

    public class FavoriteHttpConfig<TResponse> : IFavoriteHttpConfig<TResponse>
    {
        private HttpClient _client;
        public Dictionary<string, string> Headers { get; private set; } = new Dictionary<string, string>();
        public MethodType MethodType { get; private set; } = MethodType.GET;
        public PayloadType PayloadType { get; private set; } = PayloadType.JSON;
        public Uri Address { get; private set; }
        public Type ResponseType { get; set; }
        public IFavoriteHttpConfig<TResponse> SetHttpClient(HttpClient httpClient)
        {
            _client = httpClient;
            return this;
        }
        public IFavoriteHttpConfig<TResponse> SetResponseType<T>()
        {
            ResponseType = typeof(T);
            return this;
        }

        public IFavoriteHttpConfig<TResponse> SetUrl(string url)
        {
            Address = new Uri(url);
            return this;
        }

        public IFavoriteHttpConfig<TResponse> WithHeader(string key, string value)
        {
            Headers[key] = value;
            return this;
        }
        public IFavoriteHttpConfig<TResponse> WithHeaders(Dictionary<string, string> headers)
        {
            foreach(KeyValuePair<string, string> h in headers)
            {
                WithHeader(h.Key, h.Value);
            }
            return this;
        }
        public IFavoriteHttpConfig<TResponse> WithMethodType(MethodType methodType)
        {
            MethodType = methodType;
            return this;
        }

        public IFavoriteHttpConfig<TResponse> WithPayloadType(PayloadType payloadType)
        {
            PayloadType = payloadType;
            return this;
        }
    }
}
