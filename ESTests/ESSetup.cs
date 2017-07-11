using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using Elasticsearch;
using ESClassLib;



namespace ESTests
{
    public class PkiCluster : Ce


    public class ESSetup
    {

        public static Uri node;
        public static ConnectionSettings settings;
        public static ElasticClient client;


        public ESSetup()
        {
            node = new Uri("http://localhost:9200");
            settings = new ConnectionSettings(node);
            settings.DefaultIndex("fake_es");
            client = new ElasticClient(settings);

            var indexSettings = new IndexSettings();
            indexSettings.NumberOfReplicas = 1;
            indexSettings.NumberOfShards = 1;

            var indexConfig = new IndexState
            {
                Settings = indexSettings
            };
            
            
          

        //client.CreateIndex(c => c.Index("Team1").InitializeUsing(indexSettings).AddMapping<TeamData>(m => m.MapFromAttributes()));
        //  client.CreateIndex(c => c.Index("fake_es").InitializeUsing(indexSettings).AddMapping<TeamData>(m => m.MapFromAttributes()));
        }

        public static void AddIndex(string indexName)
        {
            var indexSettings = new IndexSettings();
            indexSettings.NumberOfReplicas = 1;
            indexSettings.NumberOfShards = 1;

            var indexConfig = new IndexState
            {
                Settings = indexSettings
            };
            client.CreateIndex(indexName, c => c.InitializeUsing(indexConfig).Mappings(m => m.Map<TeamData>(mp => mp.AutoMap())));
        }



        public static void AddFakeDataIndexes()
        {
            TeamData team1 = new TeamData()
            {
                teamName = "Team1",
                numberOfDocs = "123456",
                totalStoreSize = "100000",
                primaryStoreSize = "5000",

            };
            


        }

        









    }


}