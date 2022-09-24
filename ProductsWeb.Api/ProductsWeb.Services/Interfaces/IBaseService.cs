namespace ProductsWeb.Services.Interfaces
{
    public interface IBaseService<TBaseEntity>
    {
        public Task<TBaseEntity> GetById(Guid id);
        public Task<IEnumerable<TBaseEntity>> GetAll();
        public Task<bool> Delete(Guid id);
        public Task<TBaseEntity> Edit(TBaseEntity entity);
        Task<TBaseEntity> Create(TBaseEntity entity);
        Task<bool> IsExists(TBaseEntity entity);
    }
}
