using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using TweetSharp;

namespace AspNetIdentitySocialProfileImage.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
                var claims = claimsIdentity.Claims;

                var accessTokenClaim = claims.FirstOrDefault(x => x.Type == Startup.TwitterAccessTokenClaimType);
                var accessTokenSecretClaim =
                    claims.FirstOrDefault(x => x.Type == Startup.TwitterAccessTokenSecretClaimType);

                if (accessTokenClaim != null && accessTokenSecretClaim != null)
                {
                    var service = new TwitterService(
                        Startup.TwitterConsumerKey,
                        Startup.TwitterConsumerSecret,
                        accessTokenClaim.Value,
                        accessTokenSecretClaim.Value
                        );

                    ViewBag.Tweets = service.ListTweetsOnHomeTimeline(new ListTweetsOnHomeTimelineOptions());
                }
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}