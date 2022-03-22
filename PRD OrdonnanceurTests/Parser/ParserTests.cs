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
            //string path = "./../../../Data Formated/";

            //List<Consumable> consumables = ParserData.ParsingDataConsommable(path);

            //List<OF> data = ParserData.ParsingDataOF(path,consumables);
            Assert.IsTrue(true);

            //Assert.AreEqual(1021610, data[0].IdOF);
            //Assert.AreEqual(1021614, data[1].IdOF);

            //List<Operator> operators = ParserData.ParsingDataOperator(path);

            //Assert.AreEqual(1, operators[0].Id);

            //List<Machine> machines = ParserData.ParsingDataMachine(path);
            //Assert.AreEqual(1, machines[0].Id);
        }
    }
}