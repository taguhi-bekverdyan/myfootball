using MyFootballRestApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyFootballRestApi.Data
{
  public interface IRepository<T> where T:EntityBase<T>
  {
    Task<List<T>> GetAll(Type type);
    Task<T> Get(string id);
    Task<T> Create(string id, T item);
    Task<T> Update(string id, T item);
    Task<T> Upsert(string id, T item);
    Task Delete(string id);    
  }
}
