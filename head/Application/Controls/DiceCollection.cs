using System.Collections.Generic;
using System.Linq;

namespace Coding4Fun.DiceShaker.Application.Controls
{
    public class DiceCollection : List<Dice>
    {
        /// <summary>
        /// Add dice to the collection.
        /// </summary>
        public void AddNewDice()
        {
            AddNewDice(1, null);
        }

        /// <summary>
        /// Add dice to the collection.
        /// </summary>
        /// <param name="numToAdd">Number of dice to be added to the collection.</param>
        public void AddNewDice(int numToAdd)
        {
            AddNewDice(numToAdd, null);
        }

        /// <summary>
        /// Add dice to the collection.
        /// </summary>
        /// <param name="numToAdd">Number of dice to be added to the collection.</param>
        /// <param name="sides">The number of sides each of the dice will have.</param>
        public void AddNewDice(int numToAdd, int? sides)
        {
            for (int i = 0; i < numToAdd; i++)
            {
                Add(sides.HasValue ? new Dice { Sides = sides.Value } : new Dice());
            }
        }

        /// <summary>
        /// Rolls each dice in the collection.
        /// </summary>
        public void ThrowDice()
        {
            foreach (Dice dice in this)
            {
                dice.Roll();
            }
        }

        /// <summary>
        /// Returns the sum of all dice in the collection.
        /// </summary>
        /// <returns>SUM of all dice.</returns>
        public int GetTotalOfAllDice()
        {
            return this.Sum(dice => dice.Side);
        }
    }
}
