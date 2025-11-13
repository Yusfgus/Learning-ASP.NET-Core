using System.Collections.Generic;
using System.Threading.Tasks;
using ABC.MinimalApi.Models;

namespace ABC.MinimalApi.Services;

public interface IBookService
{
    Task<List<Book>> GetAll();
}
