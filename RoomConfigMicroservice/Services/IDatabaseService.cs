﻿using System.Linq.Expressions;

namespace RoomConfigMicroservice.Services;

public interface IDatabaseService<T>
{
    IQueryable<T> FindAll(bool trackChanges);

    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);

    void Create(T entity);

    Task CreateAsync(T entity);

    void Update(T entity);

    void Delete(T entity);
}