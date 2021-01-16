using SalesWebMVC.Data;
using SalesWebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.Services {
    public class DepartmentService {
        private readonly SalesWebMVCContext context;

        public DepartmentService(SalesWebMVCContext context) {
            this.context = context;
        }

        public List<Department> FindAll() {
            return context.Department.OrderBy(x => x.Name).ToList();
        }
    }
}
