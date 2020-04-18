using Microsoft.AspNetCore.Mvc;
using NonStdQuery.Backend.Representation.Data;
using NonStdQuery.Backend.Representation.Managers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NonStdQuery.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TooltipsController : ControllerBase
    {
        private readonly TooltipsManager _tooltipsManager;

        public TooltipsController(TooltipsManager tooltipsManager)
        {
            _tooltipsManager = tooltipsManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tooltip>>> Get()
        {
            return Ok(await _tooltipsManager.GetTooltips());
        }
    }
}
