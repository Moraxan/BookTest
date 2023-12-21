using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookTest.Data.Interfaces
{
    public interface IDbService
    {
        Task<List<TDto>> GetAsync<TEntity, TDto>() where TEntity : class, IEntity where TDto : class;
        Task<TDto> SingleAsync<TEntity, TDto>(Expression<Func<TEntity, bool>> expression) where TEntity : class, IEntity where TDto : class;
        Task<TDto> AddAsync<TEntity, TDto>(TDto dto) where TEntity : class, IEntity where TDto : class;
            
        Task<TDto> UpdateAsync<TEntity, TDto>(TDto dto) where TEntity : class, IEntity where TDto : class;
        Task DeleteAsync<TEntity>(int id) where TEntity : class, IEntity;

    }
}
