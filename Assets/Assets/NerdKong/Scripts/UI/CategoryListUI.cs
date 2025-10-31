using UnityEngine;
using TMPro;
using NerdKong.App;
using NerdKong.Utils;

namespace NerdKong.UI
{
    public class CategoryListUI : MonoBehaviour
    {
        public Transform listContainer;
        public CategoryItem categoryItemPrefab;
        public TMP_Text headerText;

        private void Start()
        {
            var catList = JsonLoader.LoadFromResources<CategoryList>("CategoryList");
            if(catList == null || catList.categories == null)
            {
                Debug.LogError("[CategoryListUI] No categories found.");
                return;
            }

            foreach(var c in catList.categories)
            {
                var item = Instantiate(categoryItemPrefab, listContainer);
                item.Bind(c.id, c.displayName, c.packPath, OnCategoryChosen);
            }
        }

        private void OnCategoryChosen(string categoryId, string packPath)
        {
            GameSession.SelectedCategoryId = categoryId;
            GameSession.SelectedPackPath = packPath;
            UnityEngine.SceneManagement.SceneManager.LoadScene("Match");
        }
    }
}
