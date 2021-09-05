using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public UnityEvent unityEvent;

    public void TriggerDialogue()
    {
        gameObject.GetComponentInParent<DialogueManager>().StartDialogue(dialogue, unityEvent);
    }
}
