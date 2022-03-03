using NUnit.Framework;
using PRD_Ordonnanceur.Algorithms;
using PRD_Ordonnanceur.Data;
using PRD_Ordonnanceur.Solution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRD_Ordonnanceur.Algorithms.Tests
{
    [TestFixture()]
    public class AvailableTests
    {
        private Available available = new Available();
        private Job_shop_algorithm job_Shop_Algorithm = new Job_shop_algorithm();
        private SolutionPlanning plannings = new SolutionPlanning();

        [Test()]
        public void FindOperatorForTankTest()
        {
            Assert.True(true);
        }

        [Test()]
        public void FindOperatorTest()
        {
            List<Operator> operators = new();
            List<Operator> operatorsExpected = new ();

            DateTime beginning = new DateTime(2020, 01, 01, 07 ,00 ,00);
            DateTime end = new DateTime(2020, 01, 01, 18, 00, 00);

            TypeMachine competence = TypeMachine.blender;
            List<TypeMachine> competences = new();
            competences.Add(competence);

            uint idOperator1 = 1;
            Operator operator1 = new Operator(beginning,end,null, idOperator1, competences);

            List<Operator> operatorsResult = available.FindOperator(plannings.PlanningOperator,operators, DateTime.Now, DateTime.Now.AddMinutes(10.0), competence);

            // Test sans Operator

            Assert.AreEqual(operatorsExpected,operatorsResult);

            Assert.AreEqual(operatorsExpected.Count, operatorsResult.Count, "FindOperatorTest : nombre d'élement incorrecte");

            // Test avec un Operator sans planning

            operators.Add(operator1);

            operatorsResult = available.FindOperator(plannings.PlanningOperator, operators, DateTime.Now, DateTime.Now.AddMinutes(10.0), competence);

            operatorsExpected.Add(operator1);

            Assert.AreEqual(operatorsExpected, operatorsResult);

            Assert.AreEqual(operatorsExpected.Count, operatorsResult.Count, "FindOperatorTest : nombre d'élement incorrecte");

            // Test avec un Operator avec un planning 

            List<Object> planningDay1 = new();

            DateTime jobtoDoBeginning = new(2020, 01, 01, 07, 05, 0); // JobTODO 1
            DateTime jobtoDoEnd = new(2020, 01, 01, 7, 10, 0);

            DateTime jobForOperatorBeginning = new(2020, 01, 01, 07, 05, 0);
            DateTime jobForOperatorEnd = new(2020, 01, 01, 07, 10, 0);

            DateTime dateTime = new DateTime(2020, 01, 01);

            int numberIdOF = 1;

            planningDay1.Add(dateTime);
            planningDay1.Add(jobtoDoBeginning);
            planningDay1.Add(jobtoDoEnd);
            planningDay1.Add("OPBefore");
            planningDay1.Add(numberIdOF);
            planningDay1.Add(idOperator1);

            plannings.PlanningOperator.Add(planningDay1);

            operatorsResult = available.FindOperator(plannings.PlanningOperator, operators, jobForOperatorBeginning, jobForOperatorEnd, competence);
            operatorsExpected = new();

            Assert.AreEqual(operatorsExpected, operatorsResult, "FindOperatorTest");

            Assert.AreEqual(operatorsExpected.Count, operatorsResult.Count, "FindOperatorTest : nombre d'élement incorrecte");

            //operatorsResult = available.FindOperator(plannings.PlanningOperator, operators, jobtoDoEnd, competence);
            //operatorsExpected.Add(operator1);

            //Assert.AreEqual(operatorsExpected, operatorsResult, "FindOperatorTest"); 

            //Assert.AreEqual(operatorsExpected.Count, operatorsResult.Count, "FindOperatorTest : nombre d'élement incorrecte");

            // Test avec deux Operators sans plannings avec les mêmes competences

            // Test avec deux Operators sans planning avec des competences différentes

            // Test avec deux Operators avec planning avec des competences différentes
        }

        [Test()]
        public void FindMachineForStepTest()
        {
            Assert.True(true);
        }

        [Test()]
        public void FindTankForStepTest()
        {
            Assert.True(true);
        }

        [Test()]
        public void FindConsoForStepTest()
        {
            Assert.True(true);
        }

        [Test()]
        public void FindTimeCleaningTankTest()
        {
            Assert.True(true);
        }
    }
}