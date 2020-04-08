using Microsoft.EntityFrameworkCore;
using SneakerShop.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

public class GenericRepo<TEntity> : IGenericRepo<TEntity> where TEntity : class
{
    public SneakerShopContext _context { get; set; }

    //ctor dependancy van de applicatie context:
    public GenericRepo(SneakerShopContext context)
    {
        this._context = context;
    }

    //interface implementatie:
    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await this._context.Set<TEntity>().AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetByExpressionAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await this._context.Set<TEntity>().Where(expression).AsNoTracking().ToListAsync();
    }

    public async Task Create(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
    }

    public async Task Update(TEntity entity)
    {
        await Task.Factory.StartNew(() =>
        {
            _context.Set<TEntity>().Update(entity);

        });
    }

    public async Task Delete(TEntity entity)
    {
        await Task.Factory.StartNew(() =>
        {
            _context.Set<TEntity>().Remove(entity);

        });
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

}
