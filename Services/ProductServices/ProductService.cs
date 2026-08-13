using AutoMapper;
using DatabaseMastery.DinnerMenuPostgreSQL.Context;
using DatabaseMastery.DinnerMenuPostgreSQL.Dtos.ProductDtos;
using DatabaseMastery.DinnerMenuPostgreSQL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatabaseMastery.DinnerMenuPostgreSQL.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public ProductService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task CreateProductAsync(CreateProductDto createProductDto)
        {
            var value = _mapper.Map<Product>(createProductDto);
            await _context.Products.AddAsync(value);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteProductAsync(int id)
        {
            var value = await _context.Products.FindAsync(id);
            _context.Products.Remove(value);
            await _context.SaveChangesAsync();
        }
        public async Task<List<ResultProductDto>> GetAllProductsAsync()
        {
            var values = await _context.Products.ToListAsync();
            return _mapper.Map<List<ResultProductDto>>(values);
        }
        public async Task<GetProductByIdDto> GetProductByIdAsync(int id)
        {
            var value = await _context.Products.FindAsync(id);
            return _mapper.Map<GetProductByIdDto>(value);
        }
        public async Task UpdateProductAsync(UpdateProductDto updateProductDto)
        {
            var value = _mapper.Map<Product>(updateProductDto);
            _context.Products.Update(value);
            await _context.SaveChangesAsync();
        }
    }
}
