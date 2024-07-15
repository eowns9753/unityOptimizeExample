using System;
using System.Collections.Generic;
using System.Text;
using Cysharp.Threading.Tasks;
using OptimizeReview;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainScene
{
    public class OptimizeListLayer : MonoBehaviour
    {
        [SerializeField] private List<OptimizeReviewSet> _optimizeReviewSet;
        [SerializeField] private OptimizeListItem _listItemPrefab;
        [SerializeField] private Transform _listViewTransform;
        [SerializeField] private TextMeshProUGUI _btnElapseTime;
        [SerializeField] private Transform _simulrationTransform;
        [SerializeField] private TextMeshProUGUI _txt_Log;

        private StringBuilder _logContent;
        public Transform SimulationTransform => _simulrationTransform;
        
        private void Start()
        {
            _logContent = new();
            foreach (var set in _optimizeReviewSet)
            {
                var nObjs = Instantiate(_listItemPrefab.gameObject, _listViewTransform);
                nObjs.GetComponent<OptimizeListItem>().Refresh(this, set);
            }
            
            //목업은 실행시 숨김
            _listItemPrefab.gameObject.SetActive(false);
            ClearLog();
        }

        public void ClearLog()
        {
            _logContent.Clear();
            FlushStrBuilderToTxt().Forget();
        }

        public void WriteLog(string txt)
        {
            _logContent.Append(txt);
            _logContent.Append('\n');
            FlushStrBuilderToTxt().Forget();
        }

        public void WriteLog(string txt, Color color)
        {
            _logContent.Append($"<color=#{ ColorUtility.ToHtmlStringRGB(color)}>");
            WriteLog(txt);
            _logContent.Append("</color>");
            FlushStrBuilderToTxt().Forget();
        }

        private async UniTask FlushStrBuilderToTxt()
        {
            _txt_Log.Rebuild(CanvasUpdate.Layout);
            await UniTask.NextFrame();
            _txt_Log.text = _logContent.ToString();
        }

    }

    [Serializable]
    public struct OptimizeReviewSet
    {
        [HorizontalGroup] public string menuName;
        [HideLabel,HorizontalGroup,SerializeReference] public OptimizeReviewBase reviewScript;
    }
}