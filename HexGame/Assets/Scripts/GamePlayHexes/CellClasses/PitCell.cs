
namespace CellClasses
{
    public class PitCell : Cell
    {
        public override void CellAction()
        {
            if (HasDice())
            {
                VarManager.Dices[GetCellName()].RemoveDice();
            }
        }
    }
}
