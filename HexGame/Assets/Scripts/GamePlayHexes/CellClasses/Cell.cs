using System.Collections.Generic;
using UnityEngine;

namespace CellClasses
{
    public class Cell : MonoBehaviour
    {
        [Header("Position")]
        [SerializeField] public int row;
        [SerializeField] public int col;

        public bool HasDice(/*Dictionary<string,string> dictionary*/)
        {
            return VarManager.Dices.ContainsKey($"{row}_{col}");
        }
        public bool HasUpdDice()
        {
            return VarManager.DicesUPD.ContainsKey($"{row}_{col}");
        }
        public virtual void CellPlacement()
        {
            VarManager.Cells.Add($"{row}_{col}", this);
        }
        private void Awake()
        {
            CellPlacement();
        }

        public virtual void CellAction(){}
        public virtual bool CheckCellProperties()
        {
            return true;
        }
        
    }
}
