using System;
using System.Collections.Generic;
using Couchbase;
using Couchbase.Core;
using Couchbase.N1QL;
using MyFootballRestApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyFootballRestApi.Data
{
    public class CouchbaseRepository<T> : IRepository<T> where T : EntityBase<T>
    {
        private readonly IBucket _bucket = ClusterHelper.GetBucket("MyFootball");

        #region Private Methods

        private string CreateKey(string id)
        {
            // generates type-prefixed key like 'player::123'
            return string.Format("{0}::{1}", typeof(T).Name.ToLower(), id);
        }

        #endregion

        #region Public Members

        #region CRUD

    public async Task<List<T>> GetAll(Type t)
    {
            var type = t.Name.ToLower();

            var queryRequest = new QueryRequest()
                .Statement("SELECT MyFootball.* FROM MyFootball WHERE type = $type")
                .AddNamedParameter("$type", type);



            var results = await _bucket.QueryAsync<T>(queryRequest);

            //return !result.Success ? null : result.Rows;
            return results.Rows;
    }

    public async Task<T> Get(string id)
    {
      var key = CreateKey(id);
      var result = await _bucket.GetAsync<T>(key);
      return !result.Success ? null : result.Value;
    }

    public async Task<T> Create(string id, T item)
    {
      item.Created = DateTime.Now;
      item.Updated = DateTime.Now;
      var key = CreateKey(id);

      var result = await _bucket.InsertAsync(key, item);
      if (!result.Success) throw result.Exception;

            return item;
        }

    public async Task<T> Update(string id, T item)
    {
      item.Updated = DateTime.Now;
      var key = CreateKey(id);
      var result = await _bucket.ReplaceAsync(key, item);

            if (!result.Success) throw result.Exception;

            return item;
        }

    public async Task<T> Upsert(string id, T item)
    {
      if (Get(id) == null) item.Created = DateTime.Now;
      item.Updated = DateTime.Now;
      var key = CreateKey(id);
      var result = await _bucket.UpsertAsync(key, item);

            item.Updated = DateTime.Now;
            var key = CreateKey(item.Id);
            var result = _bucket.Upsert(key, item);

            if (!result.Success) throw result.Exception;

    public async Task Delete(string id)
    {
      var key = CreateKey(id);
      var result = await _bucket.RemoveAsync(key);
      if (!result.Success) throw result.Exception;
    }

        public void Delete(string id)
        {
            var key = CreateKey(id);
            var result = _bucket.Remove(key);
            if (!result.Success) throw result.Exception;
        }

        #endregion

        #endregion
    }
}