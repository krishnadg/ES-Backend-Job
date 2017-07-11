using System;
using System.Diagnostics;
using Xunit;
using Xunit.Sdk;
using Nest;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

using ESClassLib;

namespace ESTests
{

    public abstract class IndexRecordParserTestBase : IDisposable    
    {   
        
        protected Dictionary<string, TeamData> expectedTeamDataDict;

        protected IndexRecordParser sut;
        protected IndexRecordParserTestBase()
        {
            // Do "global" initialization here; Called before every test method.
            expectedTeamDataDict = new Dictionary<string, TeamData>();
        }

        public void Dispose()
        {
            // Do "global" teardown here; Called after every test method.

        }
    } 


    public class IndexRecordParserTest : IndexRecordParserTestBase 
    {


        /*Test Helper method */
        public void AreSameDictionaries(Dictionary<string, TeamData> expected, Dictionary<string, TeamData> result)
        {  
            var countMatches = expected.Count == result.Count;
            Assert.True(countMatches, String.Format("expected count {0}, got count {1}", expected.Count, result.Count ));

            
            foreach (KeyValuePair<string, TeamData> pair in expected)
            {
                string teamName = pair.Key;
                bool hasTeamName = result.ContainsKey(teamName);
                Assert.True(hasTeamName, String.Format("expected team name {0}, got didn't have", teamName));

                AreSameTeamData(pair.Value, result[teamName]);
            }

        }

        private void AreSameTeamData(TeamData expected, TeamData result)
        {
            bool sameTeamname = expected.teamName == result.teamName;
            Assert.True(sameTeamname, String.Format("expected team name {0}, found team name {1}", expected.teamName, result.teamName));

            bool samePrimaryStorageSize = expected.primaryStoreSize == result.primaryStoreSize;
            Assert.True(samePrimaryStorageSize, String.Format("expected primary store size {0}, found primary store size {1}", expected.primaryStoreSize, result.primaryStoreSize));

            bool sameTotalStoreSize = expected.totalStoreSize == result.totalStoreSize;
            Assert.True(sameTotalStoreSize, String.Format("expected total store size {0}, found total store size {1}", expected.totalStoreSize, result.totalStoreSize));

            bool sameNumberOfDocs = expected.numberOfDocs == result.numberOfDocs;
            Assert.True(sameNumberOfDocs, String.Format("expected number of docs: {0}, found number of docs: {1}", expected.numberOfDocs, result.numberOfDocs));



        }


        [Fact]
        public void ParseIndexRecordIntoDict_EmptySourceDictionary_Return1TeamData()
        {
            //ARRANGE
            sut = new IndexRecordParser();
            
            Dictionary<string, TeamData> result = new Dictionary<string, TeamData>();
            //stm_uiiviewer_flume:tomcatlog_index-2017-07-08_1    1   1    23048   0    36.8mb   18.4mb 
            CatIndicesRecord arg = new CatIndicesRecord
            {
                Index = "stm_uiiviewer_flume:tomcatlog_index-2017-07-08_1",
                DocsCount = "23048", 
                StoreSize = "3688800",
                PrimaryStoreSize = "1844400",
                TotalMemory = "3688800"
            };

            
            var expectedTeamData = new TeamData
            {
                teamName = "stm",
                numberOfDocs = "23048",
                primaryStoreSize = "1844400",
                totalStoreSize = "3688800"
                
            };

            expectedTeamDataDict.Add("stm", expectedTeamData);
            //ACT
            
            result = sut.ParseIndexRecordIntoDict(result, arg);

            //ASSERT
            AreSameDictionaries(expectedTeamDataDict, result);

        }


         [Fact]
        public void ParseIndexRecordIntoDict_NonEmptySourceDictionary_Return2TeamData()
        {
            //ARRANGE
            sut = new IndexRecordParser();
            
            Dictionary<string, TeamData> result = new Dictionary<string, TeamData>();
            var preexisitngTeamData = new TeamData
            {
                teamName = "team1",
                numberOfDocs = "123",
                primaryStoreSize = "213123",
                totalStoreSize = "426000"
            };
            result.Add("team1", preexisitngTeamData);


            //ngenp2ac_masapps_applogs_index-2017-06-17_1    1   1     336     0   1mb     550.6kb 
            CatIndicesRecord arg = new CatIndicesRecord
            {
                Index = "ngenp2ac_masapps_applogs_index-2017-06-17_1 ",
                DocsCount = "336", 
                StoreSize = "3688800",
                PrimaryStoreSize = "1844400",
                TotalMemory = "500000",
                
            };

            
            var expectedTeamData = new TeamData
            {
                teamName = "ngenp2ac",
                numberOfDocs = "336",
                primaryStoreSize = "1844400",
                totalStoreSize = "500000"
                
            };
            expectedTeamDataDict.Add("team1", preexisitngTeamData);
            expectedTeamDataDict.Add("ngenp2ac", expectedTeamData);
            

            //ACT

            result = sut.ParseIndexRecordIntoDict(result, arg);


            //ASSERT

            AreSameDictionaries(expectedTeamDataDict, result);
        }

         [Fact]
        public void ParseIndexRecordIntoDict_DuplicateTeamInDictionary_Return1UpdatedTeamData()
        {
            //ARRANGE
            sut = new IndexRecordParser();


            Dictionary<string, TeamData> result = new Dictionary<string, TeamData>();
            //wcm_publisher_tridion_index-2017-07-10_1      1   1       4999    0    5.7mb    3.8mb 
            var preexisitngTeamData = new TeamData
            {
                teamName = "wcm",
                numberOfDocs = "4999",
                primaryStoreSize = "380000",
                totalStoreSize = "570000"
            };
            result.Add("wcm", preexisitngTeamData);


            CatIndicesRecord arg = new CatIndicesRecord
            {
                Index = "wcm_some_other_index-2017-07-10_1",
                DocsCount = "1000", 
                PrimaryStoreSize = "20000",
                TotalMemory = "40000",
            };



            var expectedUpdatedTeamData = new TeamData
            {
                teamName = "wcm",
                numberOfDocs = "5999",
                primaryStoreSize = "400000",
                totalStoreSize = "610000"
                
            };
            expectedTeamDataDict.Add("wcm", expectedUpdatedTeamData);

            //ACT

            result = sut.ParseIndexRecordIntoDict(result, arg);


            //ASSERT
            AreSameDictionaries(expectedTeamDataDict, result);


        }
    }
}
         