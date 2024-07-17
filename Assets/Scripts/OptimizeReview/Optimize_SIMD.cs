using System.Collections.Generic;
using UI.MainScene;
using Unity.Burst;
using Unity.Burst.CompilerServices;
using Unity.Burst.Intrinsics;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace OptimizeReview
{
    public class Optimize_SIMD : OptimizeReviewBase
    {
        private const int ArraySize = 5000000;//500만
        private int _addValue = 12;
        
        public override void CallOptimizeCase(OptimizeListLayer parent)
        {
            for (int ix = 0; ix < 3; ix++)
            {
                parent.WriteLog($"BurstTest {ix+1}, arraySize {ArraySize}",Color.green);
                var nonBurstArray = new Vector4[ArraySize];
                var burstFloat4Array = new NativeArray<Vector4>(ArraySize, Allocator.TempJob);
            
                for (int i = 0; i < nonBurstArray.Length; i++)
                    nonBurstArray[i] = new Vector4(getRand(),getRand(),getRand(),getRand());
                for (int i = 0; i < burstFloat4Array.Length; i++)
                    burstFloat4Array[i] = new Vector4(getRand(),getRand(),getRand(),getRand());
            
                var float4Job = new Float4Job(burstFloat4Array, _addValue);
                StartTimer();
                var handle = float4Job.Schedule();
                handle.Complete();
                parent.WriteLog($"UseBurst, float4 + int {EndTimer()} s");
                burstFloat4Array.Dispose();
            
            
                StartTimer();
                Float4Job.NonBurstExecute(nonBurstArray,_addValue);
                parent.WriteLog($"UseMono, float4 + int {EndTimer()} s");
            }
            
        }

        void Call()
        {
            //네이티브 컨테이너 생성
            NativeArray<int> tempArray = new NativeArray<int>(1000, Allocator.TempJob, NativeArrayOptions.ClearMemory);
            
            TestJob job = new TestJob(tempArray);
            var handle = job.Schedule();
            //잡연산이 끝날때까지 대기
            handle.Complete();
            
            //네이티브 컨테이너 소멸
            tempArray.Dispose();
        }
        

        int getRand() => UnityEngine.Random.Range(0, 1000);
    }
    
    [BurstCompile]
    public struct Float4Job : IJob
    {
        private NativeArray<Vector4> _burstArray;
        private int _addValueInt;
        
        public Float4Job(NativeArray<Vector4> arr,int addValue)
        {
            _burstArray = arr;
            _addValueInt = addValue;
        }

        public void Execute()
        {
            for (int i = 0; i < _burstArray.Length; i++)
            {
                float4 r = (float4)_burstArray[i] + _addValueInt;
                _burstArray[i]=  (Vector4)r ;
            }
        }
        
        public static void NonBurstExecute(Vector4[] arr,int addValue)
        {
            var add = new Vector4(addValue, addValue, addValue, addValue);
            for (int i = 0; i < arr.Length; i++)
            {
                var r = arr[i]+add;
                arr[i] = r;
            }
            
        }
    }
    
    [BurstCompile]
    public struct TestJob : IJob
    {
        private NativeArray<int> _list;
        public TestJob(NativeArray<int> list)
        {
            _list = list;
        }
    
        public void Execute()
        {
            for (int i = 0; i < _list.Length; i++)
            {
                _list[i] += 1;
            }
        }
    }
    
    
}