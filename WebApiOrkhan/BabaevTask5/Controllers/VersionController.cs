using BabaevTask5.Controllers.Models;
using BabaevTask5.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BabaevTask5.Controllers
{
    [Route("/Version")]
    public class VersionController : Controller
    {
        private readonly IVersion _iVersion;
        public IActionResult UploadVersion() => View();

        public VersionController(IVersion iVersion)
        {
            _iVersion = iVersion;
        }

        [HttpPost]
        public IActionResult UploadVersion(FormForVersion formForVersion)
        {
            var verId = _iVersion.UploadNewVersionOfMaterial(formForVersion);
            return RedirectToAction("UploadVersion");
        }
        
        
        
    }
}