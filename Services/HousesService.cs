using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using w10d3.Models;
using w10d3.Repositories;

namespace w10d3.Services
{
    public class HousesService
    {
        private readonly HousesRepository _repo;

        public HousesService(HousesRepository repo)
        {
            _repo = repo;
        }

        internal List<House> GetAll()
        {
            return _repo.Get();
        }

        internal House GetById(int id)
        {
            House found = _repo.GetById(id);
            if(found == null)
            {
                throw new Exception("Could not find car with that id.");
            }
            return found;
        }

        internal House Create(House data)
        {
            return _repo.Create(data);
        }

        internal House Edit(House update)
        {
            House edited = GetById(update.Id);
            if(edited.CreatorId != update.CreatorId)
            {
                throw new Exception("You do not have permission to edit this car.");
            }
            edited.Year = update.Year;
            edited.Price = update.Price;
            edited.Bathrooms = update.Bathrooms;
            edited.Bedrooms = update.Bedrooms;
            edited.Levels = update.Levels;
            edited.ImgUrl = update.ImgUrl ?? edited.ImgUrl;
            edited.Description = update.Description ?? edited.Description;


            return _repo.Edit(edited);
        }

        internal House Remove(int id, string userId)
        {
            House removed = GetById(id);
            if(removed.CreatorId != userId)
            {
                throw new Exception("You do not have permission to remove this car.");
            }
            _repo.Remove(id);
            return removed;
        }
    }
}