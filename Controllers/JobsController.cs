using System.Collections.Generic;
using jobsapi.Models;
using jobsapi.Services;
using Microsoft.AspNetCore.Mvc;

namespace jobsapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobsController : ControllerBase
    {
        private readonly JobsService _jservice;

        public JobsController(JobsService jservice)
        {
            _jservice = jservice;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Job>> GetAll()
        {
            try
            {
                return Ok(_jservice.GetAll());
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);                
            }
        }
    }
}