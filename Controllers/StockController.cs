using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;


namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public StockController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var stocks = _context.Stock.ToList()
            .Select(s => s.ToStockDto());
            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var stock = _context.Stock.Find(id);
            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateStockRequestDto stockDto)
        {
            var stockModel = stockDto.ToStockFromCreateDTO();
            // var stockModel = StockMappers.ToStockFromCreateDTO(stockDto);
            _context.Stock.Add(stockModel);
            _context.SaveChanges();
            //In Entity Framework, the ID for an entity is generated and assigned back to the entity instance after the SaveChanges()
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }


        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdatedStockRequestDto updateDto)
        {
            var stockModel = _context.Stock.FirstOrDefault(x => x.Id == id);
            if (stockModel == null)
            {
                return NotFound();
            }
            stockModel.Symbol = updateDto.Symbol;
            stockModel.CompanyName = updateDto.CompanyName;
            stockModel.Purchage = updateDto.Purchage;
            stockModel.LastDiv = updateDto.LastDiv;
            stockModel.Industry = updateDto.Industry;
            stockModel.MarketCap = updateDto.MarketCap;

            _context.SaveChanges();
            return Ok(stockModel.ToStockDto());
        }

        [HttpDelete("{id}")]
        public IActionResult delete([FromRoute] int id)
        {
            var stockModel = _context.Stock.FirstOrDefault(x => x.Id == id);
            if (stockModel == null)
            {
                return NotFound();
            }

            _context.Remove(stockModel);
            _context.SaveChanges();
            return Ok(stockModel.ToStockDto());
        }
    }
}