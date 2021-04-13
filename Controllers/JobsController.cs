using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using jobsapi.Models;
using jobsapi.Services;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("{id}")]
        public ActionResult<Job> GetOne(int id)
        {
            try
            {
                return Ok(_jservice.GetOne(id));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Job>> CreateOne([FromBody] Job newJob)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                newJob.CreatorId = userInfo.Id;
                newJob.Creator = userInfo;
                return Ok(_jservice.CreateOne(newJob));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Job>> EditOne(int id, [FromBody] Job editJob)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                editJob.CreatorId = userInfo.Id;
                editJob.Id = id;
                editJob.Creator = userInfo;
                return Ok(_jservice.EditOne(editJob));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Job>> DeleteOne(int id)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                return Ok(_jservice.DeleteOne(id));
            }
            catch (System.Exception err) 
            {
                return BadRequest(err.Message);                
            }
        }
    }
}