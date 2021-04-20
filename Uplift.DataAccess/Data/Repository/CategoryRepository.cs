﻿using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetCategoryForDropDown()
        {
            return _db.Categories.Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString(),
            });
        }

        public void Update(Category category)
        {
            var objFromDb = _db.Categories.FirstOrDefault(s =>s.Id == category.Id);

            if (objFromDb != null)
            {
                objFromDb.Name = category.Name;
                objFromDb.DisplayOrder = category.DisplayOrder;

                _db.SaveChanges();
            }
        }
    }
}
