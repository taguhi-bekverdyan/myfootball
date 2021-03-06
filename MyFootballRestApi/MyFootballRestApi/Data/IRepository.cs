﻿using MyFootballRestApi.Models;
using System;
using System.Collections.Generic;

namespace MyFootballRestApi.Data
{
  public interface IRepository<T> where T:EntityBase<T>
  {
    List<T> GetAll(Type type);
    T Get(string id);
    T Create(string id, T item);
    T Update(string id, T item);
    T Upsert(string id, T item);
    void Delete(string id);    
  }
}
