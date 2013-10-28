using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.Twitter;
using Owin;

namespace AspNetIdentitySocialProfileImage
{
    public partial class Startup
    {
        public const string TwitterConsumerKey = "your consumer key here";
        public const string TwitterConsumerSecret = "your consumer secret here";

        public const string TwitterAccessTokenClaimType = "urn:tokens:twitter:accesstoken";
        public const string TwitterAccessTokenSecretClaimType = "urn:tokens:twitter:accesstokensecret";

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
            // Use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            app.UseTwitterAuthentication(
                new TwitterAuthenticationOptions()
                {
                    ConsumerKey = TwitterConsumerKey,
                    ConsumerSecret = TwitterConsumerSecret,
                    Provider = new TwitterAuthenticationProvider()
                    {
                        OnAuthenticated = async context =>
                        {
                            context.Identity.AddClaim(new Claim(TwitterAccessTokenClaimType, context.AccessToken));
                            context.Identity.AddClaim(new Claim(TwitterAccessTokenSecretClaimType, context.AccessTokenSecret));
                        }
                    }
                }
               );
        }
    }
}