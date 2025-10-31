using System;
using System.Collections.Generic;

namespace NerdKong.Data
{
    [Serializable]
    public class QuestionPack
    {
        public string categoryId;
        public List<Question> questions = new List<Question>();
    }
}
