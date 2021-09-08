using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class QuestionManager : MonoBehaviour
    {
        public Text questionText;
        public Animator questionAnim;
        private Question _question;

        public void StartQuestion(Question question)
        {
            questionText.text = GameObject.FindGameObjectWithTag("LocalizationManager")
                .GetComponent<LocalizationManager>().GetLocalizedValue(question.keySentence);
            _question = question;
            questionAnim.SetBool("IsOpen", true);
        }

        public void ConfirmAction()
        {
            _question.posAns.Invoke();
            EndQuestion();
        }

        public void EndQuestion()
        {
            questionAnim.SetBool("IsOpen", false);
        }
    }
}
