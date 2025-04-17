using AutoMapper;
using FluentValidation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Bll.Dtos;
using ToDoList.Bll.Entities;
using ToDoList.Dal.Entities;
using ToDoList.Repository.Services;

namespace ToDoList.Bll.Services;

public class ToDoListService : IToDoListService
{
    private readonly IToDoListRepository _toDoListRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<ToDoListCreateDto> _validator;
    public ToDoListService(IToDoListRepository toDoListRepository, IMapper mapper, IValidator<ToDoListCreateDto> validator)
    {
        _toDoListRepository = toDoListRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<long> AddToDoListAsync(ToDoListCreateDto toDoList)
    {
        var status = _validator.Validate(toDoList);
        if (!status.IsValid)
        {
            throw new Exception("toDoList is not Valid");
        }
        var entity = _mapper.Map<ToDoListEntity>(toDoList);
        entity.CreatedAt = DateTime.UtcNow;
        entity.IsCompleted = false;
        return await _toDoListRepository.AddToDoListAsync(entity);
    }

    public async Task DeleteToDOListAsync(long id)
    {
        await _toDoListRepository.DeleteToDOListAsync(id);
    }

    public async Task<GetAllResponse> GetDoTOListsAsync(int skip,int take)
    {
        var count = _toDoListRepository.GetToDoListCount();
        var res  = await _toDoListRepository.GetDoTOListsAsync(skip,take);
        return new GetAllResponse() { Count = count, Dtos = res.Select(x => _mapper.Map<ToDoListGetDto>(x)).ToList() };
    }

    public async Task<ToDoListGetDto> GetToTOListByIDAsync(long id)
    {
        return _mapper.Map<ToDoListGetDto>(await _toDoListRepository.GetToTOListByIDAsync(id));
    }

    public async Task<GetAllResponse> SelectByDueDateAsync(DateTime? data  )
    {
        if(data is null)
        {
            data = DateTime.UtcNow;
        }
        var count = _toDoListRepository.GetToDoListCount();
        var toDoItems =  await _toDoListRepository.SelectByDueDateAsync(data);
        return new GetAllResponse() { Count = count, Dtos = toDoItems.Select(x => _mapper.Map<ToDoListGetDto>(x)).ToList() };
    }

    public async Task<GetAllResponse> SelectCompletedAsync(int skip, int take)
    {
        var count = _toDoListRepository.GetToDoListCount();
        var toDoItems = await _toDoListRepository.SelectCompletedAsync(skip, take);
        return new GetAllResponse() { Count = count, Dtos = toDoItems.Select(x => _mapper.Map<ToDoListGetDto>(x)).ToList() };
    }

    public async Task<GetAllResponse> SelectIncompleteAsync(int skip, int take)
    {
         var count = _toDoListRepository.GetToDoListCount();
        var toDoItems = await _toDoListRepository.SelectIncompleteAsync(skip, take);
        return  new GetAllResponse() { Count = count,Dtos = toDoItems.Select(x => _mapper.Map<ToDoListGetDto>(x)).ToList()};
    }

    public async Task UpdateToDoListAsync(ToDoListGetDto toDoList)
    {
        await _toDoListRepository.UpdateToDoListAsync(_mapper.Map<ToDoListEntity>(toDoList));
    }

}
