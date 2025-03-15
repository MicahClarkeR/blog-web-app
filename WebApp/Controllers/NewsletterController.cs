using Microsoft.AspNetCore.Mvc;
using WebApp.Services;
using WebApp.Services.Directus;

namespace WebApp.Controllers
{
    public class NewsletterController : Controller
    {
        private readonly ILogger<NewsletterController> _logger;
        private readonly DirectusService _directusService;

        public NewsletterController(ILogger<NewsletterController> logger, DirectusService directusService)
        {
            _logger = logger;
            _directusService = directusService;
        }

        public async Task<IActionResult> Submit([FromQuery] string email)
        {
            var encryptedEmail = AESHelper.Encrypt(email);
            var subscriber = await _directusService.GetSubscriberByEmailAsync(email);
            var newEmail = AESHelper.Decrypt(subscriber.Email);

            return View();
        }
    }
}
