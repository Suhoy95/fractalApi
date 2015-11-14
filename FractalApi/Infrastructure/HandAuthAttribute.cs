using Domain.DbProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace FractalApi.Infrastructure
{
    public class HandAuthAttribute : Attribute, IAuthenticationFilter
    {
        //http://www.asp.net/web-api/overview/security/authentication-filters
        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            HttpRequestMessage request = context.Request;
            AuthenticationHeaderValue authorization = request.Headers.Authorization;

            if (authorization == null)
            {
                return;
            }

            if (authorization.Scheme != "Basic")
            {
                return;
            }

            if (String.IsNullOrEmpty(authorization.Parameter))
            {
                return;
            }

            Tuple<string, string> userNameAndPasword = ExtractUserNameAndToken(authorization.Parameter);
            if (userNameAndPasword == null)
            {
                context.ErrorResult = new AuthenticationFailureResult("Invalid credentials", request);
                return;
            }

            string userName = userNameAndPasword.Item1;
            string token = userNameAndPasword.Item2;

            IPrincipal principal = await AuthenticateAsync(userName, token, cancellationToken);
            if (principal == null)
            {
                context.ErrorResult = new AuthenticationFailureResult("Invalid username or password", request);
            }
            else
            {
                context.Principal = principal;
            }

        }

        private async Task<IPrincipal> AuthenticateAsync(string userName, string token, CancellationToken cancellationToken)
        {
            var db = new UserRepository();
            var role = await db.GetRoleAsync(userName, token);

            if (role == null)
                return null;

            var identity = new GenericIdentity(userName);
            return new GenericPrincipal(identity, new string[] { role });
        }

        private Tuple<string, string> ExtractUserNameAndToken(string p)
        {
            var res = p.Split(':').Where((s) => !String.IsNullOrWhiteSpace(s)).ToList();

            if (res.Count() == 2)
                return Tuple.Create(res[0], res[1]);

            return null;
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        public bool AllowMultiple
        {
            get { return false; }
        }
    }
}