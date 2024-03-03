﻿using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IUnitOfWork
    {
        public IGenericRepository<Category> Category { get; }
        public IGenericRepository<Manufacturer> Manufacturer { get; }

        public IGenericRepository<Product> Product { get; }

        //ADd other models/tables here as you create them so UnitOfWork can access them

        // Save changes to the data source
        int Commit();

        Task<int> CommitAsync();
    }
}