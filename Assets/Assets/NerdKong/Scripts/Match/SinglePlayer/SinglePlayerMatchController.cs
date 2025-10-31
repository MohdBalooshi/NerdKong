using System;
using System.Collections.Generic;
using UnityEngine;
using NerdKong.Data;
using NerdKong.Utils;
using NerdKong.UI;

namespace NerdKong.Match
{
    public enum MatchState { Waiting, Countdown, Question, Reveal, Results }

    public class SinglePlayerMatchController : MonoBehaviour
    {
        [Header("Config")]
        [Tooltip("Path under Resources without extension, e.g. QuestionPacks/general_knowledge_pack")]
        public string questionPackPath = "QuestionPacks/general_knowledge_pack";
        public int questionsPerMatch = 5;
        public float questionTimeSeconds = 10f;
        public int pointsMaxPerQuestion = 1000;
        public int pointsMinOnCorrect = 100;

        [Header("Refs")]
        public QuestionUI questionUI;

        // runtime
        private QuestionPack _pack;
        private List<Question> _roundQuestions = new List<Question>();
        private int _currentIndex = -1;
        private float _timer = 0f;
        private bool _answered = false;
        private int _totalScore = 0;
        private MatchState _state = MatchState.Waiting;

        private void Start()
        {
            LoadPack();
            PrepareRound();
            NextQuestion();
        }

        private void Update()
        {
            if(_state != MatchState.Question) return;
            _timer -= Time.deltaTime;
            questionUI.SetTimer(Mathf.Max(0, _timer), questionTimeSeconds);
            if(_timer <= 0f && !_answered)
            {
                // time out = incorrect
                OnAnswerSelected(-1);
            }
        }

        private void LoadPack()
        {
            _pack = JsonLoader.LoadFromResources<QuestionPack>(questionPackPath);
            if(_pack == null || _pack.questions == null || _pack.questions.Count == 0)
            {
                Debug.LogError("[SinglePlayerMatchController] Question pack not found or empty.");
            }
        }

        private void PrepareRound()
        {
            _roundQuestions.Clear();
            var src = new List<Question>(_pack.questions);
            // Shuffle source
            for(int i=0;i<src.Count;i++)
            {
                int j = UnityEngine.Random.Range(i, src.Count);
                (src[i], src[j]) = (src[j], src[i]);
            }
            // Take needed
            for(int k=0; k<questionsPerMatch && k<src.Count; k++)
                _roundQuestions.Add(src[k]);
        }

        private void NextQuestion()
        {
            _currentIndex++;
            if(_currentIndex >= _roundQuestions.Count)
            {
                ShowResults();
                return;
            }

            _answered = false;
            _timer = questionTimeSeconds;
            _state = MatchState.Question;

            var q = _roundQuestions[_currentIndex];

            // Create shuffled options mapping
            int[] indices = new int[] {0,1,2,3};
            for(int i=0;i<indices.Length;i++)
            {
                int j = UnityEngine.Random.Range(i, indices.Length);
                (indices[i], indices[j]) = (indices[j], indices[i]);
            }

            int newCorrectIndex = Array.IndexOf(indices, q.correctIndex);

            questionUI.BindQuestion(q.text, new string[] {
                q.options[indices[0]],
                q.options[indices[1]],
                q.options[indices[2]],
                q.options[indices[3]]
            }, OnAnswerSelected);

            questionUI.SetQuestionCounter(_currentIndex + 1, _roundQuestions.Count);
            questionUI.SetTimer(_timer, questionTimeSeconds);

            // store mapping in UI for validation
            questionUI.SetInternalAnswerKey(newCorrectIndex);
        }

        private void OnAnswerSelected(int selectedIndex)
        {
            if(_answered) return;
            _answered = true;
            _state = MatchState.Reveal;

            bool correct = questionUI.Validate(selectedIndex);
            int gained = 0;
            if(correct)
            {
                float t = Mathf.Clamp01((_timer) / questionTimeSeconds);
                // faster => closer to max
                gained = Mathf.RoundToInt(pointsMinOnCorrect + t * (pointsMaxPerQuestion - pointsMinOnCorrect));
            }
            _totalScore += gained;

            questionUI.ShowReveal(correct, gained, _totalScore);

            // small delay before next
            Invoke(nameof(NextQuestion), 1.2f);
        }

        private void ShowResults()
        {
            _state = MatchState.Results;
            questionUI.ShowFinalResults(_totalScore, _roundQuestions.Count);
        }
    }
}
