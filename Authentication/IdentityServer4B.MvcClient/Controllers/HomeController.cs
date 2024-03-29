﻿using IdentityModel.Client;
using IdentityServer4B.MvcClient.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using IdentityServer4B.Shared;

namespace IdentityServer4B.MvcClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login([FromQuery] string returnUri)
        {
            var authProps = new AuthenticationProperties()
            {
                RedirectUri = returnUri
            };
            return Challenge(authProps, Constants.OpenIdConnect);
        }

        public IActionResult Logout()
        {
            // 모든 인증스킴에 대해 로그아웃
            return SignOut(Constants.Cookie, Constants.OpenIdConnect);
        }

        [Authorize]
        public async Task<IActionResult> Secret()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var idToken = await HttpContext.GetTokenAsync("id_token");
            var refreshToken = await HttpContext.GetTokenAsync("refresh_token");

            var claims = User.Claims.ToList();
            var _accessToken = new JwtSecurityTokenHandler().ReadJwtToken(accessToken);
            var _idToken = new JwtSecurityTokenHandler().ReadJwtToken(idToken);

            var result = await GetSecretFromApiOne(accessToken);

            await RefreshAccessToken();

            return View(model: result);
        }


        public async Task<string> GetSecretFromApiOne(string accessToken)
        {
            var apiClient = _httpClientFactory.CreateClient();

            apiClient.SetBearerToken(accessToken);

            var response = await apiClient.GetAsync(Constants.ApiOneAddress +"/secret");

            var content = await response.Content.ReadAsStringAsync();

            return content;
        }

        private async Task RefreshAccessToken()
        {
            var serverClient = _httpClientFactory.CreateClient();
            var discoveryDocument = await serverClient.GetDiscoveryDocumentAsync(Constants.ServerAddress);

            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var idToken = await HttpContext.GetTokenAsync("id_token");
            var refreshToken = await HttpContext.GetTokenAsync("refresh_token");
            var refreshTokenClient = _httpClientFactory.CreateClient();

            var tokenResponse = await refreshTokenClient.RequestRefreshTokenAsync(
                new RefreshTokenRequest
                {
                    Address = discoveryDocument.TokenEndpoint,
                    RefreshToken = refreshToken,
                    ClientId = Constants.Client_2_Id,
                    ClientSecret = Constants.Client_2_Secret
                });

            var authInfo = await HttpContext.AuthenticateAsync(Constants.Cookie);

            authInfo.Properties.UpdateTokenValue("access_token", tokenResponse.AccessToken);
            authInfo.Properties.UpdateTokenValue("id_token", tokenResponse.IdentityToken);
            authInfo.Properties.UpdateTokenValue("refresh_token", tokenResponse.RefreshToken);

            await HttpContext.SignInAsync(Constants.Cookie, authInfo.Principal, authInfo.Properties);
        }
    }
}