using System;
using Microsoft.AspNetCore.Mvc;
using NonStdQuery.Backend.Data.Queries;
using NonStdQuery.Backend.Representation.Data;
using NonStdQuery.Backend.Representation.Managers;

namespace NonStdQuery.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QueriesController : ControllerBase
    {
        private readonly ExecutionManager _executionManager;
        private readonly ExplanationManager _explanationManager;

        public QueriesController(ExecutionManager executionManager, ExplanationManager explanationManager)
        {
            _executionManager = executionManager;
            _explanationManager = explanationManager;
        }

        [HttpPost("execute")]
        public ActionResult<ExecutionResult> Execute(Query query)
        {
            try
            {
                return _executionManager.Execute(query);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("explain")]
        public ActionResult<ExplanationResult> Explain(Query query)
        {
            try
            {
                return _explanationManager.Explain(query);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
