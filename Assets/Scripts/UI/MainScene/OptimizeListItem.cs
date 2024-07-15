using OptimizeReview;
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
        private OptimizeReviewBase _reviewScript;
        private OptimizeListLayer _parent;
        
        public void Refresh(OptimizeListLayer parentLayer,OptimizeReviewSet reviewSet)
        {
            _text.text = reviewSet.menuName;
            _parent = parentLayer;
            _reviewScript = reviewSet.reviewScript;
            _btnCodeView.onClick.AddListener(_reviewScript.OpenScript);
            _btnCall.onClick.AddListener(Call);
        }

        void Call()
        {
            Debug.Log($"Call {_reviewScript.GetType().FullName}");
            _reviewScript.Initialize(_parent);
            _reviewScript.CallOptimizeCase(_parent);
        }
    }
}