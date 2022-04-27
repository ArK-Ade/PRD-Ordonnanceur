using NUnit.Framework;
using PRD_Ordonnanceur.Data;
using PRD_Ordonnanceur.Solution;
using System;
using System.Collections.Generic;

namespace PRD_Ordonnanceur.Algorithms.Tests
{
    /// <summary>
    /// class that allows to test the methods that check if the resources are available
    /// </summary>
    [TestFixture()]
    public class AvailableTests
    {
        private readonly List<SolutionPlanning> solutionPlannings = new();
        private readonly SolutionPlanning plannings = new();
        private Operator operator1;
        private readonly List<Operator> operators = new();
        private readonly int idOperator1 = 1;
        private readonly List<TypeMachine> listSkillOperator1 = new();
        private readonly DateTime jobBeginningTime1 = new(2020, 01, 01, 07, 00, 00);
        private readonly DateTime jobEndTime1 = new(2020, 01, 01, 18, 00, 00);
        private Machine machine1;
        private readonly List<Machine> machines = new();
        private readonly int idMachine1 = 1;
        private readonly TypeMachine typeMachine1 = TypeMachine.blender;
        private TimeSpan durationCleaning1;
        private readonly int idOF1 = 1;

        [SetUp]
        public void SetupBeforeEachTest()
        {
        }

        [TearDown]
        public void TearDownAfterEachTest()
        {
            solutionPlannings.Clear();
            operators.Clear();
            listSkillOperator1.Clear();
            machines.Clear();
        }

        [Test()]
        public void FindOperator()
        {
            List<Operator> operatorsExpected = new();
            DateTime startJob = new(2022, 03, 30, 07, 00, 00);
            TypeMachine competence = TypeMachine.blender;
            listSkillOperator1.Add(competence);
            operator1 = new Operator(jobBeginningTime1, jobEndTime1, null, idOperator1, listSkillOperator1);
            operators.Add(operator1);
            List<Operator> operatorsResult = RessourceAvailable.FindOperator(solutionPlannings, operators, startJob, startJob.AddMinutes(10.0), competence);
            operatorsExpected.Add(operator1);
            Assert.AreEqual(operatorsExpected, operatorsResult);
            Assert.AreEqual(operatorsExpected.Count, operatorsResult.Count);

            operators.Clear();
        }

        [Test()]
        public void FindZeroOperatorIfHeIsNotAvailable()
        {
            List<Operator> operatorsExpected = new();
            TypeMachine competence = TypeMachine.blender;
            listSkillOperator1.Add(competence);
            operator1 = new Operator(jobBeginningTime1, jobEndTime1, null, idOperator1, listSkillOperator1);
            operators.Add(operator1);

            DateTime jobDoneBeginning = new(2020, 01, 01, 07, 00, 00), jobDoneEnd = new(2020, 01, 01, 07, 05, 00);

            List<Object> planningDay1 = new();

            planningDay1.Add(jobBeginningTime1);
            planningDay1.Add(jobDoneBeginning);
            planningDay1.Add(jobDoneEnd);
            planningDay1.Add("OPBefore");
            planningDay1.Add(idOF1);
            planningDay1.Add(idOperator1);

            plannings.PlanningOperator.Add(planningDay1);
            solutionPlannings.Add(plannings);

            List<Operator> operatorsResult = RessourceAvailable.FindOperator(solutionPlannings, operators, jobDoneBeginning, jobDoneEnd, competence);
            Assert.AreEqual(operatorsExpected, operatorsResult);
            Assert.AreEqual(operatorsExpected.Count, operatorsResult.Count);
        }

        [Test()]
        public void FindZeroMachineIfNoMachineGiven()
        {
            durationCleaning1 = new(0, 10, 0);
            machine1 = new(typeMachine1, null, durationCleaning1, idMachine1);

            List<Machine> machineResult = RessourceAvailable.FindMachineForStep(solutionPlannings, machines, jobBeginningTime1, jobEndTime1, typeMachine1);
            Assert.IsEmpty(machineResult);
        }

        [Test()]
        public void FindOneMachine()
        {
            machine1 = new(typeMachine1, null, durationCleaning1, idMachine1);
            machines.Add(machine1);

            List<Machine> machineResult = RessourceAvailable.FindMachineForStep(solutionPlannings, machines, jobBeginningTime1, jobEndTime1, typeMachine1);
            List<Machine> machineExpected = new();
            machineExpected.Add(machine1);

            Assert.AreEqual(machineExpected, machineResult);
            Assert.AreEqual(machineExpected.Count, machineResult.Count);
        }

        [Test()]
        public void FindZeroMachineIfItIsNotAvailable()
        {
            machine1 = new(typeMachine1, null, durationCleaning1, idMachine1);
            List<Object> planningDay1 = new();

            DateTime jobtoDoBeginning = new(2020, 01, 01, 07, 05, 0); // JobTODO 1
            DateTime jobtoDoEnd = new(2020, 01, 01, 7, 10, 0);
            DateTime dateTime = new(2020, 01, 01);

            planningDay1.Add(dateTime);
            planningDay1.Add(jobtoDoBeginning);
            planningDay1.Add(jobtoDoEnd);
            planningDay1.Add(idOF1);
            planningDay1.Add(idOperator1);
            planningDay1.Add(idOperator1);
            planningDay1.Add(idMachine1);

            plannings.PlanningMachine.Add(planningDay1);
            solutionPlannings.Add(plannings);
            machines.Add(machine1);

            List<Machine> listMachineResult = RessourceAvailable.FindMachineForStep(solutionPlannings, machines, jobtoDoBeginning, jobtoDoEnd, typeMachine1);
            Assert.IsEmpty(listMachineResult);
        }

        [Test()]
        public void FindZeroTank()
        {
            Assert.True(true);
        }

        [Test()]
        public void FindOneTankWithoutSchedule()
        {
            Assert.True(true);
        }

        [Test()]
        public void FindOneTankWithSchedule()
        {
            Assert.True(true);
        }

        [Test()]
        public void FindTwoTankWithoutSchedule()
        {
            Assert.True(true);
        }

        [Test()]
        public void FindTwoTankWithSchedule()
        {
            Assert.True(true);
        }

        [Test()]
        public void FindZeroConso()
        {
            Assert.True(true);
        }

        [Test()]
        public void FindAllConso()
        {
            Assert.True(true);
        }

        [Test()]
        public void FindAllConsoWithDelay()
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