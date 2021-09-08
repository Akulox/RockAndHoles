
namespace CellClasses
{
    
    public class FlamerCell : Cell
    {
        public bool fire = true;

        public override void CellAction()
        {
            if (fire && HasDice())
            {
                if (VarManager.Dices[GetCellName()] is IceDice)
                {
                    VarManager.Dices[GetCellName()].RemoveDice();
                    fire = false;
                    return;
                }
                if (VarManager.Dices[GetCellName()] is ExplosiveDice)
                {
                    VarManager.Dices[GetCellName()].RemoveDice();
                }
            }
        }
    }
}
