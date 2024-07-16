using System.Collections.Generic;
using UI.MainScene;
using Unity.Burst;
using Unity.Burst.Intrinsics;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using TestType = UnityEngine.Vector4;

namespace OptimizeReview
{
    public class Optimize_SIMD : OptimizeReviewBase
    {
        private const int ArraySize = 5000000;
        private TestType _addValue = new(1.2f,1.1f,0,0);
        
        public override void CallOptimizeCase(OptimizeListLayer parent)
        {
            var nonBurstArray = new TestType[ArraySize];
            var burstArray = new NativeArray<TestType>(ArraySize, Allocator.TempJob);
            for (int i = 0; i < nonBurstArray.Length; i++)
                nonBurstArray[i] = GetRandomValue();
            for (int i = 0; i < burstArray.Length; i++)
                burstArray[i] = GetRandomValue();
            
            SIMDTestJob job = new SIMDTestJob(burstArray, _addValue);
            StartTimer();
            var handle = job.Schedule();
            handle.Complete();
            parent.WriteLog($"burst add {EndTimer()}");
            burstArray.Dispose();
            
            StartTimer();
            for (int i = 0; i < nonBurstArray.Length; i++)
                nonBurstArray[i] += _addValue;
            parent.WriteLog($"non burst add {EndTimer()}");
        }
        
        TestType GetRandomValue()
        {
            TestType result = new TestType();
            result.x = UnityEngine.Random.Range(0, 1000);
            result.y = UnityEngine.Random.Range(0, 1000);
            result.z = UnityEngine.Random.Range(0, 1000);
            return result;
        }
    }
    
    [BurstCompile]
    public  struct SIMDTestJob : IJob
    {
        private NativeArray<TestType> _burstArray;
        private Vector4 _addValue;
        
        public SIMDTestJob(NativeArray<TestType> arr,TestType addValue)
        {
            _burstArray = arr;
            _addValue = addValue;
        }

        public void Execute()
        {
            for (int i = 0; i < _burstArray.Length; i++)
            {
                //mathmatics
                /*float4 v = _burstArray[i];
                float4 r = v + _addValue;
                _burstArray[i]=  r ;*/
                
                //vector
                _burstArray[i] += _addValue;
            }
        }
    }
}