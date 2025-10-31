using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NerdKong.UI
{
    public class AnswerButton : MonoBehaviour
    {
        public Button button;
        public TMP_Text label;
        public Image background;

        private int _index;
        private Action<int> _onClick;

        public void Bind(int index, string text, Action<int> onClick)
        {
            _index = index;
            label.text = text;
            _onClick = onClick;
            SetLocked(false);
            SetStateNormal();

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => _onClick?.Invoke(_index));
        }

        public void SetLocked(bool locked)
        {
            button.interactable = !locked;
        }

        public void SetStateNormal()
        {
            if(background != null) background.color = Color.white;
            if(label != null) label.color = Color.black;
        }

        public void SetStateCorrect()
        {
            if(background != null) background.color = Color.green;
            if(label != null) label.color = Color.black;
            SetLocked(true);
        }

        public void SetStateWrong()
        {
            if(background != null) background.color = Color.red;
            if(label != null) label.color = Color.white;
            SetLocked(true);
        }
    }
}
