using System.Collections.Generic;
using System.Threading.Tasks;
using ABC.Controller.Models;

namespace ABC.Controller.Services;

public interface IBookService
{
    Task<List<Book>> GetAll();
}
