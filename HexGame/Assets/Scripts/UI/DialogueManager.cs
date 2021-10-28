using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class DialogueManager : MonoBehaviour
    {
        public Text dialogueText;

        private Queue<string> sentences;

        public Animator dialogueWindowAnimation;

        private UnityEvent _unityEvent;

        private string currentSentence;



        private void Start()
        {
            sentences = new Queue<string>();
        }

        public void StartDialogue(Dialogue dialogue, UnityEvent unityEvent)
        {
            _unityEvent = unityEvent;

            MenuCamera.active = false;

            dialogueWindowAnimation.SetBool("IsOpen", true);

            sentences.Clear();

            LocalizationManager localizationManager = GameObject.FindGameObjectWithTag("LocalizationManager")
                .GetComponent<LocalizationManager>();

            foreach (string sentence in dialogue.keySentences)
            {
                sentences.Enqueue(localizationManager.GetLocalizedValue(sentence));
            }

            DisplayNextSentence();
        }

        public void DisplayNextSentence()
        {
            if (sentences.Count == 0)
            {
                EndDialogue();
                return;
            }

            currentSentence = sentences.Dequeue();

            StopAllCoroutines();
            StartCoroutine(TypeSentences(currentSentence));
        }

        public void Skip()
        {
            if (dialogueText.text == currentSentence)
            {
                DisplayNextSentence();
                return;
            }
            StopAllCoroutines();
            dialogueText.text = currentSentence;
        }

        private IEnumerator TypeSentences(string sentence)
        {
            dialogueText.text = "";
            foreach (char letter in sentence.ToCharArray())
            {
                dialogueText.text += letter;
                yield return null;
            }
        }

        void EndDialogue()
        {
            dialogueWindowAnimation.SetBool("IsOpen", false);
            MenuCamera.active = true;
            _unityEvent.Invoke();
        }
    }
}
