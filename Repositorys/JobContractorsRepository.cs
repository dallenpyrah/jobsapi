using System;
using System.Data;
using Dapper;
using jobsapi.Models;

namespace jobsapi.Repositorys
{
    public class JobContractorsRepository
    {

        private readonly IDbConnection _db;

        public JobContractorsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal JobContractor CreateOne(JobContractor newjobCon)
        {
            string sql = @"INSERT INTO jobcontractors
            (jobId, contractorId, creatorId)
            VALUES
            (@JobId, @ContractorId, @CreatorId);
            SELECT LAST_INSERT_ID();";
            int id = _db.ExecuteScalar<int>(sql, newjobCon);
            newjobCon.Id = id;
            return newjobCon;
        }

        internal void DeleteOne(int id)
        {
            string sql = "DELETE FROM jobcontractors WHERE id = @id LIMIT 1;";
            _db.Execute(sql, new { id });
        }
    }
}