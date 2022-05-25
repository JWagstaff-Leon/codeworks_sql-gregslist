using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using w10d3.Models;
using w10d3.Repositories;

namespace w10d3.Services
{
    public class JobsService
    {
        private readonly JobsRepository _repo;

        public JobsService(JobsRepository repo)
        {
            _repo = repo;
        }

        internal List<Job> GetAll()
        {
            return _repo.Get();
        }

        internal Job GetById(int id)
        {
            Job found = _repo.GetById(id);
            if(found == null)
            {
                throw new Exception("Could not find car with that id.");
            }
            return found;
        }

        internal Job Create(Job data)
        {
            return _repo.Create(data);
        }

        internal Job Edit(Job update)
        {
            Job edited = GetById(update.Id);
            if(edited.CreatorId != update.CreatorId)
            {
                throw new Exception("You do not have permission to edit this car.");
            }
            edited.JobTitle = update.JobTitle ?? edited.JobTitle;
            edited.Rate = update.Rate;
            edited.Hours = update.Hours;
            edited.Description = update.Description ?? edited.Description;
            edited.Company = update.Company ?? edited.Company;
            edited.Company = update.Company ?? edited.Company;

            return _repo.Edit(edited);
        }

        internal Job Remove(int id, string userId)
        {
            Job removed = GetById(id);
            if(removed.CreatorId != userId)
            {
                throw new Exception("You do not have permission to remove this car.");
            }
            _repo.Remove(id);
            return removed;
        }
    }
}