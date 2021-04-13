using System;
using System.Collections.Generic;
using jobsapi.Models;
using jobsapi.Repositorys;

namespace jobsapi.Services
{
    public class JobsService
    {

        private readonly JobsRepository _jrepo;

        public JobsService(JobsRepository jrepo)
        {
            _jrepo = jrepo;
        }

        internal IEnumerable<Job> GetAll()
        {
            return _jrepo.GetAll();
        }

        internal Job GetOne(int id)
        {
            var job = _jrepo.GetOne(id);
            if (job.Id == null)
            {
                throw new SystemException("Invalid Id");
            }
            else
            {
                return job;
            }
        }

        internal Job CreateOne(Job newJob)
        {
            return _jrepo.CreateOne(newJob);
        }

        internal Job EditOne(Job editJob)
        {
            Job current = GetOne(editJob.Id);
            if (current == null)
            {
                throw new SystemException("Invalid Id");
            }
            else
            {
                editJob.Budget = editJob.Budget != null ? editJob.Budget : current.Budget;
                editJob.Location = editJob.Location != null ? editJob.Location : current.Location;
                editJob.Name = editJob.Name != null ? editJob.Name : current.Name;
                return _jrepo.EditOne(editJob);
            }
        }

        internal String DeleteOne(int id)
        {
            Job job = GetOne(id);
            if (job == null)
            {
                throw new SystemException("Invalid Id");
            }
            else
            {
                _jrepo.DeleteOne(id);
                return "Deleted";
            }
        }
    }
}