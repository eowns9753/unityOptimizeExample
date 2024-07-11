using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainScene
{
    public class OptimizeListItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Button _btnCall;
        [SerializeField] private Button _btnCodeView;
        
        public void Refresh(OptimizeListLayer parentLayer,OptimizeReviewSet reviewSet)
        {
            _text.text = reviewSet.menuName;
            var reviewScript = reviewSet.reviewScript;
            reviewScript.ParentLayer = parentLayer;
            _btnCodeView.onClick.AddListener(reviewScript.OpenScript);
            _btnCall.onClick.AddListener(reviewScript.CallOptimizeCase);
        }
    }
}