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
            if (!dataManager.data.firstDialoguePassed)
            {
                MenuCamera.active = false;
                StartCoroutine(WaitFor(1f, delegate { lang.SetBool("isOpen", true); }));
            }
        }

        public void LangChosen()
        {
            StartCoroutine(WaitFor(0.5f, delegate { firstDialogue.TriggerDialogue(); }));
        }

        public static IEnumerator WaitFor(float time, Action func)
        {
            yield return new WaitForSeconds(time);
            func.Invoke();
        }

        public void FirstDialoguePassed()
        {
            dataManager.data.firstDialoguePassed = true;
            dataManager.SaveField();
        }
    }
}