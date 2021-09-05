using UnityEngine;

namespace DiceClasses
{
    public class Dice : MonoBehaviour
    {
        string _animationActive;
        int _animTick;
        bool hidden = false;
             
        [Header("Position")]
        public int row;
        public int col;
        
        [Header("Properties")]
        public int colorID;
        public int rotation;
        public bool movable = true;
        public bool onFire;
        
        


        public void DicePlacement()
        {
            VarManager.Dices.Add($"{row}_{col}", this);
        }
        public void DiceUpd()
        {
            VarManager.DicesUPD.Add($"{row}_{col}", this);
        }
        public virtual void RemoveDice()
        {
            Destroy(VarManager.Dices[$"{row}_{col}"].gameObject);
            VarManager.Dices.Remove($"{row}_{col}");
        }
        
        public void DiceAnimationTick()
        {
            if (_animationActive != null) 
            {
                _animTick += 1;
            }
            if (_animTick == 50) 
            {
                _animTick = 0;
                _animationActive = null;
            }
        }
        public void DiceAnimationPlay(string aName)
        {
            _animTick = 0;
            _animationActive = aName; 
            transform.GetChild(0).GetComponent<Animator>().Play(aName);
        }
        public void DiceAnimationControl()
        {
            if (_animTick == 25 && (
                _animationActive == "Leap0" 
                || _animationActive == "Leap1"
                || _animationActive == "Leap2" 
                || _animationActive == "Leap3" 
                || _animationActive == "Leap4" 
                || _animationActive == "Leap5"
                ))
            {
                transform.position = new Vector3(
                    VarManager.Cells[$"{row}_{col}"].transform.position.x, 
                    VarManager.Cells[$"{row}_{col}"].transform.position.y, 
                    VarManager.Cells[$"{row}_{col}"].transform.position.z);
                transform.rotation = Quaternion.AngleAxis(rotation * 60, Vector3.up);
                return;
            }
            if (_animTick == 18 && _animationActive == "Up")
            {
                gameObject.transform.localScale = new Vector3(0,0,0);
                return;
            }
            if (_animTick == 12 && _animationActive == "Down")
            {
                gameObject.transform.localScale = new Vector3(1,1,1); 
                return;
            }
        }
        public bool CanGetCellValue(string key)
        {
            return VarManager.Cells.ContainsKey(key);
        }
        public bool CanGetDiceValue(string key)
        {
            return VarManager.Dices.ContainsKey(key);
        }
        
        //Move Dice
        public void MakeMove(int dir)
        {
            if (movable && !hidden)
            {
                if (dir == 0)
                {
                    while (true)
                    {
                        DiceAnimationPlay("Leap5");
                        row++;
                        if (!CanGetCellValue($"{row}_{col}"))
                            while (CanGetCellValue($"{row - 1}_{col}"))
                                row--;
                        if (CanGetDiceValue($"{row}_{col}") && !VarManager.Dices[$"{row}_{col}"].movable) continue;
                        return;
                    }
                }

                if (dir == 1)
                {
                    while (true)
                    {
                        DiceAnimationPlay("Leap4");
                        col--;
                        if (!CanGetCellValue($"{row}_{col}"))
                            while (CanGetCellValue($"{row}_{col + 1}"))
                                col++;
                        if (CanGetDiceValue($"{row}_{col}") && !VarManager.Dices[$"{row}_{col}"].movable) continue;
                        return;
                    }
                }

                if (dir == 2)
                {
                    while (true)
                    {
                        DiceAnimationPlay("Leap3");
                        row--;
                        col--;
                        if (!CanGetCellValue($"{row}_{col}"))
                            while (CanGetCellValue($"{row + 1}_{col + 1}"))
                            {
                                row++;
                                col++;
                            }

                        if (CanGetDiceValue($"{row}_{col}") && !VarManager.Dices[$"{row}_{col}"].movable) continue;
                        return;
                    }
                }

                if (dir == 3)
                {
                    DiceAnimationPlay("Leap2");
                    while (true)
                    {
                        row--;
                        if (!CanGetCellValue($"{row}_{col}"))
                            while (CanGetCellValue($"{row + 1}_{col}"))
                                row++;
                        if (CanGetDiceValue($"{row}_{col}") && !VarManager.Dices[$"{row}_{col}"].movable) continue;
                        return;
                    }
                }

                if (dir == 4)
                {
                    DiceAnimationPlay("Leap1");
                    while (true)
                    {
                        col++;
                        if (!CanGetCellValue($"{row}_{col}"))
                            while (CanGetCellValue($"{row}_{col - 1}"))
                                col--;
                        if (CanGetDiceValue($"{row}_{col}") && !VarManager.Dices[$"{row}_{col}"].movable) continue;
                        return;
                    }
                }

                if (dir == 5)
                {
                    DiceAnimationPlay("Leap0");
                    while (true)
                    {
                        row++;
                        col++;
                        if (!CanGetCellValue($"{row}_{col}"))
                            while (CanGetCellValue($"{row - 1}_{col - 1}"))
                            {
                                row--;
                                col--;
                            }

                        if (CanGetDiceValue($"{row}_{col}") && !VarManager.Dices[$"{row}_{col}"].movable) continue;
                        return;
                    }
                }
            }
        }

        public void HideShowDice(bool state)
        {
            if (movable) DiceAnimationPlay(state ? "Up" : "Down");
        }

        public virtual void DiceAction() { }
        public virtual bool CheckDiceProperties()
        {
            return true;
        }
        private void Awake()
        {
            DicePlacement();
        }
        private void FixedUpdate()
        {
            DiceAnimationTick();
            DiceAnimationControl();
        }
    }
}
