using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Shell;

namespace Coding4Fun.DiceShaker.Application.Controls
{
    public partial class Risk : IDiceControl
    {
        private static IList<RiskConfiguration> _configurations;

        private int _attackRows;
        private int _defendRows;

        public Risk()
        {
            InitializeComponent();
			InitializeConfigurations();
        }

		private RiskConfiguration SelectedConfiguration
        {
			get { return (RiskConfiguration) cbRiskProfile.SelectedItem; }
            set
            {
                cbRiskProfile.SelectedItem = value;
                SetCurrentStateSelectedConfiguraiton();
            }
        }

        private static IEnumerable<RiskConfiguration> Configurations
        {
            get
            {
                return _configurations ?? (_configurations = new List<RiskConfiguration>
                                                                 {
                                                                     new RiskConfiguration
                                                                         {
                                                                             Name = "1 vs 1",
                                                                             NumberOfAttackers = 1,
                                                                             NumberOfDefenders = 1
                                                                         },
                                                                     new RiskConfiguration
                                                                         {
                                                                             Name = "2 vs 1",
                                                                             NumberOfAttackers = 2,
                                                                             NumberOfDefenders = 1
                                                                         },
                                                                     new RiskConfiguration
                                                                         {
                                                                             Name = "3 vs 1",
                                                                             NumberOfAttackers = 3,
                                                                             NumberOfDefenders = 1
                                                                         },
                                                                     new RiskConfiguration
                                                                         {
                                                                             Name = "1 vs 2",
                                                                             NumberOfAttackers = 1,
                                                                             NumberOfDefenders = 2
                                                                         },
                                                                     new RiskConfiguration
                                                                         {
                                                                             Name = "2 vs 2",
                                                                             NumberOfAttackers = 2,
                                                                             NumberOfDefenders = 2
                                                                         },
                                                                     new RiskConfiguration
                                                                         {
                                                                             Name = "3 vs 2",
                                                                             NumberOfAttackers = 3,
                                                                             NumberOfDefenders = 2
                                                                         }
                                                                 });
            }
        }

		private List<Dice> _attackers = new List<Dice>();
		private List<Dice> _defenders = new List<Dice>();

        #region IDiceControl Members

        public void FillDice()
        {
            Setup(SelectedConfiguration);
        }

        public void SetValues()
        {
            Reset();
            SplitDice();
            InitializeColumns();
        }

		public void SetTotals()
		{
			for (var i = 0; i < _defenders.Count; i++)
			{
			    if (_attackers.Count <= i)
					continue;

			    HighlightDice(_attackers[i].Side > _defenders[i].Side ? _attackers[i] : _defenders[i]);
			}
		}

    	public DiceCollection Dice { get; set; }

        #endregion

        private void SetCurrentStateSelectedConfiguraiton()
        {
            if (PhoneApplicationService.Current.State.ContainsKey("SelectedConfiguration"))
            {
                PhoneApplicationService.Current.State["SelectedConfiguration"] = SelectedConfiguration.Name;
            }
            else
            {
                PhoneApplicationService.Current.State.Add("SelectedConfiguration", SelectedConfiguration.Name);
            }
        }

        private static void HighlightDice(Dice dice)
        {
            dice.IsHighlighted = true;
        }

        private void AddDiceToGrid(Dice dice, PlayerSide playerSide)
        {
            switch (playerSide)
            {
                case PlayerSide.Attack:
                    Grid.SetColumn(dice, 0);
                    Grid.SetRow(dice, _attackRows++);
                    break;
                case PlayerSide.Deffend:
                    Grid.SetColumn(dice, 1);
                    Grid.SetRow(dice, _defendRows++);
                    break;
            }

            ContentPanel.Children.Add(dice);
        }

        private void InitializeColumns()
        {
            ContentPanel.ColumnDefinitions.Add(new ColumnDefinition {Width = new GridLength(150)});
            ContentPanel.ColumnDefinitions.Add(new ColumnDefinition {Width = new GridLength(150)});
        }

        private void InitializeGridRows(int rows)
        {
            ContentPanel.RowDefinitions.Clear();

            // Define the new Columns
            var rowDef1 = new RowDefinition {Height = new GridLength(150)};
            var rowDef2 = new RowDefinition {Height = new GridLength(150)};
            var rowDef3 = new RowDefinition {Height = new GridLength(150)};

            if (rows >= 1)
                ContentPanel.RowDefinitions.Add(rowDef1);
            if (rows >= 2)
                ContentPanel.RowDefinitions.Add(rowDef2);
            if (rows >= 3)
                ContentPanel.RowDefinitions.Add(rowDef3);
        }

        private void InitializeConfigurations()
        {
            _configurations = Configurations.ToList();
			cbRiskProfile.ItemsSource = _configurations;

            if (PhoneApplicationService.Current.State.ContainsKey("SelectedConfiguration"))
            {
                SelectedConfiguration = _configurations.FirstOrDefault(c => c.Name == PhoneApplicationService.Current.State["SelectedConfiguration"].ToString());
            }
            else
            {
                SelectedConfiguration = _configurations.FirstOrDefault();
            }
        }

        private void Reset()
        {
            ClearDice();
            InitializeRows();
        }

        private void ClearDice()
        {
            ContentPanel.Children.Clear();
            ContentPanel.RowDefinitions.Clear();
            ContentPanel.ColumnDefinitions.Clear();
        }

        private void SplitDice()
        {
            _attackers.Clear();
            _defenders.Clear();

            //Add Dice to Sides
            for (int i = 0; i < SelectedConfiguration.NumberOfAttackers; i++)
            {
                _attackers.Add(Dice[i]);
            }

            for (int i = 0; i < SelectedConfiguration.NumberOfDefenders; i++)
            {
                _defenders.Add(Dice[SelectedConfiguration.NumberOfAttackers + i]);
            }

            //Sort Highest to Lowest
            _attackers = _attackers.OrderByDescending(a => a.Side).ToList();
            _defenders = _defenders.OrderByDescending(d => d.Side).ToList();

            //Put dice on screen
            foreach (var attacker in _attackers)
            {
                AddDiceApperance(attacker, PlayerSide.Attack);
            }

            foreach (var deffender in _defenders)
            {
                AddDiceApperance(deffender, PlayerSide.Deffend);
            }
        }

        private void AddDiceApperance(Dice dice, PlayerSide playerSide)
        {
            dice.IsAccent = playerSide.Equals(PlayerSide.Attack);
            dice.Height = 125;
            dice.Width = 125;
            dice.HorizontalAlignment = HorizontalAlignment.Center;
            dice.VerticalAlignment = VerticalAlignment.Center;

            AddDiceToGrid(dice, playerSide);
        }

        private void InitializeRows()
        {
            _attackRows = 0;
            _defendRows = 0;

			InitializeGridRows(Math.Max(SelectedConfiguration.NumberOfAttackers, SelectedConfiguration.NumberOfDefenders));
        }
        
		private void cbRiskProfile_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			FillDice();
			SetValues();
			SetTotals();
		}

    	private void Setup(RiskConfiguration configuration)
        {
            Dice = new DiceCollection();

            Dice.AddNewDice(configuration.NumberOfAttackers + configuration.NumberOfDefenders, 6);
        }
    }
}

internal enum PlayerSide
{
    Attack,
    Deffend
}

public class RiskConfiguration
{
    public string Name { get; set; }
    public int NumberOfAttackers { get; set; }
    public int NumberOfDefenders { get; set; }
}