using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ABC.MinimalApi.Models;

namespace ABC.MinimalApi.Services;

public class BookService : IBookService
{
    public async Task<List<Book>> GetAll()
    {
        await Task.Delay(Random.Shared.Next(500, 3000));

        return new List<Book>
        {
            new Book {Id = 0, Title = "Book 0", Author = "Author 0"},
            new Book {Id = 1, Title = "Book 1", Author = "Author 1"},
            new Book {Id = 2, Title = "Book 2", Author = "Author 2"},
            new Book {Id = 3, Title = "Book 3", Author = "Author 3"},
            new Book {Id = 4, Title = "Book 4", Author = "Author 4"},
            new Book {Id = 5, Title = "Book 5", Author = "Author 5"},
            new Book {Id = 6, Title = "Book 6", Author = "Author 6"},
            new Book {Id = 7, Title = "Book 7", Author = "Author 7"},
            new Book {Id = 8, Title = "Book 8", Author = "Author 8"},
            new Book {Id = 9, Title = "Book 9", Author = "Author 9"},
        };
    }
}