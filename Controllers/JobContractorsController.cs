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
    public class JobContractorsController : ControllerBase
    {

        private readonly JobContractorsService _jcservice;

        public JobContractorsController(JobContractorsService jcservice)
        {
            _jcservice = jcservice;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<JobContractor>> Create([FromBody] JobContractor newjobCon)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                newjobCon.CreatorId = userInfo.Id;
                newjobCon.Creator = userInfo;
                return Ok(_jcservice.CreateOne(newjobCon));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<JobContractor>> DeleteOne(int id)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                return Ok(_jcservice.DeleteOne(id));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);                
            }
        }
    }
}