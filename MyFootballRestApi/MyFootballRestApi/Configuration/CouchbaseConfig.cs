using Couchbase;
using Couchbase.Configuration.Client;
using System;
using System.Collections.Generic;

namespace MyFootballRestApi.Configuration
{
  public class CouchbaseConfig
  {
    public static void Setup()
    {
      var config = new ClientConfiguration
      {
        Servers = new List<Uri> { new Uri("http://173.249.55.17:8091") },
        UseSsl = false,
        BucketConfigs = new Dictionary<string, BucketConfiguration>()
        {
          {
            "MyFootball", new BucketConfiguration
            {
              BucketName = "MyFootball",
              Password = "myfootball",
              UseSsl = false
            }
          }
        }
      };

      ClusterHelper.Initialize(config);
    }

    public static void Cleanup()
    {
      ClusterHelper.Close();
    }
  }
}
