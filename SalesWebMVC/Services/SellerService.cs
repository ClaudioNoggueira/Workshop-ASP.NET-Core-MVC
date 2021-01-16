using System;
using System.Collections.Generic;
using System.Linq;
using SalesWebMVC.Services.Exceptions;
using SalesWebMVC.Data;
using SalesWebMVC.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace SalesWebMVC.Services {
    public class SellerService {
        private readonly SalesWebMVCContext context;

        public SellerService(SalesWebMVCContext context) {
            this.context = context;
        }

        public async Task<List<Seller>> FindAllAsync() {
            return await context.Seller.ToListAsync();
        }
        public async Task InsertAsync(Seller obj) {
            context.Add(obj);
            await context.SaveChangesAsync();
        }

        public async Task<Seller> FindByIdAsync(int id) {
            return await context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoveAsync(int id) {
            try {
                var obj = await context.Seller.FindAsync(id);
                context.Seller.Remove(obj);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException e) {
                throw new IntegrityException("Cannot delete seller because he/she has sales. Otherwise, there would be sales without sellers.");
            }
        }

        public async Task UpdateAsync(Seller obj) {
            if (!await context.Seller.AnyAsync(x => x.Id == obj.Id)) {
                throw new NotFoundException("Id not found");
            }
            try {
                context.Update(obj);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e) {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
