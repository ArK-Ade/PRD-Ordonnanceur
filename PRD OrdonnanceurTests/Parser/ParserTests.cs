using NUnit.Framework;
using PRD_Ordonnanceur.Data;
using PRD_Ordonnanceur.Parser;
using System.Collections.Generic;

namespace PRD_OrdonnanceurTests.Parser
{
    [TestFixture()]
    public class ParserTests
    {
        [Test()]
        public void ParsingTest()
        {
            string path = "C:\\Travail\\DI5\\PR&D\\Data Formated\\";

            //dataParsed = parser.ParsingData(path);

            List<Consumable> consumables = ParserData.ParsingDataConsommable(path);

            List<OF> data = ParserData.ParsingDataOF(path,consumables);
            Assert.AreEqual(1021610, data[0].IdOF);
            Assert.AreEqual(1021614, data[1].IdOF);
        }
    }
}
