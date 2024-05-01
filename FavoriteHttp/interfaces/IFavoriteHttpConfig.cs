namespace FavoriteHttp.interfaces
{
    using FavoriteHttp.enums;
    using FavoriteHttp.models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IFavoriteHttpConfig<TResponse>
    {
        Dictionary<string, string> Headers { get; }
        MethodType MethodType { get; }
        PayloadType PayloadType { get; }
        Uri Address { get; }
        Type ResponseType { get; set; }
        IFavoriteHttpConfig<TResponse> SetResponseType<T>();
        IFavoriteHttpConfig<TResponse> SetUrl(string url);
        IFavoriteHttpConfig<TResponse> SetHttpClient(HttpClient httpClient);
        IFavoriteHttpConfig<TResponse> WithMethodType(MethodType methodType);
        IFavoriteHttpConfig<TResponse> WithPayloadType(PayloadType payloadType);
        IFavoriteHttpConfig<TResponse> WithHeader(string key, string value);
        IFavoriteHttpConfig<TResponse> WithHeaders(Dictionary<string, string> headers);

    }
}
