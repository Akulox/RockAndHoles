namespace CellClasses
{
    public class HoleCell : Cell
    {
        public override bool CheckCellProperties()
        {
            return HasDice();
        }
    }
}
