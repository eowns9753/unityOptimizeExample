using System;

namespace OptimizeReview
{
    public delegate void OptimizeReviewResultHandler(string resultComment,bool isSuccess);
    
    [Serializable]
    public abstract class OptimizeReviewBase
    {
        [NonSerialized] public OptimizeReviewResultHandler Result = null;
        public virtual bool HasCall { get; set; } 
        
        public abstract void Initialize();
        public abstract void CallOptimizeCase();
    }
}