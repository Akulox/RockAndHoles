using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class DialogueTrigger : MonoBehaviour
    {
        public Dialogue dialogue;
        public UnityEvent unityEvent;

        public void TriggerDialogue()
        {
            GameObject.FindGameObjectWithTag((dialogue.isGameInstruction ? "Tip" : "Dialogue") + "Manager").GetComponent<DialogueManager>().StartDialogue(dialogue, unityEvent);
        }

        public void TriggerLevelStartEnding()
        {
            GameObject.FindGameObjectWithTag("LevelStarter").GetComponent<LevelStarter>().EndLevelIntroduce();
        }

        public void Home()
        {
            GameObject.FindGameObjectWithTag("SceneController").GetComponent<SceneController>().Home();
        }
    }
}
