using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using jobsapi.Models;

namespace jobsapi.Repositorys
{
    public class JobsRepository
    {

        private readonly IDbConnection _db;

        public JobsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal IEnumerable<Job> GetAll()
        {
            string sql = @"SELECT
            j.*,
            p.*
            FROM jobs j
            JOIN profiles p ON j.creatorId = p.id;";
            return _db.Query<Job, Profile, Job>(sql, (job, profile) =>
            {
                job.Creator = profile;
                return job;
            }, splitOn: "id");
        }

        internal Job GetOne(int id)
        {
            string sql = @"SELECT
            j.*,
            p.*
            FROM jobs j 
            JOIN profiles p ON j.creatorId = p.id
            WHERE j.id = @id;";
            return _db.Query<Job, Profile, Job>(sql, (job, profile) =>
            {
                job.Creator = profile;
                return job;
            }, new { id }, splitOn: "id").FirstOrDefault();
        }

        internal Job CreateOne(Job newJob)
        {
            string sql = @"INSERT INTO jobs
            (name, location, budget, creatorId)
            VALUES
            (@Name, @Location, @Budget, @CreatorId);
            SELECT LAST_INSERT_ID();";
            int id = _db.ExecuteScalar<int>(sql, newJob);
            newJob.Id = id;
            return newJob;
        }

        internal Job EditOne(Job editJob)
        {
            string sql = @"UPDATE jobs
            SET
                name = @Name,
                location = @Location,
                budget = @Budget
            WHERE id = @id;";
            _db.Execute(sql, editJob);
            return editJob;
        }

        internal void DeleteOne(int id)
        {
            string sql = "DELETE FROM jobs WHERE id = @id LIMIT 1;";
            _db.Execute(sql, new { id });
        }
    }
}