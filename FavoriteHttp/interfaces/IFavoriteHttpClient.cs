namespace FavoriteHttp.interfaces
{
    using FavoriteHttp.interfaces;
    using FavoriteHttp.models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;

    public interface IFavoriteHttpClient<TResponse>
    {
        IFavoriteHttpClient<TResponse> WithSettings(IFavoriteHttpConfig<TResponse> config);
        IFavoriteHttpClient<TResponse> WithBody(object body);
        IFavoriteHttpClient<TResponse> WithSuccess(Action<HttpResponseMessage, TResponse> onSuccess);
        IFavoriteHttpClient<TResponse> WithFailure(Action<string> onFailure);
        Task<IFavoriteHttpClient<TResponse>> SendRequestAsync();
    }
}
