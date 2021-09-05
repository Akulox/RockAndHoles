
namespace CellClasses
{
    public class TeleportCell : Cell
    {
        public int teleportID;
        public override void CellPlacement()
        {
            base.CellPlacement();
            try
            {
                VarManager.PortalCells.Add($"{teleportID}", new []{this, null});
            }
            catch
            {
                VarManager.PortalCells[$"{teleportID}"][1] = this;
            }
        }
    }
}
