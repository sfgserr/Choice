﻿using Choice.ReviewService.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Choice.ReviewService.Api.Infrastructure.Data;

namespace Choice.ReviewService.Api.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ReviewContext _context;

        public ReviewRepository(ReviewContext context)
        {
            _context = context;
        }

        public async Task Add(Review review)
        {
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<Review>> Get(string guid)
        {
            return await _context.Reviews.Include(r => r.Author).Where(r => r.UserGuid == guid).ToListAsync();
        }

        public async Task<Review> Get(int id)
        {
            return await _context.Reviews.Include(r => r.Author).FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<bool> Update(Review review)
        {
            _context.Reviews.Update(review);

            int affections = await _context.SaveChangesAsync();

            return affections > 0;
        }
    }
}
