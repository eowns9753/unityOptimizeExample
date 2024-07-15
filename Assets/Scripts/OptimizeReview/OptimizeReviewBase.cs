using System;
using System.Diagnostics;
using UI.MainScene;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace OptimizeReview
{
    public delegate void OptimizeReviewResultHandler(string resultComment,bool isSuccess);
    
    [Serializable]
    public abstract class OptimizeReviewBase
    {
        protected readonly Color HeaderColor = Color.yellow;
        private static Stopwatch _stopwatch = new();
        [NonSerialized] public OptimizeReviewResultHandler Result = null;
        public virtual bool HasCall { get; set; }

        public void OpenScript()
        {
            Debug.Log("Call View Script");
        }

        public virtual void Initialize(OptimizeListLayer parent)
        {
            parent.ClearLog();
            
        }
        
        public abstract void CallOptimizeCase(OptimizeListLayer parent);
        
        protected void StartTimer()
        {
            _stopwatch.Reset();
            _stopwatch.Start();
        }

        protected float EndTimer()
        {
            _stopwatch.Stop();
            return (float)_stopwatch.ElapsedMilliseconds / 1000;
        }
    }
}