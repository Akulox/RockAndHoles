using System.Collections.Generic;
using System.Linq;
using GamePlayHexes.CellClasses;
using GamePlayHexes.DiceClasses;
using GamePlayHexes.Tile;
using UnityEngine;
using UI;

namespace GamePlayHexes
{
    public class VarManager : MonoBehaviour
    {
    
        public static float step = 8f;
        
        public static Dictionary<string, Dice> Dices = new Dictionary<string, Dice>();
        public static Dictionary<string, Dice> DicesUPD = new Dictionary<string, Dice>();
        public static Dictionary<string, Cell> Cells = new Dictionary<string, Cell>();
        public static Dictionary<string, CellObject> CellsObjects = new Dictionary<string, CellObject>();
    
    
        static int _playButtonsCd = 0;
        static bool _playButtonsOn = true;
        static bool _lose = false;
        static bool _dicesHidden = false;
        
    
        public static void DictionariesClear()
        {
            Dices.Clear();
            DicesUPD.Clear();
            Cells.Clear();
        }
        static void Win()
        {
            GameObject.FindGameObjectWithTag("LevelStarter").GetComponent<LevelStarter>().StartLevelEnding();
        }
        public static void Lose()
        {
            _lose = true;
            Debug.Log("Вы проиграли");
        }
    
        public void HideShowDices()
        {
            if (_playButtonsCd == 0)
            {
                _playButtonsCd = 50;
                _playButtonsOn = _dicesHidden;
                _dicesHidden = !_dicesHidden;
                HideShowDicesAnimation(_dicesHidden);
            }
        }
        public static void MakeMove(int dir)
        {
            if (_playButtonsCd == 0 && _playButtonsOn)
            {
                _playButtonsCd = 50;
                bool win = true;
                
                //Moving dices
                foreach (var dice in Dices.Values)
                {
                    dice.MakeMove(dir);
                    dice.DiceUpd();
                }
    
                ResetDicePositions();
                
                // Cell action after moving
                foreach (var cell in Cells.Values)
                {
                    cell.CellAction();
                }
    
                ResetDicePositions();
    
                // Dice action after cell action
                foreach (var dice in Dices.Values)
                {
                    dice.DiceAction();
                }
                
                // Cell conditions check
                foreach (var cell in Cells.Values)
                {
                    win &= cell.CheckCellProperties();
                }
                
                // Dice conditions check
                foreach (var dice in Dices.Values)
                {
                    win &= dice.CheckDiceProperties();
                }
                
                // Level passed check
                if (win && !_lose)
                {
                    Win();
                }
            }
        }
    
        private static void ResetDicePositions()
        {
            Dices = DicesUPD.ToDictionary(entry => entry.Key, entry => entry.Value);
            DicesUPD.Clear();
        }
        
        void HideShowDicesAnimation(bool state)
        {
            GameObject.FindGameObjectWithTag("Wizard").transform.GetChild(0).GetComponent<Animator>().SetBool("isHold", state);
            GameObject.FindGameObjectWithTag("Wizard").transform.GetChild(1).GetComponent<Animator>().SetBool("isHold", state);
            foreach (var dice in Dices.Values)
            {
                dice.HideShowDice(state);
            }
        }
    
        private void FixedUpdate()
        {
            if (_playButtonsCd > 0) _playButtonsCd--;
        }
    }

}
