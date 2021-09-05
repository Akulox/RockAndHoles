
namespace CellClasses
{
    public class PitCell : Cell
    {
        public override void CellAction()
        {
            if (HasDice())
            {
                VarManager.Dices[$"{row}_{col}"].RemoveDice();
            }
        }
    }
}
