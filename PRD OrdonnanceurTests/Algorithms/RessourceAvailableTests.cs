﻿using NUnit.Framework;
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
    public class RessourceAvailableTests
    {
        private readonly List<SolutionPlanning> solutionPlannings = new();
        private readonly SolutionPlanning plannings = new();
        private Operator operatorA = new();
        private readonly List<Operator> operators = new();
        private readonly int idOperator1 = 1;
        private readonly List<TypeMachine> listSkillOperator1 = new();
        private readonly DateTime jobBeginningTime1 = new(2020, 01, 01, 07, 00, 00);
        private readonly DateTime jobEndTime1 = new(2020, 01, 01, 18, 00, 00);
        private Machine machine1 = new();
        private readonly List<Machine> machines = new();
        private readonly int idMachine1 = 1;
        private readonly TypeMachine typeMachine1 = TypeMachine.blender;
        private TimeSpan durationCleaning1;
        private readonly int idOF1 = 1;
        private readonly Tank tank1 = new();
        private readonly int idTank1 = 1;
        private readonly int typeTank1 = 1;
        private readonly List<Tank> tanks = new();
        private Consumable consumable1 = new();
        private readonly List<Consumable> consumables = new();

        [SetUp]
        public void SetupBeforeEachTest()
        {
            operatorA.StartOfShiftSchedule = jobBeginningTime1;
            operatorA.EndOfShiftSchedule = jobEndTime1;

            consumable1.QuantityAvailable = 10.0;
            consumable1.Id = 1;
        }

        [TearDown]
        public void TearDownAfterEachTest()
        {
            solutionPlannings.Clear();
            operators.Clear();
            listSkillOperator1.Clear();
            machines.Clear();
            tanks.Clear();
            consumables.Clear();
        }

        [Test()]
        public void FindOneOperatorReturnOneOperator()
        {
            List<Operator> operatorsExpected = new();
            DateTime startJob = new(2022, 03, 30, 07, 00, 00);
            TypeMachine competence = TypeMachine.blender;
            listSkillOperator1.Add(competence);
            operatorA = new Operator(jobBeginningTime1, jobEndTime1, null, idOperator1, listSkillOperator1);
            operators.Add(operatorA);
            List<Operator> operatorsResult = RessourceAvailable.FindOperator(solutionPlannings, operators, startJob, startJob.AddMinutes(10.0), competence);
            operatorsExpected.Add(operatorA);
            Assert.AreEqual(operatorsExpected, operatorsResult);
            Assert.AreEqual(operatorsExpected.Count, operatorsResult.Count);

            operators.Clear();
        }

        [Test()]
        public void FindOperator_ReturnsCorrectOperator()
        {
            // Arrange
            List<Operator> operatorsExpected = new List<Operator>();
            DateTime startJob = new DateTime(2022, 03, 30, 07, 00, 00);
            TypeMachine competence = TypeMachine.cleaning; // Assuming TypeMachine is an enum

            // Create a list to store the skills of operator1
            List<TypeMachine> listSkillOperator1 = new()
            {
                competence
            };

            // Create operatorA
            Operator operatorA = new Operator(jobBeginningTime1, jobEndTime1, null, idOperator1, listSkillOperator1);

            // Create a list of operators and add operatorA to it
            List<Operator> operators = new()
            {
                operatorA
            };

            // Act
            List<Operator> operatorsResult = RessourceAvailable.FindOperator(solutionPlannings, operators, startJob, startJob.AddMinutes(10.0), competence);

            // Assert
            operatorsExpected.Add(operatorA);
            Assert.AreEqual(operatorsExpected.Count, operatorsResult.Count);
        }

        [Test()]
        public void FindZeroOperatorIfHeIsNotAvailable()
        {
            List<Operator> operatorsExpected = new();
            TypeMachine competence = TypeMachine.blender;
            listSkillOperator1.Add(competence);
            operatorA = new Operator(jobBeginningTime1, jobEndTime1, null, idOperator1, listSkillOperator1);
            operators.Add(operatorA);

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
        public void FindZeroTankIfNoMachineGiven()
        {
            List<Tank> tanks = new();
            List<Tank> listTankResult = RessourceAvailable.FindTankForStep(solutionPlannings, tanks, jobBeginningTime1, jobEndTime1);
            Assert.IsEmpty(listTankResult);
        }

        [Test()]
        public void FindZeroTankIfItIsNotAvailable()
        {
            tank1.IdTank = idTank1;
            tank1.TypeTank = typeTank1;

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
            planningDay1.Add(idTank1);

            plannings.PlanningTank.Add(planningDay1);
            solutionPlannings.Add(plannings);
            tanks.Add(tank1);

            List<Tank> listTankResult = RessourceAvailable.FindTankForStep(solutionPlannings, tanks, jobtoDoBeginning, jobtoDoEnd);
            Assert.IsEmpty(listTankResult);
        }

        [Test()]
        public void FindOneTankIfIsAvailableWithoutSchedule()
        {
            tank1.IdTank = idTank1;
            tank1.TypeTank = typeTank1;
            tanks.Add(tank1);
            List<Tank> listTankResult = RessourceAvailable.FindTankForStep(solutionPlannings, tanks, jobBeginningTime1, jobEndTime1);
            List<Tank> listTankExpected = new()
            {
                tank1
            };
            Assert.AreEqual(listTankExpected,listTankResult);
        }

            [Test()]
        public void FindOneTankIfIsAvailableWithSchedule()
        {
            tank1.IdTank = idTank1;
            tank1.TypeTank = typeTank1;

            List<Object> planningDay1 = new();

            DateTime jobtoDoBeginningFreeTime = new(2020, 01, 01, 07, 00, 0); // JobTODO 1
            DateTime jobtoDoEndFreeTime = new(2020, 01, 01, 7, 05, 0);

            DateTime jobtoDoBeginning = new(2020, 01, 01, 07, 05, 0); // JobTODO 1
            DateTime jobtoDoEnd = new(2020, 01, 01, 7, 10, 0);
            DateTime dateTime = new(2020, 01, 01);

            planningDay1.Add(dateTime);
            planningDay1.Add(jobtoDoBeginning);
            planningDay1.Add(jobtoDoEnd);
            planningDay1.Add(idOF1);
            planningDay1.Add("");
            planningDay1.Add(idTank1);

            plannings.PlanningTank.Add(planningDay1);
            solutionPlannings.Add(plannings);
            tanks.Add(tank1);

            List<Tank> listTankResult = RessourceAvailable.FindTankForStep(solutionPlannings, tanks, jobtoDoBeginningFreeTime, jobtoDoEndFreeTime);
            List<Tank> listTankExpected = new()
            {
                tank1
            };
            Assert.AreEqual(listTankExpected, listTankResult);
        }

        [Test()]
        public void FindZeroConsoIfTheQuantityRequiredIsTooHigh()
        {
            List<Object> planningDay1 = new();
            List<double> listQuantityUsed = new()
            {
                12.0
            };

            DateTime dateTime = new(2020, 01, 01);

            planningDay1.Add(dateTime);
            planningDay1.Add(consumable1.Id);
            planningDay1.Add(11.0);

            plannings.PlanningCons.Add(planningDay1);
            solutionPlannings.Add(plannings);
            consumables.Add(consumable1);

            bool consoIsAvailable = RessourceAvailable.FindConsoForStep(plannings.PlanningCons, consumables, jobBeginningTime1, consumables, listQuantityUsed);
            Assert.False(consoIsAvailable);
        }

        [Test()]
        public void FindConsoIfThereIsEnough()
        {
            List<Object> planningDay1 = new();
            List<double> listQuantityUsed = new()
            {
                12.0
            };

            DateTime dateTime = new(2020, 01, 01);

            planningDay1.Add(dateTime);
            planningDay1.Add(consumable1.Id);
            planningDay1.Add(10.0);

            plannings.PlanningCons.Add(planningDay1);
            solutionPlannings.Add(plannings);
            consumables.Add(consumable1);

            bool consoIsAvailable = RessourceAvailable.FindConsoForStep(plannings.PlanningCons, consumables, jobBeginningTime1, consumables, listQuantityUsed);
            Assert.True(consoIsAvailable);
        }
    }

}