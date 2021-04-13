using System;
using System.Collections.Generic;
using jobsapi.Models;
using jobsapi.Repositorys;

namespace jobsapi.Services
{
    public class ContractorsService
    {

        private readonly ContractorsRepository _crepo;

        public ContractorsService(ContractorsRepository crepo)
        {
            _crepo = crepo;
        }

        internal IEnumerable<Contractor> GetAll()
        {
            return _crepo.GetAll();
        }

        internal Contractor GetOne(int id)
        {
            var contractor = _crepo.GetOne(id);
            if (contractor == null)
            {
                throw new SystemException("INVALID ID");
            }
            else
            {
                return contractor;
            }
        }

        internal Contractor CreateOne(Contractor newContractor)
        {
            return _crepo.CreateOne(newContractor);
        }

        internal Contractor EditOne(Contractor editContractor)
        {
            Contractor contractor = GetOne(editContractor.Id);
            if (contractor == null)
            {
                throw new SystemException("INVALID ID");
            }
            else
            {
                editContractor.Age = editContractor.Age != null ? editContractor.Age : contractor.Age;
                editContractor.Name = editContractor.Name != null ? editContractor.Name : contractor.Name;
                editContractor.Salary = editContractor.Salary != null ? editContractor.Salary : contractor.Salary;
                return _crepo.EditOne(editContractor);
            }
        }

        internal String DeleteOne(int id)
        {
            Contractor contractor = GetOne(id);
            if (contractor == null)
            {
                throw new SystemException("INVALID ID");
            }
            else
            {
                _crepo.DeleteOne(id);
                return "Deleted";
            }
        }
    }
}