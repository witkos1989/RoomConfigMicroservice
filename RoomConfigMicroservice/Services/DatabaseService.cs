using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RoomConfigMicroservice.Persistence;

namespace RoomConfigMicroservice.Services;

public class DatabaseService<T> : IDatabaseService<T> where T : class
{
    ApplicationDbContext _db;

    public DatabaseService(ApplicationDbContext db)
    {
        _db = db;
    }

    public void Create(T entity) =>
        _db.Set<T>().Add(entity);

    public async Task CreateAsync(T entity) =>
        await _db.Set<T>().AddAsync(entity);

    public void Delete(T entity) =>
        _db.Set<T>().Remove(entity);

    public void Update(T entity) =>
        _db.Set<T>().Update(entity);

    public IQueryable<T> FindAll(bool trackChanges) =>
        !trackChanges ? _db.Set<T>().AsNoTracking() : _db.Set<T>();

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
        !trackChanges ? _db.Set<T>().Where(expression).AsNoTracking() : _db.Set<T>().Where(expression);
}