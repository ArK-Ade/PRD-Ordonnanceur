using NUnit.Framework;
using PRD_Ordonnanceur.Data;
using PRD_Ordonnanceur.Solution;
using System;
using System.Collections.Generic;

namespace PRD_Ordonnanceur.Algorithms.Tests
{
    [TestFixture()]
    public class AvailableTests
    {
        private Available available = new Available();
        private SolutionPlanning plannings = new SolutionPlanning();

        private Operator operator1, operator2;
        private int idOperator1, id2Operator;
        private List<TypeMachine> listSkillOperator1, listSkillOperator2;
        private DateTime jobBeginningTime, jobEndTime;


        [Test()]
        public void FindOperatorForTankTest()
        {
            Assert.True(true);
        }

        [Test()]
        public void FindOperatorTest()
        {
            List<Operator> operators = new();
            List<Operator> operatorsExpected = new();

            DateTime beginning = new DateTime(2020, 01, 01, 07, 00, 00);
            DateTime end = new DateTime(2020, 01, 01, 18, 00, 00);

            TypeMachine competence = TypeMachine.blender;
            List<TypeMachine> competenceOperator1 = new();
            competenceOperator1.Add(competence);

            uint idOperator1 = 1;
            Operator operator1 = new Operator(beginning, end, null, idOperator1, competenceOperator1);

            List<Operator> operatorsResult = available.FindOperator(plannings.PlanningOperator, operators, DateTime.Now, DateTime.Now.AddMinutes(10.0), competence);

            // Test sans Operator

            Assert.AreEqual(operatorsExpected, operatorsResult);

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
            operatorsExpected.Clear();

            Assert.AreEqual(operatorsExpected, operatorsResult, "FindOperatorTest");

            Assert.AreEqual(operatorsExpected.Count, operatorsResult.Count, "FindOperatorTest : nombre d'élement incorrecte");

            // Test avec deux Operators sans plannings avec les mêmes competences

            planningDay1.Clear();
            plannings.PlanningOperator.Clear();

            uint idOperator2 = 2;
            Operator operator2 = new(beginning, end, null, idOperator2, competenceOperator1);

            operators.Add(operator1);
            operators.Add(operator2);

            operatorsResult = available.FindOperator(plannings.PlanningOperator, operators, jobForOperatorBeginning, jobForOperatorEnd, competence);
            operatorsExpected.Clear();

            operatorsExpected.Add(operator1);
            operatorsExpected.Add(operator2);

            Assert.AreEqual(operatorsExpected, operatorsResult, "FindOperatorTest");

            Assert.AreEqual(operatorsExpected.Count, operatorsResult.Count, "FindOperatorTest : nombre d'élement incorrecte");

            // Test avec deux Operators sans planning avec des competences différentes

            List<TypeMachine> competenceOperator2 = new();

            operator1 = new Operator(beginning, end, null, idOperator1, competenceOperator1);
            operator2 = new(beginning, end, null, idOperator2, competenceOperator2);

            competenceOperator1.Clear();

            competence = TypeMachine.blender;
            TypeMachine competence2 = TypeMachine.cleaning;
            operator1.CleanSkill();
            operator1.AddSkill(competence);

            operator2.CleanSkill();
            operator2.AddSkill(competence2);

            operators.Clear();
            operators.Add(operator1);
            operators.Add(operator2);

            operatorsExpected.Clear();
            operatorsExpected.Add(operator1);

            operatorsResult = available.FindOperator(plannings.PlanningOperator, operators, jobForOperatorBeginning, jobForOperatorEnd, TypeMachine.blender);

            Assert.AreEqual(operatorsExpected, operatorsResult, "FindOperatorTest");

            Assert.AreEqual(operatorsExpected.Count, operatorsResult.Count, "FindOperatorTest : nombre d'élement incorrecte");

            // Test avec deux Operators avec planning avec des competences différentes

            planningDay1 = new();

            jobtoDoBeginning = new(2020, 01, 01, 07, 05, 0); // JobTODO 1
            jobtoDoEnd = new(2020, 01, 01, 7, 10, 0);

            jobForOperatorBeginning = new(2020, 01, 01, 07, 05, 0);
            jobForOperatorEnd = new(2020, 01, 01, 07, 10, 0);

            dateTime = new DateTime(2020, 01, 01);

            numberIdOF = 1;

            planningDay1.Add(dateTime);
            planningDay1.Add(jobtoDoBeginning);
            planningDay1.Add(jobtoDoEnd);
            planningDay1.Add("OPBefore");
            planningDay1.Add(numberIdOF);
            planningDay1.Add(idOperator1);

            plannings.PlanningOperator.Add(planningDay1);

            operatorsResult = available.FindOperator(plannings.PlanningOperator, operators, jobForOperatorBeginning, jobForOperatorEnd, TypeMachine.blender);

            operatorsExpected.Clear();

            Assert.AreEqual(operatorsExpected, operatorsResult, "FindOperatorTest");

            Assert.AreEqual(operatorsExpected.Count, operatorsResult.Count, "FindOperatorTest : nombre d'élement incorrecte");
        }

        [Test()]
        public void FindMachineForStepTest()
        {

            int idMachine1 = 1, idMachine2 = 2;
            Calendar calendarMachine1 = new(), calendarMachine2 = new();
            TimeSpan timeCleaningMachine1 = new(0, 10, 0), timeCleaningMachine2 = new(0, 15, 0);
            TypeMachine typeMachine1 = TypeMachine.blender, typeMachine2 = TypeMachine.cleaning;

            Machine machine1 = new(typeMachine1, calendarMachine1, timeCleaningMachine1, idMachine1), machine2 = new(typeMachine2, calendarMachine2, timeCleaningMachine2, idMachine2);

            List<Machine> listMachineExpected = new(), listMachineResult, machines = new();

            // Case One : Pas de planning, pas de machines
            listMachineResult = available.FindMachineForStep(plannings.PlanningMachine, machines, DateTime.Now, DateTime.Now, typeMachine1);

            Assert.AreEqual(listMachineExpected.Count, listMachineResult.Count, "qqch");

            // Case two : Une machine sans planning

            machines.Add(machine1);
            listMachineResult = available.FindMachineForStep(plannings.PlanningMachine, machines, DateTime.Now, DateTime.Now, typeMachine1);

            listMachineExpected.Add(machine1);

            Assert.AreEqual(listMachineExpected, listMachineResult, "qqch");

            // Case Three : Une machine avec un planning

            List<Object> planningDay1 = new();

            DateTime jobtoDoBeginning = new(2020, 01, 01, 07, 05, 0); // JobTODO 1
            DateTime jobtoDoEnd = new(2020, 01, 01, 7, 10, 0);
            DateTime dateTime = new DateTime(2020, 01, 01);

            int numberIdOF = 1, numberIdOperator = 1, numberIdMachine = 1;

            planningDay1.Add(dateTime);
            planningDay1.Add(jobtoDoBeginning);
            planningDay1.Add(jobtoDoEnd);
            planningDay1.Add(numberIdOF);
            planningDay1.Add(numberIdOperator);
            planningDay1.Add(numberIdMachine);

            plannings.PlanningMachine.Add(planningDay1);

            listMachineResult = available.FindMachineForStep(plannings.PlanningMachine, machines, jobtoDoBeginning, jobtoDoEnd, typeMachine1);
            listMachineExpected.Clear();

            Assert.AreEqual(listMachineExpected.Count, listMachineResult.Count, "qqch");

            // Case Four : Une machine avec un planning et un calendrier

            // Case Five : deux machines avec un planning avec des types differents


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