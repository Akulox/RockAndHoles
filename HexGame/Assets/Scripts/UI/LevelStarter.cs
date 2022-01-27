using System;
using GamePlayHexes;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UI.GameIntroduce;

public class LevelStarter : MonoBehaviour
{
    public Animator joystick;
    
    private DataManager dataManager;
    private Action startDialogue;
    private Action endDialogue;
    
    void ToPrepare(bool ready)
    {
        joystick.SetBool("isOpen", ready);
        GameObject.FindGameObjectWithTag("Wizard")?.transform.GetChild(0)
            .GetComponent<Animator>().SetBool("isReady", ready);
        GameObject.FindGameObjectWithTag("Wizard")?.transform.GetChild(1)
            .GetComponent<Animator>().SetBool("isReady", ready);
    }
    
    public void StartLevelEnding()
    {
        dataManager.data.levels[SceneController.GetCurrentLevel()].levelCompleted = true;
        dataManager.SaveField();
        ToPrepare(false);
        StartCoroutine(WaitFor(0.5f, delegate { endDialogue.Invoke(); }));
    }
    public void EndLevelIntroduce()
    {
        joystick.SetBool("isOpen", true);
        dataManager.data.levels[SceneController.GetCurrentLevel()].dialoguePassed = true;
        dataManager.SaveField();
        ToPrepare(true);
    }

    public void RequiredSettings()
    {
        Controls.SetCamera();
        dataManager = GameObject.FindGameObjectWithTag("GameData").GetComponent<DataManager>();
        joystick = GameObject.FindGameObjectWithTag("LevelUI").GetComponent<Animator>();
       
        GameObject.FindGameObjectWithTag("LevelUI").GetComponent<Controls>().ResetButtonsAngles();
        if (!dataManager.data.levels[SceneController.GetCurrentLevel()].dialoguePassed) 
        {
            StartCoroutine(WaitFor(0.5f, delegate { startDialogue?.Invoke(); }));
        }
        else EndLevelIntroduce();
    }

    public void SetDialogues()
    {
        startDialogue = GameObject.FindGameObjectWithTag("StartDialogue").GetComponent<DialogueTrigger>().TriggerDialogue;
        endDialogue = GameObject.FindGameObjectWithTag("EndDialogue").GetComponent<DialogueTrigger>().TriggerDialogue;
    }
    
    void Start()
    {
        RequiredSettings();
    }

    private void OnDisable()
    {
        joystick.SetBool("isOpen", false);
    }
}
