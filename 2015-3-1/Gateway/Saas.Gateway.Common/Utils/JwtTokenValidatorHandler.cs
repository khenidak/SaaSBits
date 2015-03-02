using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.IdentityModel.Protocols;
using System.IdentityModel.Selectors;


namespace Saas.Gateway.Common
{

    public class JwtTokenValidatorHandler 
    {
        
        //string authority = "https://login.windows.net/common/";
        string stsDiscoveryEndpoint = "https://login.windows.net/common/.well-known/openid-configuration";
        static string _issuer = string.Empty;
        static List<SecurityToken> _signingTokens = null;
        static DateTime _stsMetadataRetrievalTime = DateTime.MinValue;
        static string scopeClaimType = "http://schemas.microsoft.com/identity/claims/scope";


        public async Task<ClaimsPrincipal> Validate(string jwtToken, string audience)
        {
            string issuer;
            List<SecurityToken> signingTokens;

            try
            {
                // The issuer and signingTokens are cached for 24 hours. They are updated if any of the conditions in the if condition is true.            
                if (DateTime.UtcNow.Subtract(_stsMetadataRetrievalTime).TotalHours > 24
                    || string.IsNullOrEmpty(_issuer)
                    || _signingTokens == null)
                {
                    // Get tenant information that's used to validate incoming jwt tokens
                    ConfigurationManager<OpenIdConnectConfiguration> configManager = new ConfigurationManager<OpenIdConnectConfiguration>(stsDiscoveryEndpoint);
                    OpenIdConnectConfiguration config = await configManager.GetConfigurationAsync();
                    _issuer = config.Issuer;
                    _signingTokens = config.SigningTokens.ToList();

                    _stsMetadataRetrievalTime = DateTime.UtcNow;
                }

                issuer = _issuer;
                signingTokens = _signingTokens;
            }
            catch (Exception e)
            {
                throw new Exception("error getting authority metadata: " + e.Message);
            }

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            TokenValidationParameters validationParameters = new TokenValidationParameters
            {
                ValidAudience = audience,
                ValidIssuer = issuer,
                IssuerSigningTokens = signingTokens,
                CertificateValidator = X509CertificateValidator.None,
                ValidateIssuer = false
            };

            try
            {
                // Validate token.
                SecurityToken validatedToken = new JwtSecurityToken();
                ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(jwtToken, validationParameters, out validatedToken);

                // If the token is scoped, verify that required permission is set in the scope claim.
                if (claimsPrincipal.FindFirst(scopeClaimType) != null &&
                   claimsPrincipal.FindFirst(scopeClaimType).Value != "user_impersonation")
                    throw new Exception("user_impersonation is not included in the token");

                return claimsPrincipal;
            }
            catch (SecurityTokenValidationException ste)
            {
                throw new GatewayAuthException("invalid token:" + ste.Message);
            }
            catch (AggregateException ae)
            {
                throw new GatewayAuthException("invalid token:" + ae.Flatten().Message);

            }
            catch (Exception exp)
            {
                throw new GatewayAuthException("general error processing the authentication token: " + exp.Message);
            }
          

          
        }
    }
}
