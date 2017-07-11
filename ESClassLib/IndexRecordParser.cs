using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace ESClassLib
{

    public class IndexRecordParser
    {
        
        TeamNameParser teamNameParser;

        public IndexRecordParser()
        {
            teamNameParser= new TeamNameParser();
        }


        //Parses a new Index into our Dictionary by either adding a new K,V pair or updating an already existing pair. Returns our dictionary back
        public Dictionary<string, TeamData> ParseIndexRecordIntoDict(Dictionary<string, TeamData> teamDataDict, CatIndicesRecord record)
        {

            //Get the official team name from the index name
            string indexTeamName = teamNameParser.GetTeamName(record.Index);

            //If this team is not already in our dictionary, instantiate a new TeamData and add the pair in
            if (!teamDataDict.ContainsKey(indexTeamName))
            {
                TeamData teamData = new TeamData
                {
                    teamName = indexTeamName,
                    primaryStoreSize = record.PrimaryStoreSize,
                    numberOfDocs = record.DocsCount,
                    totalStoreSize = record.TotalMemory
                };
                

                teamDataDict.Add(indexTeamName,teamData);

            }
            //Otherwise, we have to update the TeamData value for this team's additional index in ES
            else
            {
                TeamData previousTeamData = teamDataDict[indexTeamName];

                TeamData updatedTeamData = new TeamData
                {
                    teamName = previousTeamData.teamName,
                    primaryStoreSize = "" + (Convert.ToInt64(previousTeamData.primaryStoreSize) + Convert.ToInt64(record.PrimaryStoreSize)),
                    numberOfDocs = "" + (Convert.ToInt64(previousTeamData.numberOfDocs) + Convert.ToInt64(record.DocsCount)),
                    totalStoreSize = "" + (Convert.ToInt64(previousTeamData.totalStoreSize) + Convert.ToInt64(record.TotalMemory))
                };

                teamDataDict[indexTeamName] = updatedTeamData;
            }


            return teamDataDict;
        }

    }
}