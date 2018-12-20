using MyFootballRestApi.Models;
using System;
using System.Collections.Generic;

namespace MyFootballRestApi.Data
{
  public interface IRepository<T> where T:EntityBase<T>
  {
    List<T> GetAll(Type type);
    T Get(string id);
    T Create( T item);
    T Update( T item);
    T Upsert( T item);
    void Delete(string id);    
  }
}
