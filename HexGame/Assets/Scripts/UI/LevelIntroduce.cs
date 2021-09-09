using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace UI
{
    public class LevelIntroduce : GameIntroduce
    {
        public UnityEvent startDialogue;
        public UnityEvent endDialogue;
        public Animator joystick;

        private void Start()
        {
            if (!dataManager.data.levels[SceneManager.GetActiveScene().buildIndex - 1].dialoguePassed)
            {
                StartCoroutine(WaitFor(0.5f, delegate { startDialogue?.Invoke(); }));
            }
            else EndLevelIntroduce();
        }

        public void EndLevelIntroduce()
        {
            dataManager.data.levels[SceneManager.GetActiveScene().buildIndex - 1].dialoguePassed = true;
            dataManager.SaveField();
            ToPrepare(true);
        }

        void ToPrepare(bool ready)
        {
            joystick.SetBool("isOpen", ready);
            GameObject.FindGameObjectWithTag("Wizard").transform.GetChild(0).GetComponent<Animator>()
                .SetBool("isReady", ready);
            GameObject.FindGameObjectWithTag("Wizard").transform.GetChild(1).GetComponent<Animator>()
                .SetBool("isReady", ready);
        }

        public void StartLevelEnding()
        {
            dataManager.data.levels[SceneManager.GetActiveScene().buildIndex - 1].levelCompleted = true;
            dataManager.SaveField();
            ToPrepare(false);
            StartCoroutine(WaitFor(0.5f, delegate { endDialogue.Invoke(); }));
        }
    }
}
