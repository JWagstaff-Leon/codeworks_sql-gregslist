using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using w10d3.Models;

namespace w10d3.Repositories
{
    public class CarsRepository
    {
        private readonly IDbConnection _db;

        public CarsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal List<Car> Get()
        {
            string sql = @"
            SELECT child.*, parent.*
            FROM cars child
            JOIN accounts parent ON child.creatorId = parent.id";
            return _db.Query<Car, Account, Car>(sql, (car, account) => {
                car.Creator = account;
                return car;
            }).ToList();
        }

        internal Car GetById(int id)
        {
            string sql = @"
            SELECT child.*, parent.*
            FROM cars child
            JOIN accounts parent ON child.creatorId = parent.id
            WHERE child.id = @id";
            return _db.Query<Car, Account, Car>(sql, (car, account) => {
                car.Creator = account;
                return car;
            }, new { id }).FirstOrDefault();
        }

        internal Car Create(Car data)
        {
            string sql = @"
            INSERT INTO cars
            (make, model, color, year, price, description, imgUrl, creatorId)
            VALUES
            (@Make, @Model, @Color, @Year, @Price, @Description, @ImgUrl, @CreatorId);
            SELECT LAST_INSERT_ID();";
            data.Id = _db.ExecuteScalar<int>(sql, data);
            data.CreatedAt = DateTime.Now;
            data.UpdatedAt = DateTime.Now;
            return data;
        }

        internal Car Edit(Car update)
        {
            string sql = @"
            UPDATE cars
            SET
                Make = @Make,
                Model = @Model,
                Color = @Color,
                Year = @Year,
                Price = @Price,
                Description = @Description,
                ImgUrl = @ImgUrl,
            WHERE id = @Id";
            _db.Execute(sql, update);
            update.UpdatedAt = DateTime.Now;
            return update;
        }

        internal void Remove(int id)
        {
            string sql = "DELETE FROM cars WHERE id = @id LIMIT 1";
            _db.Execute(sql, new { id });
        }
    }
}