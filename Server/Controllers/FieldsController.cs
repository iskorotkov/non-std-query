using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NonStdQuery.Backend.Representation.Managers;

namespace NonStdQuery.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FieldsController : ControllerBase
    {
        private readonly FieldsManager _fieldsManager;

        public FieldsController(FieldsManager fieldsManager)
        {
            _fieldsManager = fieldsManager;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Index()
        {
            return Ok(_fieldsManager.GetFriendlyFields());
        }
    }
}
