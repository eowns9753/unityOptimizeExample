using System;
using UI.MainScene;
using UnityEngine;

namespace OptimizeReview
{
    public delegate void OptimizeReviewResultHandler(string resultComment,bool isSuccess);
    
    [Serializable]
    public abstract class OptimizeReviewBase
    {
        [NonSerialized] public OptimizeReviewResultHandler Result = null;
        public OptimizeListLayer ParentLayer { get; set; }
        public virtual bool HasCall { get; set; }

        public void OpenScript()
        {
            Debug.Log("Call View Script");
        }
        
        public abstract void Initialize();
        public abstract void CallOptimizeCase();
    }
}