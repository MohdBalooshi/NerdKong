using UnityEngine;
using NerdKong.App;
using NerdKong.Match;

namespace NerdKong.Bootstrap
{
    public class CategoryAwareMatchBootstrap : MonoBehaviour
    {
        public SinglePlayerMatchController controller;

        private void Awake()
        {
            if(controller == null)
            {
                controller = FindObjectOfType<SinglePlayerMatchController>();
            }
            if(controller != null)
            {
                controller.questionPackPath = GameSession.SelectedPackPath;
                controller.questionsPerMatch = GameSession.QuestionsPerMatch;
            }
        }
    }
}
