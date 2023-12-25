using Microsoft.AspNetCore.Mvc;

namespace Cookie.Controllers
{
    [Route("Cookies")]
    public class CookiesController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.CookieValue = Request.Cookies["CustomCookie"];
            return View("Index");
        }

        [HttpPost("SetCookies")]
        public IActionResult SetCookies(string value, DateTime expiration)
        {
            CookieOptions option = new CookieOptions
            {
                Expires = expiration
            };

            Response.Cookies.Append("CustomCookie", value, option);

            return RedirectToAction("Index");
        }
    }
}