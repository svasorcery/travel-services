﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace HermesSpa.Controllers.RailAPI
{
    using HermesSpa.Services;

    [Authorize]
    [Route("api/rail/[controller]")]
    public class StationsController : Controller
    {
        private readonly RailKaolinApiClient _rail;

        public StationsController(RailKaolinApiClient railClient)
        {
            _rail = railClient;
        }


        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]string term)
        {
            return Ok(await _rail.SearchStationsAsync(term));
        }
    }
}
