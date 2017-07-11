using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using ESClassLib;



namespace ESTests
{

    public class ESSetup
    {

        public static Uri node;
        public static ConnectionSettings settings;
        public static ElasticClient client;


        public ESSetup()
        {
            node = new Uri("http://localhost:9200");
            settings = new ConnectionSettings(node);
          //  settings.DefaultIndex("fake_es");
            client = new ElasticClient(settings);

            var indexSettings = new IndexSettings();
            indexSettings.NumberOfReplicas = 1;
            indexSettings.NumberOfShards = 1;

            var indexConfig = new IndexState
            {
                Settings = indexSettings
            };
            
            
            client.CreateIndex("Team1", c => c.InitializeUsing(indexConfig).Mappings(m => m.Map<TeamData>(mp => mp.AutoMap())));
            client.CreateIndex("Team2", c => c.InitializeUsing(indexConfig).Mappings(m => m.Map<TeamData>(mp => mp.AutoMap())));
            client.CreateIndex("Team3", c => c.InitializeUsing(indexConfig).Mappings(m => m.Map<TeamData>(mp => mp.AutoMap())));
            client.CreateIndex("Team4", c => c.InitializeUsing(indexConfig).Mappings(m => m.Map<TeamData>(mp => mp.AutoMap())));
            client.CreateIndex("Team5", c => c.InitializeUsing(indexConfig).Mappings(m => m.Map<TeamData>(mp => mp.AutoMap())));
            client.CreateIndex("Team6", c => c.InitializeUsing(indexConfig).Mappings(m => m.Map<TeamData>(mp => mp.AutoMap())));



          //  client.CreateIndex(c => c.Index("fake_es").InitializeUsing(indexSettings).AddMapping<TeamData>(m => m.MapFromAttributes()));
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
            
            /* 
            var result = client.GetIndex("Team1");
            
            var result2 = client.Search<dynamic>(s => s
                .AllIndices()
                .AllTypes());
            
            
            var result = client.CatIndices();
            var list = result.Records.ToList();

            foreach (CatIndicesRecord indexRecord in list)
            {
                indexRecord.Index
            }

            TeamData team2 = new TeamData()
            {
                teamName = "Team2",
                numberOfDocs = 654321,
                totalStoreSize = 200000,
                primaryStoreSize = 10000,
                

            };

            TeamData team3 = new TeamData()
            {
                teamName = "Team3",
                numberOfDocs = 10000,
                totalStoreSize = 200000,
                primaryStoreSize = 10000,
                

            };

             TeamData team4 = new TeamData()
            {
                teamName = "Team4",
                numberOfDocs = 123456,
                totalStoreSize = 100000,
                primaryStoreSize = 5000,
                

            };

            TeamData team5 = new TeamData()
            {
                teamName = "Team5",
                numberOfDocs = 654321,
                totalStoreSize = 200000,
                primaryStoreSize = 10000,
                

            };

            TeamData team6 = new TeamData()
            {
                teamName = "Team6",
                numberOfDocs = 10000,
                totalStoreSize = 200000,
                primaryStoreSize = 10000,
                

            };

            */

        }

        









    }


}