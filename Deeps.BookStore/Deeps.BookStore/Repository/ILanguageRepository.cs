using Deeps.BookStore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deeps.BookStore.Repository
{
    public interface ILanguageRepository
    {
        Task<List<LanguageModel>> GetLanguages();
    }
}