using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ECommerce.Domain.Interfaces
{
    public interface IGenericRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {

        Task<IEnumerable<TEntity>> GetAllAsync();


        Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity,Tkey>specifications);

        //to get it with the specifications and the specifications will be used to filter the data and
        //return only the data that we need and also to include the related data if we need it and also to order the data
        //if we need it and also to paginate the data if we need it
        Task<int> CountAsync(ISpecifications<TEntity,Tkey> specifications);

        Task<TEntity> GetByIdAsync(ISpecifications<TEntity,Tkey> specifications);


        Task<TEntity> GetByIdAsync(Tkey id);

        Task AddAsync(TEntity entity);

        void Remove(TEntity entity);

        void Update(TEntity entity);

    }
}
