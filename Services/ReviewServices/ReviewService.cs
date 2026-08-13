using AutoMapper;
using DatabaseMastery.DinnerMenuPostgreSQL.Context;
using DatabaseMastery.DinnerMenuPostgreSQL.Dtos.ReviewDtos;
using DatabaseMastery.DinnerMenuPostgreSQL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatabaseMastery.DinnerMenuPostgreSQL.Services.ReviewServices
{
    public class ReviewService : IReviewService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public ReviewService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task CreateReviewAsync(CreateReviewDto createReviewDto)
        {
            var value = _mapper.Map<Review>(createReviewDto);
            await _context.Reviews.AddAsync(value);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteReviewAsync(int id)
        {
            var value = await _context.Reviews.FindAsync(id);
            _context.Reviews.Remove(value);
            await _context.SaveChangesAsync();
        }
        public async Task<List<ResultReviewDto>> GetAllReviewsAsync()
        {
            var values = await _context.Reviews.Include(y => y.Product).ToListAsync();
            return _mapper.Map<List<ResultReviewDto>>(values);
        }
        public async Task<GetReviewByIdDto> GetReviewByIdAsync(int id)
        {
            var value = await _context.Reviews.FindAsync(id);
            return _mapper.Map<GetReviewByIdDto>(value);
        }
        public async Task UpdateReviewAsync(UpdateReviewDto updateReviewDto)
        {
            var value = _mapper.Map<Review>(updateReviewDto);
            _context.Reviews.Update(value);
            await _context.SaveChangesAsync();
        }
    }
}
