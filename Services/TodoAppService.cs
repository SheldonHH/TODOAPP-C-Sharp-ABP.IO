using TodoApp.Services;

using TodoAppEntities;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using TodoApp.Services.Dtos;

namespace TodoApp.Services;

public class TodoAppService : TodoAppAppService
{
    private readonly IRepository<TodoItem, Guid> _todoItemRepository;
    
    public TodoAppService(IRepository<TodoItem, Guid> todoItemRepository)
    {
        _todoItemRepository = todoItemRepository;
    }
    
    public async Task<List<TodoItemDto>> GetListAsync()
    {
    var items = await _todoItemRepository.GetListAsync();
    return items
        .Select(item => new TodoItemDto
        {
            Id = item.Id,
            Text = item.Text
        }).ToList();
    }
    public async Task<TodoItemDto> CreateAsync(string text)
    {
        var todoItem = await _todoItemRepository.InsertAsync(
            new TodoItem {Text = text}
        );

        return new TodoItemDto
        {
            Id = todoItem.Id,
            Text = todoItem.Text
        };
    }
    public async Task DeleteAsync(Guid id)
    {
        await _todoItemRepository.DeleteAsync(id);
    }



}
