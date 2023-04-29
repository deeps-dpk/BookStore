using Deeps.BookStore.Models;
using Deeps.BookStore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Deeps.BookStore.Repository
{
    public class LanguageRepository : ILanguageRepository
    {
        private readonly BookStoreContext _context = null;
        public LanguageRepository(BookStoreContext context)
        {
            _context = context;
        }
        public async Task<List<LanguageModel>> GetLanguages()
        {
            return await _context.Language.Select(x => new LanguageModel()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            }).ToListAsync();
        }
    }
}
