using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelIntroduce : GameIntroduce
{
    public UnityEvent startDialogue;
    public UnityEvent endDialogue;
    public Animator joystick;
    private void Start()
    {
        if (!DataManager.Data.levels[SceneManager.GetActiveScene().buildIndex-1].dialogue_passed)
        {
            StartCoroutine(WaitFor(0.5f, delegate
            {
                startDialogue?.Invoke();
            }));
        }
        else EndLevelIntroduce();
    }

    public void EndLevelIntroduce()
    {
        DataManager.Data.levels[SceneManager.GetActiveScene().buildIndex-1].dialogue_passed = true;
        DataManager.SaveField();
        ToPrepare(true);
    }

    void ToPrepare(bool ready)
    {
        joystick.SetBool("isOpen", ready);
        GameObject.FindGameObjectWithTag("Wizard").transform.GetChild(0).GetComponent<Animator>().SetBool("isReady", ready);
        GameObject.FindGameObjectWithTag("Wizard").transform.GetChild(1).GetComponent<Animator>().SetBool("isReady", ready);
    }

    public void StartLevelEnding()
    {
        DataManager.Data.levels[SceneManager.GetActiveScene().buildIndex-1].level_completed = true;
        DataManager.SaveField();
        ToPrepare(false);
        StartCoroutine(WaitFor(0.5f, delegate
        {
            endDialogue.Invoke();
        }));
    }
}
