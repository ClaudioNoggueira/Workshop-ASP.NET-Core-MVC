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
            obj.Department = context.Department.First();
            context.Add(obj);
            context.SaveChanges();
        }
    }
}
