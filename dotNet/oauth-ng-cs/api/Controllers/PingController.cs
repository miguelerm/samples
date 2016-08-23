using System.Web.Http;

namespace Api.Controllers
{
    public class PingController : ApiController
    {
        private string GetUserDisplayName()
        {
            return $"{User.GetEmailAddress()} - {User.GetGivenName()} {User.GetSurname()}";
        }

        public IHttpActionResult Get()
        {
            return Ok($"Pong [GET] ({GetUserDisplayName()})");
        }

        public IHttpActionResult Post()
        {
            return Ok($"Pong [POST] ({GetUserDisplayName()})");
        }

        public IHttpActionResult Put()
        {
            return Ok($"Pong [PUT] ({GetUserDisplayName()})");
        }

        public IHttpActionResult Patch()
        {
            return Ok($"Pong [PATCH] ({GetUserDisplayName()})");
        }

        public IHttpActionResult Delete()
        {
            return Ok($"Pong [DELETE] ({GetUserDisplayName()})");
        }
    }
}