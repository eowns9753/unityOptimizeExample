using System;
using System.Collections.Generic;
using OptimizeReview;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace UI.MainScene
{
    public class OptimizeListLayer : MonoBehaviour
    {
        [SerializeField] private List<OptimizeReviewSet> _optimizeReviewSet;
        [SerializeField] private OptimizeListItem _listItemPrefab;
        [SerializeField] private Transform _listViewTransform;
        [SerializeField] private TextMeshProUGUI _btnElapseTime;
        
        private void Start()
        {
            foreach (var set in _optimizeReviewSet)
            {
                var nObjs = Instantiate(_listItemPrefab.gameObject, _listViewTransform);
                nObjs.GetComponent<OptimizeListItem>().Refresh(set);
            }
            
            //목업은 실행시 숨김
            _listItemPrefab.gameObject.SetActive(false);
        }
    }

    [Serializable]
    public struct OptimizeReviewSet
    {
        [HorizontalGroup]public string menuName;
        [HideLabel,HorizontalGroup,SerializeReference] public OptimizeReviewBase reviewScript;
    }
}