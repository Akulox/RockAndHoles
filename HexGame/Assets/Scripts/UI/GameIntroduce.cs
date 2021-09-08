using System;
using System.Collections;
using UnityEngine;

namespace UI
{
    public class GameIntroduce : MonoBehaviour
    {
        public DataManager dataManager;

        public Animator lang;

        public DialogueTrigger firstDialogue;

        private void Start()
        {
            if (!dataManager.data.first_dialogue_passed)
            {
                MenuCamera.active = false;
                StartCoroutine(WaitFor(1f, delegate { lang.SetBool("isOpen", true); }));
            }
        }

        public void LangChosen()
        {
            StartCoroutine(WaitFor(0.5f, delegate { firstDialogue.TriggerDialogue(); }));
        }

        public IEnumerator WaitFor(float time, Action func)
        {
            yield return new WaitForSeconds(time);
            func.Invoke();
        }

        public void FirstDialoguePassed()
        {
            dataManager.data.first_dialogue_passed = true;
            dataManager.SaveField();
        }
    }
}