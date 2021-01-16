﻿using System;
using System.Collections.Generic;
using System.Linq;
using SalesWebMVC.Services.Exceptions;
using SalesWebMVC.Data;
using SalesWebMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMVC.Services {
    public class SellerService {
        private readonly SalesWebMVCContext context;

        public SellerService(SalesWebMVCContext context) {
            this.context = context;
        }

        public List<Seller> FindAll() {
            return context.Seller.ToList();
        }
        public void Insert(Seller obj) {
            context.Add(obj);
            context.SaveChanges();
        }

        public Seller FindById(int id) {
            return context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id);
        }

        public void Remove(int id) {
            var obj = context.Seller.Find(id);
            context.Seller.Remove(obj);
            context.SaveChanges();
        }

        public void Update(Seller obj) {
            if (!context.Seller.Any(x => x.Id == obj.Id)) {
                throw new NotFoundException("Id not found");
            }
            try {
                context.Update(obj);
                context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e) {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
