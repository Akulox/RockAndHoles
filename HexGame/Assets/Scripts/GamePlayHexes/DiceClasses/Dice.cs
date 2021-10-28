using UnityEngine;

namespace GamePlayHexes.DiceClasses
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

        public string GetDiceName()
        {
            return $"{row}_{col}";
        }
        public void DicePlacement()
        {
            VarManager.Dices.Add(GetDiceName(), this);
        }
        public void DiceUpd()
        {
            VarManager.DicesUPD.Add(GetDiceName(), this);
        }
        public virtual void RemoveDice()
        {
            Destroy(VarManager.Dices[GetDiceName()].gameObject);
            VarManager.Dices.Remove(GetDiceName());
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

        public void MoveDiceRow(int r)
        {
            row += r;
        }
        public void MoveDiceCol(int c)
        {
            col += c;
        }
        public void MoveDice(int r, int c)
        {
            MoveDiceRow(r);
            MoveDiceCol(c);
        }
        public void SetDicePlacement(int r, int c)
        {
            row = r;
            col = c;
        }
        
        public void MakeMove(int dir)
        {
            if (movable && !hidden)
            {
                if (dir == 0)
                {
                    while (true)
                    {
                        DiceAnimationPlay("Leap5");
                        MoveDiceRow(1);
                        if (!CanGetCellValue(GetDiceName()))
                            while (CanGetCellValue($"{row - 1}_{col}"))
                                MoveDiceRow(-1);
                        if (CanGetDiceValue(GetDiceName()) && !VarManager.Dices[GetDiceName()].movable) continue;
                        return;
                    }
                }

                if (dir == 1)
                {
                    while (true)
                    {
                        DiceAnimationPlay("Leap4");
                        MoveDiceCol(-1);
                        if (!CanGetCellValue(GetDiceName()))
                            while (CanGetCellValue($"{row}_{col + 1}"))
                                MoveDiceCol(1);;
                        if (CanGetDiceValue($"{row}_{col}") && !VarManager.Dices[GetDiceName()].movable) continue;
                        return;
                    }
                }

                if (dir == 2)
                {
                    while (true)
                    {
                        DiceAnimationPlay("Leap3");
                        MoveDice(-1,-1);
                        if (!CanGetCellValue(GetDiceName()))
                            while (CanGetCellValue($"{row + 1}_{col + 1}"))
                            {
                                MoveDice(1,1);
                            }

                        if (CanGetDiceValue(GetDiceName()) && !VarManager.Dices[GetDiceName()].movable) continue;
                        return;
                    }
                }

                if (dir == 3)
                {
                    DiceAnimationPlay("Leap2");
                    while (true)
                    {
                        MoveDiceRow(-1);
                        if (!CanGetCellValue(GetDiceName()))
                            while (CanGetCellValue($"{row + 1}_{col}"))
                                MoveDiceRow(1);
                        if (CanGetDiceValue(GetDiceName()) && !VarManager.Dices[GetDiceName()].movable) continue;
                        return;
                    }
                }

                if (dir == 4)
                {
                    DiceAnimationPlay("Leap1");
                    while (true)
                    {
                        MoveDiceCol(1);
                        if (!CanGetCellValue(GetDiceName()))
                            while (CanGetCellValue($"{row}_{col - 1}"))
                                MoveDiceCol(-1);
                        if (CanGetDiceValue(GetDiceName()) && !VarManager.Dices[GetDiceName()].movable) continue;
                        return;
                    }
                }

                if (dir == 5)
                {
                    DiceAnimationPlay("Leap0");
                    while (true)
                    {
                        MoveDice(1,1);
                        if (!CanGetCellValue(GetDiceName()))
                            while (CanGetCellValue($"{row - 1}_{col - 1}"))
                            {
                                MoveDice(-1,-1);
                            }
                        if (CanGetDiceValue(GetDiceName()) && !VarManager.Dices[GetDiceName()].movable) continue;
                        return;
                    }
                }
            }
        }
        
        #region AnimationShit_ReplacementRequired

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
                    VarManager.Cells[GetDiceName()].transform.position.x, 
                    VarManager.Cells[GetDiceName()].transform.position.y, 
                    VarManager.Cells[GetDiceName()].transform.position.z);
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

        #endregion
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
