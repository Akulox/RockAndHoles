using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoaderScript : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    
    public void Restart()
    {
        OpenLevel(SceneManager.GetActiveScene().name);
    }
    public void OpenLevel(string level)
    {
        VarManager.DictionaryClear();
        StartCoroutine(LoadLevel(level));
    }

    IEnumerator LoadLevel(string level)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(level);
    }

    public void QuitLevel()
    {
        OpenLevel("LevelMap");
    }
}
