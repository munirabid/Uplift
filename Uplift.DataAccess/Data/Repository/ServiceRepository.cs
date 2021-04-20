using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Uplift.DataAccess.Data.Repository.IRepository;
using Uplift.Models;

namespace Uplift.DataAccess.Data.Repository
{
    public class ServiceRepository : Repository<Service>, IServiceRepository
    {
        private readonly ApplicationDbContext _db;

        public ServiceRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Service service)
        {
            var objFromDb = _db.Services.FirstOrDefault(s =>s.Id == service.Id);

            if (objFromDb != null)
            {
                objFromDb.Name = service.Name;
                objFromDb.Price = service.Price;
                objFromDb.LongDescription = service.LongDescription;
                objFromDb.ImageUrl = service.ImageUrl;
                objFromDb.CategoryId = service.CategoryId;
                objFromDb.FrequencyId = service.FrequencyId;

                _db.SaveChanges();
            }
        }
    }
}
