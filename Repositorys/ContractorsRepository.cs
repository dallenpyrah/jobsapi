using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using jobsapi.Models;

namespace jobsapi.Repositorys
{
    public class ContractorsRepository
    {

        private readonly IDbConnection _db;

        public ContractorsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal IEnumerable<Contractor> GetAll()
        {
            string sql = @"SELECT
            c.*,
            p.*
            FROM contractors c
            JOIN profiles p ON c.creatorId = p.id;";
            return _db.Query<Contractor, Profile, Contractor>(sql, (contractor, profile) =>
            {
                contractor.Creator = profile;
                return contractor;
            }, splitOn: "id");
        }

        internal Contractor GetOne(int id)
        {
            string sql = @"SELECT
            c.*,
            p.*
            FROM contractors c
            JOIN profiles p ON c.creatorId = p.id
            WHERE c.id = @id;";
            return _db.Query<Contractor, Profile, Contractor>(sql, (contractor, profile) =>
            {
                contractor.Creator = profile;
                return contractor;
            }, new { id }, splitOn: "id").FirstOrDefault();
        }

        internal Contractor CreateOne(Contractor newContractor)
        {
            string sql = @"INSERT INTO contractors
            (name, age, salary, creatorId)
            VALUES
            (@Name, @Age, @Salary, @CreatorId);
            SELECT LAST_INSERT_ID();";
            int id = _db.ExecuteScalar<int>(sql, newContractor);
            newContractor.Id = id;
            return newContractor;
        }

        internal Contractor EditOne(Contractor editContractor)
        {
            string sql = @"UPDATE contractors
            SET
                name = @Name,
                age = @Age, 
                salary = @Salary
            WHERE id = @id;";
            _db.Execute(sql, editContractor);
            return editContractor;
        }

        internal void DeleteOne(int id)
        {
            string sql = "DELETE FROM contractors WHERE id = @id LIMIT 1;";
            _db.Execute(sql, new { id });
            return;
        }

        internal IEnumerable<JobContractorViewModel> GetContractorsByJobId(int id)
        {
            string sql = @"SELECT
            c.*,
            jc.id as JobContractorId
            FROM jobcontractors jc
            JOIN contractors c ON jc.contractorId = c.id
            WHERE jobId = @id;";
            return _db.Query<JobContractorViewModel>(sql, new { id });
        }
    }
}