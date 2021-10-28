namespace GamePlayHexes.CellClasses
{
    public class ColorCell : Cell
    {
        public int colorID;
        public override bool CheckCellProperties()
        {
            return HasDice() && VarManager.Dices[GetCellName()].colorID == colorID;
        }
    }
}

