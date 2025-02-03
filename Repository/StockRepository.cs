using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context;
        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Stock> CreateAsunc(Stock stockModel)
        {
            await _context.Stock.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;//retruning incomming stockModel back with newly generated id on success
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);
            if (stockModel == null)
            {
                return null;
            }
            _context.Stock.Remove(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;

        }

        public Task<List<Stock>> GetAllAsync()
        {
            return _context.Stock.ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stock.FindAsync(id);

        }

        public async Task<Stock?> UpdateAsync(int id, UpdatedStockRequestDto updateDto)
        {
            var existingStock = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);

            if (existingStock == null)
            {
                return null;
            }
            existingStock.Symbol = updateDto.Symbol;
            existingStock.CompanyName = updateDto.CompanyName;
            existingStock.Purchage = updateDto.Purchage;
            existingStock.LastDiv = updateDto.LastDiv;
            existingStock.Industry = updateDto.Industry;
            existingStock.MarketCap = updateDto.MarketCap;

            await _context.SaveChangesAsync();

            return existingStock;
        }

    }
}