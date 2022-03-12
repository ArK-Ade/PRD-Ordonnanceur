using NUnit.Framework;
using PRD_Ordonnanceur.Data;
using PRD_Ordonnanceur.Parser;
using PRD_Ordonnanceur.Solution;
using System;
using System.Collections.Generic;

namespace PRD_Ordonnanceur.Algorithms.Tests
{
    [TestFixture()]
    public class Job_shop_algorithmTests
    {
        [Test()]
        public void step_algorithmTest()
        {
            Operator operator1;
            List<Operator> operators = new();
            uint idOperator1 = 1;
            List<TypeMachine> listSkillOperator1 = new(), listSkillOperator2 = new();
            TypeMachine typeMachine = TypeMachine.blender;
            listSkillOperator1.Add(typeMachine);
            DateTime jobBeginningTime1 = new(2020, 01, 01, 07, 00, 00), jobEndTime1 = new DateTime(2020, 01, 01, 18, 00, 00);

            operator1 = new(jobBeginningTime1, jobEndTime1, null, idOperator1, listSkillOperator1);

            operators.Add(operator1);

            Machine machine1;
            List<Machine> machines = new();
            int idMachine1 = 1;
            TypeMachine typeMachine1 = TypeMachine.blender;
            TimeSpan durationCleaning1;

            durationCleaning1 = new(0, 10, 0);
            machine1 = new(typeMachine1, null, durationCleaning1, idMachine1);
            machines.Add(machine1);

            Tank tank1;
            List<Tank> tanks = new();
            int typeTank1 = 1;
            int idTank1 = 1;

            tank1 = new(idTank1, typeTank1);
            tanks.Add(tank1);

            Consumable consumable1;
            List<Consumable> consumables = new();
            int idConsumable1 = 1;
            int quantityAvailable1 = 10;

            consumable1 = new(idConsumable1, quantityAvailable1, null, DateTime.MinValue);
            consumables.Add(consumable1);

            Step step;
            int idStep = 1;
            TypeMachine type = TypeMachine.blender;
            Duration duration = new(new(00, 10, 00), new(00, 10, 00), new(00, 10, 00));
            DateTime durationMax = DateTime.MaxValue;
            bool reportable = false;
            List<int> quantityNeeded = new();
            quantityNeeded.Add(5);


            step = new(idStep, type, duration, durationMax, reportable, consumables, quantityNeeded);

            OF oF;
            int idOF = 1;

            DateTime startingHour = DateTime.MinValue;
            List<Step> steps = new();
            steps.Add(step);

            DateTime earlyDate = DateTime.MinValue;
            DateTime lateDate = DateTime.MaxValue;
            string numberProduct = "1";
            List<List<string>> consommableQuantity = new();

            oF = new(idOF, startingHour, 0, steps, earlyDate, lateDate, new(), numberProduct, consommableQuantity);

            List<OF> oFs = new();
            oFs.Add(oF);

            List<Machine> machine = new();
            machine.Add(machine1);
            Matrix_cleaning matrix_Cleaning = new();

            DataParsed data = new(oFs, consumables, machine, tanks, operators, matrix_Cleaning);

            List<SolutionPlanning> plannings = new();
            SolutionPlanning solutionPlanning = new();
            Available available = new();

            Job_shop_algorithm algorithm = new(data, available, plannings, solutionPlanning);
            DateTime algotime = new(2022, 01, 01, 7, 0, 0);
            int constraint = algorithm.StepAlgorithm(algotime);

            Console.WriteLine(constraint);

            Assert.AreEqual(0, constraint); 
        }
    }
}