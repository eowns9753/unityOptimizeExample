using System;
using System.Collections.Generic;
using OptimizeReview;
using UnityEngine;

namespace UI.MainScene
{
    public class OptimizeListLayer : MonoBehaviour
    {
        [SerializeField] private List<OptimizeReviewSet> _optimizeReviewSet;
        [SerializeField] private OptimizeListItem _listItemPrefab;
        [SerializeField] private Transform _listViewTransform;
        
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
        public string menuName;
        public OptimizeReviewBase reviewScript;
    }
}