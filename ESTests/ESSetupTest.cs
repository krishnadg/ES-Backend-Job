using System;
using Xunit;

namespace ESTests
{
    public class ESSetupTest
    {
        [Fact]
        public void TestDefaultESSetup()
        {
            var setup = new ESSetup();
            ESSetup.AddIndex("datalend");
            ESSetup.AddIndex("datalens");
            ESSetup.AddIndex("ddos");
            ESSetup.AddIndex("ebs");
            ESSetup.AddIndex("npm");

        }
    }
}
