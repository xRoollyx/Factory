using UnityEngine;
using UnityEngine.UI;

namespace myProject{
    [ExecuteInEditMode]
    public class AutomaticVerticalSize : MonoBehaviour{
        [SerializeField]private float childHeight = 60f;
        private VerticalLayoutGroup _verticalLayoutGroup;
        private RectTransform _rectTransform;
        private void Start() {
            AdjustSize();
        }

        public void AdjustSize() {
            _rectTransform = GetComponent<RectTransform>();
            _verticalLayoutGroup = GetComponent<VerticalLayoutGroup>();
            Vector2 size = _rectTransform.sizeDelta;
            size.y = this.transform.childCount * (childHeight + _verticalLayoutGroup.spacing) + _verticalLayoutGroup.padding.bottom + _verticalLayoutGroup.padding.top;
            _rectTransform.sizeDelta = size;
        }
    }
}
