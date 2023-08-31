using NUnit.Framework;
using PRD_Ordonnanceur.Data;
using System;
using System.Collections.Generic;

namespace PRD_Ordonnanceur.Algorithms.Tests
{
    /// <summary>
    /// Class for testing the heuristics
    /// </summary>
    [TestFixture()]
    public class HeuristicTests
    {
        [Test()]
        public void Smallest_index_DTITest()
        {
            DateTime earliestDate1 = DateTime.MinValue;
            DateTime earliestDate2 = DateTime.Now;
            DateTime earliestDate3 = DateTime.MaxValue;

            DateTime latestDate = DateTime.Now;

            string numberProduct1 = "1";
            string numberProduct2 = "2";

            DateTime starting_hour = DateTime.Now;
            int next_step = 2;

            OF OF1 = new(1, starting_hour, next_step, new(), earliestDate1, latestDate, numberProduct1);
            OF OF2 = new(2, starting_hour, next_step, new(), earliestDate2, latestDate, numberProduct2);
            OF OF3 = new(3, starting_hour, next_step, new(), earliestDate3, latestDate, numberProduct1);

            List<OF> OFs1 = new()
            {
                OF1,
                OF2,
                OF3
            };

            List<OF> OFs2 = new()
            {
                OF2,
                OF1,
                OF3
            };

            List<OF> OFs3 = new()
            {
                OF2,
                OF3,
                OF1
            };

            int resultIndex = Heuristic.Smallest_index_DTI(OFs1);
            int expectedIndex = 0;

            Assert.AreEqual(expectedIndex, resultIndex);

            resultIndex = Heuristic.Smallest_index_DTI(OFs2);
            expectedIndex = 1;

            Assert.AreEqual(expectedIndex, resultIndex);

            resultIndex = Heuristic.Smallest_index_DTI(OFs3);
            expectedIndex = 2;

            Assert.AreEqual(expectedIndex, resultIndex);
        }

        [Test()]
        public void SortCrescentDtiCrescentDliTest()
        {
            DateTime earliestDate1 = DateTime.MinValue;
            DateTime earliestDate2 = DateTime.Now;
            DateTime earliestDate3 = new(5000, 01, 01);

            DateTime latestDate = DateTime.Now;

            string numberProduct1 = "1";
            string numberProduct2 = "2";

            List<Step> stepSequence = new();

            DateTime starting_hour = DateTime.Now;
            int next_step = 2;

            OF OF1 = new(1, starting_hour, next_step, stepSequence, earliestDate1, latestDate, numberProduct1);
            OF OF2 = new(2, starting_hour, next_step, stepSequence, earliestDate2, latestDate, numberProduct2);
            OF OF3 = new(3, starting_hour, next_step, stepSequence, earliestDate3, latestDate, numberProduct1);

            List<OF> OFs1 = new()
            {
                OF1,
                OF2,
                OF3
            };

            List<OF> OFs2 = new()
            {
                OF2,
                OF1,
                OF3
            };

            List<OF> OFs3 = new()
            {
                OF2,
                OF3,
                OF1
            };

            Heuristic heuristic = new();

            OFs1 = Heuristic.SortCrescentDtiCrescentDli(OFs1);

            Assert.AreEqual(OFs1[0].EarliestDate, OF1.EarliestDate);
            Assert.AreEqual(OFs1[1].EarliestDate, OF2.EarliestDate);
            Assert.AreEqual(OFs1[2].EarliestDate, OF3.EarliestDate);

            OFs2 = Heuristic.SortCrescentDtiCrescentDli(OFs2);

            Assert.AreEqual(OFs2[0].EarliestDate, OF1.EarliestDate);
            Assert.AreEqual(OFs2[1].EarliestDate, OF2.EarliestDate);
            Assert.AreEqual(OFs2[2].EarliestDate, OF3.EarliestDate);

            OFs3 = Heuristic.SortCrescentDtiCrescentDli(OFs3);

            Assert.AreEqual(OFs3[0].EarliestDate, OF1.EarliestDate);
            Assert.AreEqual(OFs3[1].EarliestDate, OF2.EarliestDate);
            Assert.AreEqual(OFs3[2].EarliestDate, OF3.EarliestDate);
        }
    }
}