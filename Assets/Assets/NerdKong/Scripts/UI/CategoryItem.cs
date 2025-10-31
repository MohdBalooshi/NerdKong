using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace NerdKong.UI
{
    public class CategoryItem : MonoBehaviour
    {
        public Button button;
        public TMP_Text label;

        private string _categoryId;
        private string _packPath;
        private System.Action<string,string> _onClick;

        public void Bind(string id, string displayName, string packPath, System.Action<string,string> onClick)
        {
            _categoryId = id;
            _packPath = packPath;
            label.text = displayName;
            _onClick = onClick;

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => _onClick?.Invoke(_categoryId, _packPath));
        }
    }
}
