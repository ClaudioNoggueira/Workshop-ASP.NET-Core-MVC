using SalesWebMVC.Data;
using SalesWebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMVC.Services {
    public class DepartmentService {
        private readonly SalesWebMVCContext context;

        public DepartmentService(SalesWebMVCContext context) {
            this.context = context;
        }

        public async Task<List<Department>> FindAllAsync() {
            return await context.Department.OrderBy(x => x.Name).ToListAsync();
        }
    }
}
