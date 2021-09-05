using System;
using System.Collections.Generic;
using System.Linq;
using CellClasses;
using DiceClasses;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VarManager : MonoBehaviour
{
    public static Dictionary<string, Dice> Dices = new Dictionary<string, Dice>();
    public static Dictionary<string, Dice> DicesUPD = new Dictionary<string, Dice>();
    public static Dictionary<string, Cell> Cells = new Dictionary<string, Cell>();
    public static Dictionary<string, TeleportCell[]> PortalCells = new Dictionary<string, TeleportCell[]>();
    
    public float step = 8f;
    static int _playButtonsCd = 0;
    static bool _playButtonsOn = true;
    static bool _lose = false;
    bool dicesHidden = false;

    public static void DictionaryClear()
    {
        Dices.Clear();
        DicesUPD.Clear();
        Cells.Clear();
        PortalCells.Clear();
    }
    public static void Lose()
    {
        _lose = true;
        Debug.Log("Вы проиграли");
    }

    static void Win()
    {
        FindObjectOfType<LevelIntroduce>().StartLevelEnding();
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
    public void HideShowDices()
    {
        if (_playButtonsCd == 0)
        {
            _playButtonsCd = 50;
            _playButtonsOn = dicesHidden;
            dicesHidden = !dicesHidden;
            HideShowDicesAnimation(dicesHidden);
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
            
            //Dice teleportation
            foreach (var sect in PortalCells.Values)
            {
                if (sect[0].HasUpdDice())
                {
                    DicesUPD[$"{sect[0].row}_{sect[0].col}"].row = sect[1].row;
                    DicesUPD[$"{sect[0].row}_{sect[0].col}"].col = sect[1].col;
                }
                if (sect[1].HasUpdDice())
                {
                    DicesUPD[$"{sect[1].row}_{sect[1].col}"].row = sect[0].row;
                    DicesUPD[$"{sect[1].row}_{sect[1].col}"].col = sect[0].col;
                }
            }
            
            //End of move, dictionaries reset
            Dices = DicesUPD.ToDictionary(entry => entry.Key, entry => entry.Value);
            DicesUPD.Clear();
            
            // Cell action after moving
            foreach (var cell in Cells.Values)
            {
                cell.CellAction();
            }
            
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

    private void FixedUpdate()
    {
        if (_playButtonsCd > 0) _playButtonsCd--;
    }
}
