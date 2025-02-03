using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Dtos.Stock;
namespace api.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync();
        Task<Stock?>GetByIdAsync(int id); //Stock might not return if notFound
        Task<Stock>CreateAsunc(Stock stockModel);
        Task<Stock?>UpdateAsync(int id,UpdatedStockRequestDto stockDto);
        Task<Stock?>DeleteAsync(int id);

    }
}