namespace Coding4Fun.DiceShaker.Application.Controls
{
    public interface IDiceControl
    {
        void FillDice();
        void SetValues();
        void SetTotals();
        DiceCollection Dice { get; set; }
    }
}