using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NerdKong.UI
{
    public class QuestionUI : MonoBehaviour
    {
        [Header("Texts")]
        public TMP_Text questionText;
        public TMP_Text counterText;
        public TMP_Text timerText;
        public TMP_Text scoreText;
        public TMP_Text revealText;

        [Header("Buttons")]
        public AnswerButton[] answerButtons;

        private int _correctIndex = -1;
        private Action<int> _onAnswer;

        public void BindQuestion(string qText, string[] options, Action<int> onAnswer)
        {
            questionText.text = qText;
            for(int i=0;i<answerButtons.Length && i<options.Length;i++)
            {
                answerButtons[i].Bind(i, options[i], HandleClick);
                answerButtons[i].SetStateNormal();
            }
            revealText.text = "";
            _onAnswer = onAnswer;
        }

        public void SetInternalAnswerKey(int correctIndex) => _correctIndex = correctIndex;

        public void SetQuestionCounter(int current, int total) => counterText.text = $"Q {current}/{total}";

        public void SetTimer(float remaining, float total)
        {
            timerText.text = Mathf.CeilToInt(remaining).ToString();
        }

        private void HandleClick(int index)
        {
            // lock all buttons
            foreach(var b in answerButtons) b.SetLocked(true);
            _onAnswer?.Invoke(index);
        }

        public bool Validate(int selectedIndex)
        {
            bool correct = selectedIndex == _correctIndex;
            for(int i=0;i<answerButtons.Length;i++)
            {
                if(i == _correctIndex) answerButtons[i].SetStateCorrect();
                else if(i == selectedIndex) answerButtons[i].SetStateWrong();
                else answerButtons[i].SetLocked(true);
            }
            return correct;
        }

        public void ShowReveal(bool correct, int gained, int totalScore)
        {
            revealText.text = correct ? $"Correct! +{gained}" : "Time's up or wrong";
            scoreText.text = $"Score: {totalScore}";
        }

        public void ShowFinalResults(int totalScore, int totalQuestions)
        {
            revealText.text = $"Final Score: {totalScore} / {totalQuestions * 1000}";
        }
    }
}
