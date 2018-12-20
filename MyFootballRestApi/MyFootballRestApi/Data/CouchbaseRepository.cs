using System;
using System.Collections.Generic;
using Couchbase;
using Couchbase.Core;
using Couchbase.N1QL;
using MyFootballRestApi.Models;

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

        public List<T> GetAll(Type t)
        {
            var type = t.Name.ToLower();
            var query = new QueryRequest("SELECT MyFootball.* FROM MyFootball WHERE type = $type");
            query.AddNamedParameter("type", type);
            var result = _bucket.Query<T>(query);
            return !result.Success ? null : result.Rows;
        }

        public T Get(string id)
        {
            var key = CreateKey(id);
            var result = _bucket.Get<T>(key);
            return !result.Success ? null : result.Value;
        }

        public T Create(T item)
        {
            item.Id = Guid.NewGuid().ToString();
            item.Created = DateTime.Now;
            item.Updated = DateTime.Now;
            var key = CreateKey(item.Id);

            var result = _bucket.Insert(key, item);
            if (!result.Success) throw result.Exception;

            return item;
        }

        public T Update(T item)
        {
            item.Updated = DateTime.Now;
            var key = CreateKey(item.Id);
            var result = _bucket.Replace(key, item);

            if (!result.Success) throw result.Exception;

            return item;
        }

        public T Upsert(T item)
        {
            if (Get(item.Id) == null)
            {
                item.Id = Guid.NewGuid().ToString();
                item.Created = DateTime.Now;
            }

            item.Updated = DateTime.Now;
            var key = CreateKey(item.Id);
            var result = _bucket.Upsert(key, item);

            if (!result.Success) throw result.Exception;

            return item;
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