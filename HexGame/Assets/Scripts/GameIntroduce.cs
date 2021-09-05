using System;
using System.Collections;
using UnityEngine;

public class GameIntroduce : MonoBehaviour
{
    public Animator lang;

    public DialogueTrigger firstDialogue;
    private void Start()
    {
        if (!DataManager.Data.first_dialogue_passed)
        {
            MenuCamera.active = false;
            StartCoroutine(WaitFor(1f, delegate
            {
                lang.SetBool("isOpen", true);
            }));
        }
    }
    public void LangChosen()
    {
        StartCoroutine(WaitFor(0.5f, delegate
        {
            firstDialogue.TriggerDialogue();
        }));
    }
    public IEnumerator WaitFor(float time, Action func)
    {
        yield return new WaitForSeconds(time);
        func.Invoke();
    }
    public void FirstDialoguePassed()
    {
        DataManager.Data.first_dialogue_passed = true;
        DataManager.SaveField();
    }
}
