using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using Newtonsoft.Json;
using ESClassLib;

 namespace S3ClassLib {
    static class Program {
        
        //@Author Krishna Ganesan
        //Runs entire Elastic Search backend job, ending with the output data structure

        // Args in format []
        static void Main(string[] args) {



        var settings = new ConnectionSettings();
        

        ElasticClient client = new ElasticClient();
        //client.ConnectionSettings.ClientCertificates.Add();
        ESJob jobDoer = new ESJob(client);

        Dictionary<string, TeamData> teamsData = jobDoer.GetData();

        jobDoer.PrintData();


        }
    }

}