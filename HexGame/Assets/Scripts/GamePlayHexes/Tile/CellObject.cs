using UnityEngine;

namespace GamePlayHexes.Tile
{
    [CreateAssetMenu(fileName = "HexGame", menuName = "New Cell")]
    public class CellObject : ScriptableObject
    {
        public int row;
        public int col;
        public GameObject gameObject;
    
        public bool HasDice()
        {
            return VarManager.Dices.ContainsKey($"{row}_{col}");
        }
        public bool HasUpdDice()
        {
            return VarManager.DicesUPD.ContainsKey($"{row}_{col}");
        }
        public virtual void CellPlacement()
        {
            VarManager.CellsObjects.Add($"{row}_{col}", this);
        }

        public virtual void CellAction  (){}
        public virtual bool CheckCellProperties()
        {
            return true;
        }
    }
}

