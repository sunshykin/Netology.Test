using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Netology.Test.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TestController : ControllerBase
	{
		public const string SessionKeyName = "_Name";

		public TestController()
		{
			
		}

		[HttpGet("")]
		public IActionResult Get()
		{
			if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKeyName)))
			{
				HttpContext.Session.SetString(SessionKeyName, "Guest");
			}

			// Some work

			var name = HttpContext.Session.GetString(SessionKeyName);

			return Ok("Hello, " + name);
		}

		[HttpPost("")]
		public IActionResult SetName([FromBody] string name)
		{
			HttpContext.Session.SetString(SessionKeyName, name);

			return Ok();
		}
	}
}
