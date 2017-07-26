using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebAppTest20170719
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 設定和服務

            // Web API 路由
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{name}", //新增一個{name}作為傳入參數，代表URI的完整結構
                defaults: new { name = RouteParameter.Optional } //設定name為一個Routing傳入的參數
            );
        }
    }
}
