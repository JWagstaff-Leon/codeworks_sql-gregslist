using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using w10d3.Models;
using w10d3.Repositories;

namespace w10d3.Services
{
    public class CarsService
    {
        private readonly CarsRepository _repo;

        public CarsService(CarsRepository repo)
        {
            _repo = repo;
        }

        internal List<Car> GetAll()
        {
            return _repo.Get();
        }

        internal Car GetById(int id)
        {
            Car found = _repo.GetById(id);
            if(found == null)
            {
                throw new Exception("Could not find car with that id.");
            }
            return found;
        }

        internal Car Create(Car data)
        {
            return _repo.Create(data);
        }

        internal Car Edit(Car update)
        {
            Car edited = GetById(update.Id);
            if(edited.CreatorId != update.CreatorId)
            {
                throw new Exception("You do not have permission to edit this car.");
            }
            edited.Make = update.Make ?? edited.Make;
            edited.Model = update.Model ?? edited.Model;
            edited.Color = update.Color ?? edited.Color;
            edited.Year = update.Year;
            edited.Year = update.Price;
            edited.Description = update.Description ?? edited.Description;
            edited.ImgUrl = update.ImgUrl ?? edited.ImgUrl;

            return _repo.Edit(edited);
        }

        internal Car Remove(int id, string userId)
        {
            Car removed = GetById(id);
            if(removed.CreatorId != userId)
            {
                throw new Exception("You do not have permission to remove this car.");
            }
            _repo.Remove(id);
            return removed;
        }
    }
}