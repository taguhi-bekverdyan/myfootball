using Couchbase;
using Couchbase.Configuration.Client;
using System;
using System.Collections.Generic;

namespace MyFootballRestApi.Configuration
{
  public class CouchbaseConfig
  {
    private const string ServerUri = "http://173.249.55.17:8091";
    private const string BucketName = "MyFootball";
    private const string BucketPass = "myfootball";

    public static void Setup()
    {
      var config = new ClientConfiguration
      {
        Servers = new List<Uri> { new Uri(ServerUri) },
        UseSsl = false,
        BucketConfigs = new Dictionary<string, BucketConfiguration>()
        {
          {
            "MyFootball", new BucketConfiguration
            {
              BucketName = BucketName,
              Password = BucketPass,
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
