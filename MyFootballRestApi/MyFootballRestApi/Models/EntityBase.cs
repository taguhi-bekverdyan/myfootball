using System;

namespace MyFootballRestApi.Models
{
  public class EntityBase<T>
  {
    public string Id { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    public string Type => typeof(T).Name.ToLower();
  }
}
