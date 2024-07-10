using System;

namespace OptimizeReview
{
    public delegate void OptimizeReviewResultHandler(string resultComment,bool isSuccess);
    public abstract class OptimizeReviewBase
    {
        [NonSerialized] public OptimizeReviewResultHandler Result = null;
        public virtual bool HasCall { get; set; } 
        
        public abstract void Initialize();
        public abstract void CallNonOptimizeCase();
        public abstract void CallOptimizeCase();
    }
}