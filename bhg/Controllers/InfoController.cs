using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bhg.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace bhg.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class InfoController : ControllerBase
    {
        private readonly AppInfo _appInfo;

        public InfoController(IOptions<AppInfo> appInfoWrapper)
        {
            _appInfo = appInfoWrapper.Value;
        }

        [HttpGet(Name = nameof(GetInfo))]
        [ProducesResponseType(200)]
        public ActionResult<AppInfo> GetInfo()
        {
            _appInfo.Href = Url.Link(nameof(GetInfo), null);
            return _appInfo;
        }
    }
}