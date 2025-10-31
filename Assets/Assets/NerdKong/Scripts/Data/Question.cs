using System;
using UnityEngine;

namespace NerdKong.Data
{
    [Serializable]
    public class Question
    {
        public string id;
        public string categoryId;
        [TextArea] public string text;
        public string[] options = new string[4];
        [Range(0,3)] public int correctIndex;
        [Range(1,5)] public int difficulty = 1;
    }
}
