using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bhg.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bhg.Controllers
{
    [Route("/")]
    [ApiController]
    public class RootController : ControllerBase
    {
        [HttpGet(Name = nameof(GetRoot))]
        [ProducesResponseType(200)]
        public IActionResult GetRoot()
        {
            var response = new RootResponse
            {
                Self = Link.To(nameof(GetRoot)),
                TreasureMaps = Link.To(nameof(TreasureMapsController.Get)),  // Link.ToCollection(nameof(TreasureMapsController.GetAllTreasureMaps)),
                Info = Link.To(nameof(InfoController.GetInfo))
            };

            return Ok(response);
        }
    }
}