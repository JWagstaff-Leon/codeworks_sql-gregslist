using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using w10d3.Models;

namespace w10d3.Repositories
{
    public class HousesRepository
    {
        private readonly IDbConnection _db;

        public HousesRepository(IDbConnection db)
        {
            _db = db;
        }

        internal List<House> Get()
        {
            string sql = @"
            SELECT child.*, parent.*
            FROM houses child
            JOIN accounts parent ON child.creatorId = parent.id";
            return _db.Query<House, Account, House>(sql, (house, account) => {
                house.Creator = account;
                return house;
            }).ToList();
        }

        internal House GetById(int id)
        {
            string sql = @"
            SELECT child.*, parent.*
            FROM houses child
            JOIN accounts parent ON child.creatorId = parent.id
            WHERE child.id = @id";
            return _db.Query<House, Account, House>(sql, (house, account) => {
                house.Creator = account;
                return house;
            }, new { id }).FirstOrDefault();
        }

        internal House Create(House data)
        {
            string sql = @"
            INSERT INTO houses
            (year, price, bathrooms, bedrooms, levels, imgUrl, description, creatorId)
            VALUES
            (@Year, @Price, @Bathrooms, @Bedrooms, @Levels, @ImgUrl, @Description, @CreatorId);
            SELECT LAST_INSERT_ID();";
            data.Id = _db.ExecuteScalar<int>(sql, data);
            data.CreatedAt = DateTime.Now;
            data.UpdatedAt = DateTime.Now;
            return data;
        }

        internal House Edit(House update)
        {
            string sql = @"
            UPDATE houses
            SET
                Year = @Year,
                Price = @Price,
                Bathrooms = @Bathrooms,
                Bedrooms = @Bedrooms,
                Levels = @Levels,
                ImgUrl = @ImgUrl,
                Description = @Description
            WHERE id = @Id";
            _db.Execute(sql, update);
            update.UpdatedAt = DateTime.Now;
            return update;
        }

        internal void Remove(int id)
        {
            string sql = "DELETE FROM houses WHERE id = @id LIMIT 1";
            _db.Execute(sql, new { id });
        }
    }
}