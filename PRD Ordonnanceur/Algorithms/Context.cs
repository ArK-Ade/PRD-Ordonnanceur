using PRD_Ordonnanceur.Data;
using System;
using System.Collections.Generic;

namespace PRD_Ordonnanceur.Algorithms
{
    /// <summary>
    /// This class allows to choose the strategy to use for scheduling.
    /// </summary>
    internal class Context
    {
        /// <summary>
        /// The Context maintains a reference to one of the Strategy objects. The
        /// Context does not know the concrete class of a strategy. It should
        /// work with all strategies via the Strategy interface.
        /// </summary>
        private IHeuristic _strategy;

        /// <summary>
        /// Default constructor
        /// </summary>
        public Context()
        { }

        /// <summary>
        /// Confortable Constructor
        /// </summary>
        /// <param name="strategy"></param>
        public Context(IHeuristic strategy)
        {
            this._strategy = strategy;
        }

        /// <summary>
        /// Setter
        /// </summary>
        /// <param name="strategy"></param>
        public void SetStrategy(IHeuristic strategy)
        {
            this._strategy = strategy;
        }

        /// <summary>
        /// The Context delegates some work to the Strategy object instead of
        /// implementing multiple versions of the algorithm on its own.
        /// </summary>
        /// <param name="choice"></param>
        /// <param name="oFs"></param>
        /// <returns></returns>
        public List<OF> Launch(int choice, List<OF> oFs)
        {
            Console.WriteLine("Context: Sorting data using the strategy");
            var result = this._strategy.SortingAlgorithm(choice, oFs);
            return result;
        }
    }
}