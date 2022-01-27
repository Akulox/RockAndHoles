using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoadCheck : MonoBehaviour
{
    void Start()
    {
        GameObject.FindGameObjectWithTag("LevelStarter").GetComponent<LevelStarter>().SetDialogues();
    }
}
