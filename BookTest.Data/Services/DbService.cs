using BookTest.Common.DTOs;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<TDto>> GetAsync<TEntity, TDto>() where TEntity : class where TDto : class
        {
            var entities = await _db.Set<TEntity>().ToListAsync();
            return _mapper.Map<List<TDto>>(entities);
        }

        public async Task<List<TDto>> GetAsync<TEntity, TDto>(Expression<Func<TEntity, bool>> expression) where TEntity : class, IEntity where TDto : class
        {
            var entities = await _db.Set<TEntity>().Where(expression).ToListAsync();
            return _mapper.Map<List<TDto>>(entities);
        }

        private async Task<TEntity?> SingleAsync<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class, IEntity
        {
            return await _db.Set<TEntity>().SingleOrDefaultAsync(expression);
        }

        public async Task<TDto> SingleAsync<TEntity, TDto>(Expression<Func<TEntity, bool>> expression) where TEntity : class, IEntity where TDto : class
        {
            var entity = await SingleAsync(expression);
            return _mapper.Map<TDto>(entity);
        }

        public async Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class, IEntity
        {
            return await _db.Set<TEntity>().AnyAsync(expression);
        }

        public async Task<TEntity> AddAsync<TEntity, TDto>(TDto dto) where TEntity : class where TDto : class
        {
            var entity = _mapper.Map<TEntity>(dto);
            await _db.Set<TEntity>().AddAsync(entity);
            return entity;
        }

        public void Update<TEntity, TDto>(TDto dto, int id) where TEntity : class, IEntity where TDto : class
        {
            var entity = _mapper.Map<TEntity>(dto);
            entity.Id = id;
            _db.Set<TEntity>().Update(entity);
        }

        public async Task<bool> DeleteAsync<TEntity>(int id) where TEntity : class, IEntity
        {
            try
            {
                var entity = await SingleAsync<TEntity>(e => e.Id == id);
                if (entity is null)
                {
                    return false;
                }
                _db.Remove(entity);
            }
            catch (Exception)
            {
                throw;
            }
            return true;
        }

        public bool Delete<TReferenceEntity, TDto>(TDto dto) where TReferenceEntity : class, IReference where TDto : class
        {
            try
            {
                var entity = _mapper.Map<TReferenceEntity>(dto);
                if (entity is null)
                {
                    return false;
                }
                _db.Remove(entity);
            }
            catch (Exception)
            {
                throw;
            }
            return true;
        }

        public async Task Include<TEntity>() where TEntity : class

        {
            var propertyNames = _db.Model.FindEntityType(typeof(TEntity))?.GetNavigations().Select(e => e.Name);

            if (propertyNames is null) return;

            foreach (var name in propertyNames)
                await _db.Set<TEntity>().Include(name).LoadAsync();

        }


        public async Task<bool> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync() >= 0;
        }

        public string GetURI<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            return $"/{typeof(TEntity).Name.ToLower()}s/{entity.Id}";
        }




        //Join table methods
        public async Task<TReferenceEntity> AddReferenceAsync<TReferenceEntity, TDto>(TDto dto)
    where TReferenceEntity : class, IReference, new()
    where TDto : class
        {
            var entity = new TReferenceEntity();

            // Manual mapping from DTO to entity
            // Replace 'AuthorBookDTO' and 'AuthorBook' with your actual DTO and entity class names
            // Also, replace 'AuthorId' and 'BookId' with the actual property names in your DTO and entity
            if (dto is AuthorBookDTO authorBookDto && entity is AuthorBook authorBookEntity)
            {
                authorBookEntity.AuthorId = authorBookDto.AuthorId;
                authorBookEntity.BookId = authorBookDto.BookId;
            }

            // After mapping, perform the existence checks
            // Example for AuthorBook, adjust as needed for other entity types
            if (entity is AuthorBook authorBook)
            {
                // Check if Author exists
                var authorExists = await _db.Authors.AnyAsync(a => a.Id == authorBook.AuthorId);
                if (!authorExists)
                {
                    throw new InvalidOperationException("Author not found.");
                }

                // Check if Book exists
                var bookExists = await _db.Books.AnyAsync(b => b.Id == authorBook.BookId);
                if (!bookExists)
                {
                    throw new InvalidOperationException("Book not found.");
                }
            }

            // Add the entity to the DbSet and save changes
            _db.Set<TReferenceEntity>().Add(entity);
            await _db.SaveChangesAsync();

            return entity;
        }



        public async Task<bool> DeleteReferenceAsync<TReferenceEntity>(int id)
            where TReferenceEntity : class, IReference
        {
            var entity = await _db.Set<TReferenceEntity>().FindAsync(id);
            if (entity == null)
            {
                return false;
            }

            _db.Set<TReferenceEntity>().Remove(entity);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<List<TDto>> GetReferenceAsync<TReferenceEntity, TDto>(Expression<Func<TReferenceEntity, bool>> expression)
        where TReferenceEntity : class, IReference
        where TDto : class
        {
            var entities = await _db.Set<TReferenceEntity>().Where(expression).ToListAsync();
            var dtos = _mapper.Map<List<TDto>>(entities);
            return dtos;
        }
        public async Task<bool> DeleteByCompositeKey<TReferenceEntity>(
        Expression<Func<TReferenceEntity, bool>> predicate)
        where TReferenceEntity : class, IReference
        {
            var entity = await _db.Set<TReferenceEntity>().FirstOrDefaultAsync(predicate);
            if (entity == null)
            {
                return false;
            }

            _db.Set<TReferenceEntity>().Remove(entity);
            await _db.SaveChangesAsync();

            return true;
        }

    }
}