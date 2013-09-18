﻿using InstaSharp.Models.Responses;
using PortableRest;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace InstaSharp.Endpoints
{
    public class Subscription
    {
        public enum Object
        {
            User,
            Tag,
            Location,
            Geography
        }

        public enum Aspect
        {
            Media
        }

        private readonly InstagramConfig config;
        private readonly RestClient client;

        public Subscription(InstagramConfig config)
        {
            this.config = config;
            client = new RestClient { BaseUrl = config.RealTimeAPI };
        }

        public Task<SubscriptionsResponse> Create(Object type, Aspect aspect)
        {
            // create a new guid that uniquely identifies this subscription request
            var verifyToken = Guid.NewGuid().ToString();
            var request = new RestRequest { Method = HttpMethod.Post };

            request.AddParameter("client_id", config.ClientId);
            request.AddParameter("client_secret", config.ClientSecret);
            request.AddParameter("object", type.ToString().ToLower());
            request.AddParameter("aspect", aspect.ToString().ToLower());
            request.AddParameter("verify_token", verifyToken);
            request.AddParameter("callback_url", config.CallbackURI);

            return client.ExecuteAsync<SubscriptionsResponse>(request);
        }
    }
}
