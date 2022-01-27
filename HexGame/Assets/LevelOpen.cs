using System;
using System.Collections;
using System.Collections.Generic;
using GamePlayHexes;
using UI;
using UnityEngine;

public class LevelOpen : MonoBehaviour
{
    public void OpenLevelWindow(int level)
    {
        FindObjectOfType<LevelWindowOpen>().Open(level);
    }
}
