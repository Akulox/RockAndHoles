using System.Collections;
using System.Collections.Generic;
using GamePlayHexes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    public static int CurrentScene = (int) Scenes.LevelMenu;
    
    public static int levelCountStart = 3;

    private readonly List<AsyncOperation> _operationList = new List<AsyncOperation>();
    
    enum Scenes
    {
        ManagerScene = 0,
        LevelMenu,
        FieldName,
        Level1,
    }

    private void OpenPart(int[] partToLoad, int[] partToUnload)
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
        OpenPart(new []{(int) Scenes.FieldName, CurrentScene}, new []{(int)Scenes.LevelMenu});
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
                _operationList.Add(a);
            }
        }

        foreach (var p in partToLoad)
        {
            a = LoadPart(p);
            _operationList.Add(a);
        }

        while (!EachOperationIsDone())
        {
            yield return null;
        } 
        
        _operationList.Clear();
        
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
        foreach (var operation in _operationList)
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
