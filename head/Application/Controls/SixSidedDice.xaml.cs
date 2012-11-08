using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Coding4Fun.DiceShaker.Application.Helper;

namespace Coding4Fun.DiceShaker.Application.Controls
{
    public partial class SixSidedDice : IDiceControl
    {
        private static IList<SixSidedDiceConfiguration> _configurations;
        private int _colCount;
        private int _columns;
        private int _rowCount;

        public SixSidedDice()
        {
            InitializeComponent();
        }

        private int NumberofDice { get; set; }

        public static IList<SixSidedDiceConfiguration> Configurations
        {
            get
            {
                return _configurations ?? (_configurations = new List<SixSidedDiceConfiguration>
                                                                 {
                                                                     new SixSidedDiceConfiguration
                                                                         {Name = "One", Value = 1},
                                                                     new SixSidedDiceConfiguration
                                                                         {Name = "Two", Value = 2},
                                                                     new SixSidedDiceConfiguration
                                                                         {Name = "Three", Value = 3},
                                                                     new SixSidedDiceConfiguration
                                                                         {Name = "Four", Value = 4},
                                                                     new SixSidedDiceConfiguration
                                                                         {Name = "Five", Value = 5},
                                                                     new SixSidedDiceConfiguration
                                                                         {Name = "Six", Value = 6},
                                                                     new SixSidedDiceConfiguration
                                                                         {Name = "Seven", Value = 7},
                                                                     new SixSidedDiceConfiguration
                                                                         {Name = "Eight", Value = 8},
                                                                     new SixSidedDiceConfiguration
                                                                         {Name = "Nine", Value = 9},
                                                                 });
            }
        }

        #region IDiceControl Members

        public void FillDice()
        {
            Setup(SettingsHelper.ApplicationSettings.NumberOfDice);
            NumberofDice = Dice.Count;
        }

        public void SetValues()
        {
            Reset();
            AddDice(Dice);
        }

        public void SetTotals()
        {
        }

        public DiceCollection Dice { get; set; }

        #endregion

        public void InitializeNewGridRow()
        {
            // Define the new Row
            var rowDef1 = new RowDefinition {Height = new GridLength(RowHeight())};
            ContentPanel.RowDefinitions.Add(rowDef1);
        }

        public void InitializeGridColumns(int columns)
        {
            ContentPanel.ColumnDefinitions.Clear();

            // Define the new Columns
            var colDef1 = new ColumnDefinition();
            var colDef2 = new ColumnDefinition();
            var colDef3 = new ColumnDefinition();

            if (columns >= 1)
                ContentPanel.ColumnDefinitions.Add(colDef1);
            if (columns >= 2)
                ContentPanel.ColumnDefinitions.Add(colDef2);
            if (columns >= 3)
                ContentPanel.ColumnDefinitions.Add(colDef3);
        }

        public void InitializeNewDice(Dice dice, int rowCount, int colCount)
        {
            Grid.SetRow(dice, rowCount);
            Grid.SetColumn(dice, colCount);
            ContentPanel.Children.Add(dice);
        }

        private void CalculateColumns()
        {
            switch (NumberofDice)
            {
                case 1:
                case 2:
                case 3:
                    _columns = 1;
                    break;
                case 4:
                case 5:
                case 6:
                    _columns = 2;
                    break;
                case 7:
                case 8:
                case 9:
                    _columns = 3;
                    break;
                default:
                    _columns = 1;
                    break;
            }

            InitializeGridColumns(_columns);
        }

        public double RowHeight()
        {
            double rows = Math.Ceiling(Convert.ToDouble(NumberofDice)/_columns);

            switch (Convert.ToInt32(rows))
            {
                case 2:
                    return 225;
                case 3:
                    return 175;
                default:
                    return 300;
            }
        }

        private void Reset()
        {
            _colCount = 0;
            _rowCount = 0;
            ClearDice();
            CalculateColumns();
            CalculateRows();
        }

        public void ClearDice()
        {
            ContentPanel.Children.Clear();
            ContentPanel.RowDefinitions.Clear();
            ContentPanel.ColumnDefinitions.Clear();
        }

        private void AddDice(IEnumerable<Dice> diceCollection)
        {
            foreach (Dice dice in diceCollection)
            {
                AddDice(dice.Side);
            }
        }

        public void AddDice(int diceValue)
        {
            var dice = new Dice
                           {
                               Side = diceValue,
                               Height = 150,
                               Width = 150
                           };

            InitializeNewDice(dice, _rowCount, _colCount);
            CalculateColumn();
        }

        private void CalculateColumn()
        {
            if (_colCount.Equals(_columns - 1))
            {
                _colCount = 0;
                _rowCount++;
            }
            else
                _colCount++;
        }

        private void CalculateRows()
        {
            switch (NumberofDice)
            {
                case 1:
                    InitializeNewGridRow();
                    break;
                case 2:
                case 4:
                    InitializeNewGridRow();
                    InitializeNewGridRow();
                    break;
                case 3:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                    InitializeNewGridRow();
                    InitializeNewGridRow();
                    InitializeNewGridRow();
                    break;
            }
        }

        public void Setup(int dice)
        {
            Dice = new DiceCollection();

            Dice.AddNewDice(dice, 6);
        }

        public static int GetNumberOfDiceFromConfigurationName(string name)
        {
            return Configurations.Where(configuration => configuration.Name == name).FirstOrDefault().Value;
        }

        public static string GetConfigurationNameFromNumberOfDice(int number)
        {
            return Configurations[number - 1].Name;
        }
    }
}

public class SixSidedDiceConfiguration
{
    public string Name { get; set; }
    public int Value { get; set; }
}