using System.Runtime.InteropServices;
using UI.MainScene;
using UnityEngine;

namespace OptimizeReview
{
    public class Optimize_StructPaking : OptimizeReviewBase
    {
        public override void CallOptimizeCase(OptimizeListLayer parent)
        {
            Vec3 a = new();
            Vec3_Optimize b = new();
            parent.WriteLog("size check", HeaderColor);
            parent.WriteLog($"Vec3 Size - {Marshal.SizeOf(a)}");
            parent.WriteLog($"Vec3_Optimize Size - {Marshal.SizeOf(b)}");

            
            parent.WriteLog("initialize Test",HeaderColor);
            int cycle = 10000000;
            StartTimer();
            for (int i = 0; i < cycle; i++)
                a = new();
            parent.WriteLog($"Vec3 initialize {cycle} call ElapseTime -{EndTimer()}");
            
            StartTimer();
            for (int i = 0; i < cycle; i++)
                b = new();
            parent.WriteLog($"Vec3_Optimize initialize {cycle} call ElapseTime -{EndTimer()}");
        }
    }

    public struct Vec3
    {
        public float x;
        public float y;
        public float z;
    }
    
    public struct Vec3_Optimize
    {
        public float x;
        public float y;
        public short z_int;
    }
}