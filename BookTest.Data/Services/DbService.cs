

using AutoMapper;
using System.Linq.Expressions;

namespace BookTest.Data.Services
{
    public class DbService : IDbService
    {
        private readonly BookContext _db;
        private readonly IMapper _mapper;
        public DbService(BookContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;

        }

        //Create implementation the IDBService interface
        public async Task<TDto> AddAsync<TEntity, TDto>(TDto dto) where TEntity : class, IEntity where TDto : class
        {
            var entity = _mapper.Map<TEntity>(dto);
            await _db.Set<TEntity>().AddAsync(entity);
            await _db.SaveChangesAsync();
            return _mapper.Map<TDto>(entity);
        }
        public async Task DeleteAsync<TEntity>(int id) where TEntity : class, IEntity
        {
            var entity = await _db.Set<TEntity>().FindAsync(id);
            _db.Set<TEntity>().Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<List<TDto>> GetAsync<TEntity, TDto>() where TEntity : class, IEntity where TDto : class
        {
            var entities = await _db.Set<TEntity>().ToListAsync();
            return _mapper.Map<List<TDto>>(entities);
        }
        public async Task<TDto> SingleAsync<TEntity, TDto>(Expression<Func<TEntity, bool>> expression) where TEntity : class, IEntity where TDto : class
        {
            var entity = await _db.Set<TEntity>().SingleOrDefaultAsync(expression);
            return _mapper.Map<TDto>(entity);
        }
        public async Task<TDto> UpdateAsync<TEntity, TDto>(TDto dto) where TEntity : class, IEntity where TDto : class
        {
            var entity = _mapper.Map<TEntity>(dto);
            _db.Set<TEntity>().Update(entity);
            await _db.SaveChangesAsync();
            return _mapper.Map<TDto>(entity);
        }


    }
}
