using GamePlayHexes.DiceClasses;
using UnityEngine;

namespace GamePlayHexes.CellClasses
{
    public class Cell : MonoBehaviour
    {
        [Header("Position")]
        [SerializeField] public int row;
        [SerializeField] public int col;

        public bool HasDice()
        {
            return VarManager.Dices.ContainsKey(GetCellName());
        }

        public Dice GetDice()
        {
            if (HasDice())
            {
                return VarManager.Dices[GetCellName()];
            }
            return null;
        }

        protected string GetCellName()
        {
            return $"{row}_{col}";
        }
        public bool HasUpdDice()
        {
            return VarManager.DicesUPD.ContainsKey(GetCellName());
        }
        public virtual void CellPlacement()
        {
            VarManager.Cells.Add(GetCellName(), this);
        }
        private void Awake()
        {
            CellPlacement();
        }

        public virtual void CellAction()
        {
            GetDice()?.DiceUpd();
        }
        public virtual bool CheckCellProperties()
        {
            return true;
        }
        
    }
}
