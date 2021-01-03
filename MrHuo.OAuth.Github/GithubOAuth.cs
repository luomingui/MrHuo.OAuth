﻿using System;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace MrHuo.OAuth.Github
{
    public class GithubOAuth : OAuthApiBase<GithubAccessTokenModel, GithubUserModel>
    {
        private const string AUTHORIZE_URI = "https://github.com/login/oauth/authorize";
        private const string ACCESS_TOKEN_URI = "https://github.com/login/oauth/access_token";
        private const string USERINFO_URI = "https://api.github.com/user";
        private readonly string AppId;
        private readonly string AppKey;
        private readonly string RedirectUri;
        private readonly string Scope;

        public GithubOAuth(IConfiguration configuration, IHttpContextAccessor httpContextAccessor) 
            : base(configuration, httpContextAccessor)
        {
            AppId = _configuration["oauth:github:app_id"];
            AppKey = _configuration["oauth:github:app_key"];
            RedirectUri = _configuration["oauth:github:redirect_uri"];
            Scope = _configuration["oauth:github:scope"];
        }

        protected override string GetRedirectAuthorizeUrl(string state)
        {
            return $"{AUTHORIZE_URI}?client_id={AppId}&redirect_uri={RedirectUri}&state={state}&scope={Scope}";
        }

        protected override string GetAccessTokenUrl(string code, string state)
        {
            return $"{ACCESS_TOKEN_URI}?client_id={AppId}&client_secret={AppKey}&code={code}";
        }

        public override GithubUserModel GetUserInfo(GithubAccessTokenModel accessToken)
        {
            var json = API.Get(USERINFO_URI, new Dictionary<string, string>()
            {
                ["Authorization"] = $"token {accessToken.AccessToken}"
            });
            return JsonSerializer.Deserialize<GithubUserModel>(json);
        }
    }
}