using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;
using CERNA_T3.Models;

namespace CERNA_T3.Repository
{
    public interface IClaimService
    {
        Usuario GetLoggedUser();
        void SetHttpContext(HttpContext httpContext);
        void Logout();
        void Login(ClaimsPrincipal principal);
    }
    public class ClaimService : IClaimService
    {
        private readonly Configuracion context;
        private HttpContext http;

        public ClaimService(Configuracion context)
        {
            this.context = context;
        }

        public Usuario GetLoggedUser()
        {
            var claim = http.User.Claims.FirstOrDefault();
            var user = context.Usuarios.Where(o => o.Username == claim.Value).FirstOrDefault();
            return user;
        }

        public void Login(ClaimsPrincipal principal)
        {
            http.SignInAsync(principal);
        }

        public void Logout()
        {
            http.SignOutAsync();
        }

        public void SetHttpContext(HttpContext http)
        {
            this.http = http;
        }
    }
}
