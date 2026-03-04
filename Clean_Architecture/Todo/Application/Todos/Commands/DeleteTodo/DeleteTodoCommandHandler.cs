using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Todos;
using MediatR;

namespace Application.Todos.Commands.DeleteTodo;

public class DeleteTodoCommandHandler(IAppDbContext dbContext) : IRequestHandler<DeleteTodoCommand>
{
    public async Task Handle(DeleteTodoCommand request, CancellationToken ct)
    {
        Todo? todo = await dbContext.Todos.FindAsync([request.Id], ct) 
            ?? throw new NotFoundException(nameof(Todo), request.Id);
        
        dbContext.Todos.Remove(todo);

        int x = await dbContext.SaveChangesAsync(ct);
    }
}