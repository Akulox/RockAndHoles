namespace GamePlayHexes.CellClasses
{
    public class TeleportCell : Cell
    {
        public TeleportCell nextTeleport;

        public override void CellAction()
        {
            GetDice()?.SetDicePlacement(nextTeleport.row, nextTeleport.col);
            base.CellAction();
        }
    }
}
