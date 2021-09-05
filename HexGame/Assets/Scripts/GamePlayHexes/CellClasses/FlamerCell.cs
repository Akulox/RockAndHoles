
namespace CellClasses
{
    
    public class FlamerCell : Cell
    {
        public bool fire = true;

        public override void CellAction()
        {
            if (fire && HasDice())
            {
                if (VarManager.Dices[$"{row}_{col}"] is IceDice)
                {
                    VarManager.Dices[$"{row}_{col}"].RemoveDice();
                    fire = false;
                    return;
                }
                if (VarManager.Dices[$"{row}_{col}"] is ExplosiveDice)
                {
                    VarManager.Dices[$"{row}_{col}"].RemoveDice();
                }
            }
        }
    }
}
