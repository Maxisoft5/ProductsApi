namespace Products.DataAccessEfCore.Repositories.Interfaces
{
    public interface IBaseRepository<TBaseEntity>
    {
        public Task<TBaseEntity> GetById(Guid id);
        public Task<IEnumerable<TBaseEntity>> GetAll();
        public Task<bool> Delete(Guid id);
        public Task<TBaseEntity> Edit(TBaseEntity entity);
        public Task<TBaseEntity> Create(TBaseEntity entity);
        public Task<bool> IsExists(TBaseEntity entity, bool forUpdate = false);
    }
}
