using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace ESClassLib
{

    public class ESJob
    {
        //Key = teamName - to be used for determining if a team's data has already been entered
        Dictionary<string, TeamData> teamData = new Dictionary<string, TeamData>();
        ElasticClient client;

        public ESJob(ElasticClient _client)
        {
            client = _client;
        }



        public Dictionary<string, TeamData> GetData()
        {
            //Obtain all indices and convert respoonse into List of index records...
            var catIndicesResponse = client.CatIndices();
            var listIndicesRecord  = catIndicesResponse.Records.ToList();

            
            IndexRecordParser indexParser = new IndexRecordParser();
            
            //Parse each index record into our dictionary
            foreach (CatIndicesRecord indexRecord in listIndicesRecord)
            {
                teamData = indexParser.ParseIndexRecordIntoDict(teamData, indexRecord);
            }


            //TODO: add json serialize and adding file to s3 bucket "datalens-leaderboards"




            return teamData;
        }



        public void PrintData()
        {

            foreach (KeyValuePair<string, TeamData> pair in teamData)
            {
                Console.WriteLine("Team: " + pair.Key + " Total storage size: " + pair.Value.totalStoreSize);
            }

        }
    }
}





    