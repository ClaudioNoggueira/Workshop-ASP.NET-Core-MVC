using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMVC.Data;
using SalesWebMVC.Models;

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
            return context.Seller.FirstOrDefault(obj => obj.Id == id);
        }

        public void Remove(int id) {
            var obj = context.Seller.Find(id);
            context.Seller.Remove(obj);
            context.SaveChanges();
        }
    }
}
