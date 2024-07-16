using System;
using System.Runtime.InteropServices;
using Cysharp.Threading.Tasks;
using UI.MainScene;
using UnityEngine;

namespace OptimizeReview
{
    public class Optimize_StructPaking : OptimizeReviewBase
    {
        public override void CallOptimizeCase(OptimizeListLayer parent)
        {
            parent.WriteLog("1. Struct Size", HeaderColor);
            WriteSize<TestStruct32>(parent);
            WriteSize<TestStruct24>(parent);
            WriteSize<bool>(parent);
            WriteSize<byte>(parent);
            InitializeTestTask(parent, 400000);
        }

        private async UniTask InitializeTestTask(OptimizeListLayer parent,int cycle)
        {
            parent.WriteLog("2. Initialize Test",HeaderColor);
            await InitializeTest<TestStruct32>(parent,cycle);
            await InitializeTest<TestStruct24>(parent,cycle);
            await InitializeTest<bool>(parent,cycle);
        }
        
        void WriteSize<T>(OptimizeListLayer parent)
        {
            T temp = default;
            parent.WriteLog($"{typeof(T).Name} Size - {Marshal.SizeOf(temp)}");
        }

        UniTask InitializeTest<T>(OptimizeListLayer parent,int cycle)
        {
            StartTimer();
            T temp = default;
            for (int i = 0; i < cycle; i++)
                moveStack(temp,0,100);
            parent.WriteLog($"{typeof(T).Name} initialize {cycle} call ElapseTime - {EndTimer()}");
            return UniTask.NextFrame();
        }

        int moveStack<T>(T value,int moveidx, int count)
        {
            if (moveidx > count)
                return moveidx;
            moveidx = moveStack<T>(value,++moveidx,count);
            return moveidx;
        }
    }

    public struct TestStruct32
    {
        public long a;//8
        public bool b;//8
        public long c;//8
        public short d;//8
        public float e;//
    }
    
    public struct TestStruct24
    {
        public long a;//8
        public long c;//8
        public float e;//8
        public byte b;//
        public short aa;
      
    }
   
}