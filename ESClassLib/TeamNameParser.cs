
using System;
using Nest;
using System.Collections.Generic;
using System.Linq;


namespace ESClassLib
{

    public class TeamNameParser
    {

        string teamName;
        public TeamNameParser()
        {

        }


        //May need to add additional logic here later
        public string GetTeamName(string fullIndex)
        {

            teamName = fullIndex;
            
            int indexOfUnderscore = fullIndex.IndexOf('_');

            if (indexOfUnderscore > 0)
            {
                teamName = fullIndex.Substring(0, indexOfUnderscore);
            }

            
            return teamName;
        }



    }

}