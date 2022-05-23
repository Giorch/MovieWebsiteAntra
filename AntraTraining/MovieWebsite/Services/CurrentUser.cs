using System.Security.Claims;

namespace MovieWebsite.Services
{
    public class CurrentUser : ICurrentUser
    {
        //access httpcontext in a class that isn't a controller
        //inject httpcontext class in this regular class


        private readonly IHttpContextAccessor _contextAccessor;

        public CurrentUser(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public int? userId => Convert.ToInt32(_contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

        public bool IsAdmin => throw new NotImplementedException();

        public bool IsAuthenticated => _contextAccessor.HttpContext.User.Identity.IsAuthenticated;

        public string Email => _contextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;

        public string FullName => _contextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Surname).Value + " " + _contextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName).Value;

        public IEnumerator<string> Roles => throw new NotImplementedException();
    }
}
