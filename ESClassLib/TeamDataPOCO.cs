using System;
using System.Linq;
using Nest;

namespace ESClassLib
{
    public class TeamData
    {

        public string teamName {get; set;}

        public string numberOfDocs {get; set;}

        public string totalStoreSize {get; set;}

        public string primaryStoreSize {get; set;}


        public TeamData()
        {
            teamName = " ";
            numberOfDocs = "0";
            totalStoreSize = "";
            primaryStoreSize = "0";
        }
        
    }
}
