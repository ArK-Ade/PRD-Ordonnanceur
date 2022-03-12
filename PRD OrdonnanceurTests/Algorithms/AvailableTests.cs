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
        private List<Operator> operators = new();
        private uint idOperator1 = 1, id2Operator = 2;
        private List<TypeMachine> listSkillOperator1 = new(), listSkillOperator2 = new();

        private DateTime jobBeginningTime1 = new(2020, 01, 01, 07, 00, 00), jobEndTime1 = new DateTime(2020, 01, 01, 18, 00, 00);
        private DateTime jobBeginningTime2, jobEndTime2;


        private Machine machine1, machine2;
        private List<Machine> machines = new();
        private int idMachine1, idMachine2;
        private TypeMachine typeMachine1 = TypeMachine.blender, typeMachine2;
        private List<Calendar> calendars1, calendars2;
        private TimeSpan durationCleaning1, durationCleaning2;

        private Tank tank1, tank2;
        private List<Tank> tanks;
        private int typeTank1, typeTank2;
        private int idTank1, idTank2;

        private Consumable consumable1, consumable2;
        private List<Consumable> consumables;
        private int idConsumable1, idConsumable2;
        private int quantityAvailable1, quantityAvailable2;

        private int idOF1 = 1, idOF2 = 2;

        [Test()]
        public void FindOperatorIfNoOperatorGiven()
        {
            TypeMachine competence = TypeMachine.blender;
            List<Operator> operatorsResult = available.FindOperator(plannings.PlanningOperator, operators, DateTime.Now, DateTime.Now.AddMinutes(10.0), competence);
            Assert.IsEmpty(operatorsResult);
        }

        [Test()]
        public void FindOperator()
        {
            List<Operator> operatorsExpected = new();
            TypeMachine competence = TypeMachine.blender;
            listSkillOperator1.Add(competence);
            operator1 = new Operator(jobBeginningTime1, jobEndTime1, null, idOperator1, listSkillOperator1);
            operators.Add(operator1);
            List<Operator> operatorsResult = available.FindOperator(plannings.PlanningOperator, operators, DateTime.Now, DateTime.Now.AddMinutes(10.0), competence);
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

            DateTime jobDoneBeginning = new(2020, 01, 01, 07, 00, 00), jobDoneEnd = new DateTime(2020, 01, 01, 07, 05, 00);

            List<Object> planningDay1 = new();

            planningDay1.Add(jobBeginningTime1);
            planningDay1.Add(jobDoneBeginning);
            planningDay1.Add(jobDoneEnd);
            planningDay1.Add("OPBefore");
            planningDay1.Add(idOF1);
            planningDay1.Add(idOperator1);

            plannings.PlanningOperator.Add(planningDay1);

            List<Operator> operatorsResult = available.FindOperator(plannings.PlanningOperator, operators, jobDoneBeginning, jobDoneEnd, competence);
            Assert.AreEqual(operatorsExpected, operatorsResult);
            Assert.AreEqual(operatorsExpected.Count, operatorsResult.Count);

            operators.Clear();
            plannings.PlanningOperator.Clear();
        }

        [Test()]
        public void FindZeroMachineIfNoMachineGiven()
        {
            durationCleaning1 = new(0, 10, 0);
            machine1 = new(typeMachine1, null, durationCleaning1, idMachine1);

            List<Machine> machineResult = available.FindMachineForStep(plannings.PlanningMachine, machines, jobBeginningTime1, jobEndTime1, typeMachine1);
            Assert.IsEmpty(machineResult);
        }

        [Test()]
        public void FindOneMachine()
        {
            machine1 = new(typeMachine1, null, durationCleaning1, idMachine1);
            machines.Add(machine1);

            List<Machine> machineResult = available.FindMachineForStep(plannings.PlanningMachine, machines, jobBeginningTime1, jobEndTime1, typeMachine1);
            List<Machine> machineExpected = new();
            machineExpected.Add(machine1);

            Assert.AreEqual(machineExpected, machineResult);
            Assert.AreEqual(machineExpected.Count, machineResult.Count);

            machines.Clear();
        }

        [Test()]
        public void FindZeroMachineIfItIsNotAvailable()
        {
            machine1 = new(typeMachine1, null, durationCleaning1, idMachine1);
            List<Object> planningDay1 = new();

            DateTime jobtoDoBeginning = new(2020, 01, 01, 07, 05, 0); // JobTODO 1
            DateTime jobtoDoEnd = new(2020, 01, 01, 7, 10, 0);
            DateTime dateTime = new DateTime(2020, 01, 01);


            planningDay1.Add(dateTime);
            planningDay1.Add(jobtoDoBeginning);
            planningDay1.Add(jobtoDoEnd);
            planningDay1.Add(idOF1);
            planningDay1.Add(idOperator1);
            planningDay1.Add(idMachine1);

            plannings.PlanningMachine.Add(planningDay1);
            machines.Add(machine1);

            List<Machine> listMachineResult = available.FindMachineForStep(plannings.PlanningMachine, machines, jobtoDoBeginning, jobtoDoEnd, typeMachine1);
            Assert.IsEmpty(listMachineResult);

            plannings.PlanningMachine.Clear();
            machines.Clear();
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