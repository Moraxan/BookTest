namespace BookTest.Data.Interfaces
{

    public interface IDbService
    {
        Task<TEntity> AddAsync<TEntity, TDto>(TDto dto)
            where TEntity : class
            where TDto : class;
        Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class, IEntity;
        bool Delete<TReferenceEntity, TDto>(TDto dto)
            where TReferenceEntity : class, IReference
            where TDto : class;
        Task<bool> DeleteAsync<TEntity>(int id) where TEntity : class, IEntity;
        Task<List<TDto>> GetAsync<TEntity, TDto>()
            where TEntity : class
            where TDto : class;
        Task<List<TDto>> GetAsync<TEntity, TDto>(Expression<Func<TEntity, bool>> expression)
            where TEntity : class, IEntity
            where TDto : class;
        string GetURI<TEntity>(TEntity entity) where TEntity : class, IEntity;
        Task Include<TEntity>() where TEntity : class;
        Task<bool> SaveChangesAsync();
        Task<TDto> SingleAsync<TEntity, TDto>(Expression<Func<TEntity, bool>> expression)
            where TEntity : class, IEntity
            where TDto : class;
        void Update<TEntity, TDto>(TDto dto, int id)
            where TEntity : class, IEntity
            where TDto : class;
    }
}