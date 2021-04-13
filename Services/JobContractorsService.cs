using System;
using jobsapi.Models;
using jobsapi.Repositorys;

namespace jobsapi.Services
{
    public class JobContractorsService
    {

        private readonly JobContractorsRepository _jcrepo;

        public JobContractorsService(JobContractorsRepository jcrepo)
        {
            _jcrepo = jcrepo;
        }

        internal JobContractor CreateOne(JobContractor newjobCon)
        {
            return _jcrepo.CreateOne(newjobCon);
        }

        internal String DeleteOne(int id)
        {
            _jcrepo.DeleteOne(id);
            return "Deleted";
        }
    }
}