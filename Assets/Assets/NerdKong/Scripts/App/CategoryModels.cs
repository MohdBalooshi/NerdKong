using System;
using System.Collections.Generic;

namespace NerdKong.App
{
    [Serializable]
    public class CategoryInfo
    {
        public string id;
        public string displayName;
        public string packPath;
        public string iconAddress;
    }

    [Serializable]
    public class CategoryList
    {
        public List<CategoryInfo> categories = new List<CategoryInfo>();
    }
}
