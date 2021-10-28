using System;
using System.Collections;
using System.Collections.Generic;
using GamePlayHexes;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class SceneController : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    public static int CurrentScene = (int) Scenes.LevelMenu;
    
    public static int levelCountStart = 3;

    private List<AsyncOperation> OperationList = new List<AsyncOperation>();
    
    public enum Scenes
    {
        ManagerScene = 0,
        LevelMenu,
        FieldName,
        Level1,
    }

    public void OpenPart(int[] partToLoad, int[] partToUnload)
    {
        VarManager.DictionariesClear();
        StartCoroutine(LoadScene(partToLoad, partToUnload));
    }

    public void Home()
    {
        OpenPart(new []{(int) Scenes.LevelMenu}, new []{CurrentScene, (int) Scenes.FieldName});
        CurrentScene = (int) Scenes.LevelMenu;
    }

    public void Restart()
    {
        OpenPart(new []{CurrentScene}, new []{CurrentScene});
    }

    public void OpenLevel()
    {
        OpenPart(new []{CurrentScene, (int) Scenes.FieldName}, new []{(int)SceneController.Scenes.LevelMenu});
    }

    IEnumerator LoadScene(int[] partToLoad, int[] partToUnload)
    {
        transition.SetBool("isOpen", false);

        yield return new WaitForSeconds(transitionTime);

        AsyncOperation a;
        
        if (partToUnload != null)
        {
            foreach (var p in partToUnload)
            {
                a = UnloadPart(p);
                OperationList.Add(a);
            }
        }

        foreach (var p in partToLoad)
        {
            a = LoadPart(p);
            OperationList.Add(a);
        }

        while (!EachOperationIsDone())
        {
            yield return null;
        } 
        
        OperationList.Clear();
        
        yield return new WaitForSeconds(transitionTime);
        
        transition.SetBool("isOpen", true);
    }

    public static int GetCurrentLevel()
    {
        return CurrentScene - levelCountStart;
    }

    private bool EachOperationIsDone()
    {
        bool isDone = true;
        foreach (var operation in OperationList)
        {
            isDone &= operation.isDone;
        }
        return isDone;
    }

    private AsyncOperation LoadPart(int scene)
    {
        return SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
    }

    private AsyncOperation UnloadPart(int scene)
    {
        return SceneManager.UnloadSceneAsync(scene);
    }

    private void Start()
    {
        OpenPart(new [] {(int)Scenes.LevelMenu}, null);
    }
}
