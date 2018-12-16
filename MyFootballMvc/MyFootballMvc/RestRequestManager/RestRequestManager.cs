using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MyFootballMvc
{
    //NOT USED :(
    public class RestRequestManager
    {
        public static IRestResponse Execute(RestClient client, RestRequest request)
        {
            var response = client.Execute(request);

            if(response.StatusCode != HttpStatusCode.OK)
            {

            }

            return response;
        }
    }
}
