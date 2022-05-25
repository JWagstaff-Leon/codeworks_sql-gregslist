using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using w10d3.Models;

namespace w10d3.Repositories
{
    public class JobsRepository
    {
        private readonly IDbConnection _db;

        public JobsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal List<Job> Get()
        {
            string sql = @"
            SELECT child.*, parent.*
            FROM jobs child
            JOIN accounts parent ON child.creatorId = parent.id";
            return _db.Query<Job, Account, Job>(sql, (job, account) => {
                job.Creator = account;
                return job;
            }).ToList();
        }

        internal Job GetById(int id)
        {
            string sql = @"
            SELECT child.*, parent.*
            FROM jobs child
            JOIN accounts parent ON child.creatorId = parent.id
            WHERE child.id = @id";
            return _db.Query<Job, Account, Job>(sql, (job, account) => {
                job.Creator = account;
                return job;
            }, new { id }).FirstOrDefault();
        }

        internal Job Create(Job data)
        {
            string sql = @"
            INSERT INTO jobs
            (jobTitle, rate, hours, description, company)
            VALUES
            (@JobTitle, @Rate, @Hours, @Description, @Company);
            SELECT LAST_INSERT_ID();";
            data.Id = _db.ExecuteScalar<int>(sql, data);
            data.CreatedAt = DateTime.Now;
            data.UpdatedAt = DateTime.Now;
            return data;
        }

        internal Job Edit(Job update)
        {
            string sql = @"
            UPDATE jobs
            SET
                JobTitle = @JobTitle,
                Rate = @Rate,
                Hours = @Hours,
                Description = @Description,
                Company = @Company
            WHERE id = @Id";
            _db.Execute(sql, update);
            update.UpdatedAt = DateTime.Now;
            return update;
        }

        internal void Remove(int id)
        {
            string sql = "DELETE FROM jobs WHERE id = @id LIMIT 1";
            _db.Execute(sql, new { id });
        }
    }
}