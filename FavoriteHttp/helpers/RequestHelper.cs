﻿namespace FavoriteHttp.helpers
{
    using FavoriteHttp.enums;
    using FavoriteHttp.models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class RequestHelper
    {
        public static string FormatURL(string url)
        {
            if (url.Contains("http://") || url.Contains("https://"))
            {
                return url;
            }
            return $"http://{url}";
        }
        public static string ToHttpMethod(MethodType methodType)
        {
            switch (methodType)
            {
                case MethodType.GET:
                    return "GET";
                case MethodType.POST:
                    return "POST";
                case MethodType.PUT:
                    return "PUT";
                case MethodType.DELETE:
                    return "DELETE";
                default:
                    return "GET";
            }
        }
        public static string ToPayloadType(PayloadType payloadType)
        {
            switch (payloadType)
            {
                case PayloadType.JSON:
                    return "application/json";
                case PayloadType.XML:
                    return "application/xml";
                case PayloadType.FORM:
                    return "text/plain";
                default:
                    return "application/json";
            }
        }
    }
}
