namespace Coding4Fun.DiceShaker.Application.Controls
{
    public partial class Monopoly : IDiceControl
    {
        public Monopoly()
        {
            InitializeComponent();
        }

        #region IDiceControl Members

        public void FillDice()
        {
            if (Dice == null)
                Setup();
        }

        public void SetValues()
        {
            SetDiceSides();
        }

        public void SetTotals()
        {
        }

        public DiceCollection Dice { get; set; }

        #endregion

        private void SetDiceSides()
        {
            if (ContentPanel.Children.Count > 0)
            {
                ((Dice)ContentPanel.Children[0]).Side = dice1.Side;
                ((Dice)ContentPanel.Children[1]).Side = dice2.Side;
            }
        }

        private void Setup()
        {
            Dice = new DiceCollection();
            Dice.AddNewDice(2, 6);

            if (Dice[0] != null)
                dice1 = Dice[0];

            if (Dice[1] != null)
                dice2 = Dice[1];
        }
    }
}