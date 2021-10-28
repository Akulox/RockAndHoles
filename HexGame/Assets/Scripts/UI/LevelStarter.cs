using System;
using GamePlayHexes;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UI.GameIntroduce;

public class LevelStarter : MonoBehaviour
{
    public Action startDialogue;
    public Action endDialogue;
    public Animator joystick;
    
    private DataManager dataManager;
    private SceneController sceneController;
    
    
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
    void Start()
    {
        Controls.SetCamera();
        dataManager = GameObject.FindGameObjectWithTag("GameData").GetComponent<DataManager>();
        joystick = GameObject.FindGameObjectWithTag("LevelUI").GetComponent<Animator>();
        
        sceneController = GameObject.FindGameObjectWithTag("SceneController").GetComponent<SceneController>();
        startDialogue = GameObject.FindGameObjectWithTag("StartDialogue").GetComponent<DialogueTrigger>().TriggerDialogue;
        endDialogue = GameObject.FindGameObjectWithTag("EndDialogue").GetComponent<DialogueTrigger>().TriggerDialogue;
        
        if (!dataManager.data.levels[SceneController.GetCurrentLevel()].dialoguePassed) 
        {
            StartCoroutine(WaitFor(0.5f, delegate { startDialogue?.Invoke(); }));
        }
        else EndLevelIntroduce();
    }

    private void OnDisable()
    {
        joystick.SetBool("isOpen", false);
    }
}
