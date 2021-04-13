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
    public class ContractorsController : ControllerBase
    {
        private readonly ContractorsService _cservice;

        public ContractorsController(ContractorsService cservice)
        {
            _cservice = cservice;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Contractor>> GetAll()
        {
            try
            {
                return Ok(_cservice.GetAll());
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Contractor> GetOne(int id)
        {
            try
            {
                return Ok(_cservice.GetOne(id));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Contractor>> CreateOne([FromBody] Contractor newContractor)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                newContractor.CreatorId = userInfo.Id;
                newContractor.Creator = userInfo;
                return Ok(_cservice.CreateOne(newContractor));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Contractor>> EditOne(int id, [FromBody] Contractor editContractor)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                editContractor.CreatorId = userInfo.Id;
                editContractor.Id = id;
                editContractor.Creator = userInfo;
                return Ok(_cservice.EditOne(editContractor));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Contractor>> DeleteOne(int id)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                return Ok(_cservice.DeleteOne(id));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);                
            }
        }
    }
}