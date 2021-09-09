using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sus : MonoBehaviour
{
    void Start()
    {
        Debug.Log(GameObject.FindGameObjectWithTag("GameData").GetComponent<DataManager>().data.language);
    } 
}
