using UnityEngine;

public class QuestionTrigger : MonoBehaviour
{
    public Question question;
    
    public void TriggerQuestion()
    {
        FindObjectOfType<QuestionManager>().StartQuestion(question);
    }
}
