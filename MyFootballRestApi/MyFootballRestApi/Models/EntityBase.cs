using System;

namespace MyFootballRestApi.Models
{
  public class EntityBase<T>
  {
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    public string Id { get; set; }
    public string Type
    {
      get { return typeof(T).Name.ToLower(); }
    }
  }
}
