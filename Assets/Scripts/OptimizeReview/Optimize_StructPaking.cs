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
           
            Debug.Log(Marshal.SizeOf(new Size19_32()));
            Debug.Log(Marshal.SizeOf(new Size19_24()));
            
                
            
            
            
            parent.WriteLog("1. Struct Size", HeaderColor);
            WriteSize<Size9_12>(parent);
            WriteSize<Size19_24>(parent);
            WriteSize<Size6_8>(parent);
            WriteSize<Vector3_10_12>(parent);
            WriteSize<Vector3_12_12>(parent);
            WriteSize<Vector3>(parent);
            WriteSize<Vector4>(parent);
            WriteSize<Size25_32>(parent);
            
            InitializeTestTask(parent, 400000);
        }

        private async UniTask InitializeTestTask(OptimizeListLayer parent,int cycle)
        {
            parent.WriteLog("2. Initialize Test",HeaderColor);
            await InitializeTest<Size9_12>(parent,cycle);
            await InitializeTest<Size19_24>(parent,cycle);
            await InitializeTest<Size6_8>(parent,cycle);
            await InitializeTest<Vector3_10_12>(parent,cycle);
            await InitializeTest<Vector3_12_12>(parent,cycle);
            await InitializeTest<Vector3>(parent,cycle);
            await InitializeTest<Vector4>(parent,cycle);
            await InitializeTest<Size25_32>(parent,cycle);
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
    public struct CustomVector3
    {
        public long x;
        public long y;
        public byte z;
    }
    
    public struct Size19_32 //match 64bit
    {
        public long a;
        public bool b;
        public long c;
        public short d;
    }
    
    public struct Size19_24 //match 64bit
    {
        public long a;
        public long b;
        public short c;
        public bool d;
    }
    
    
    public struct Size25_32 //match 64bit
    {
        public long a; //8
        public long b; //8
        public long c; //8
        public byte d; //1
    }
    
    public struct Size9_12 //mismatch 64bit
    {
        public float x;
        public float y;
        public byte z;
    }
    
    public struct Size6_8 //match 64bit
    {
        public float x;
        public short y;
    }
    
    public struct Vector3_10_12
    {
        public float x;
        public float y;
        public byte smallZ;
        public byte smallW;
    }
    
    public struct Vector3_12_12
    {
        public float x;
        public float y;
        public short smallZ;
        public short smallW;
    }
    
    //8byte(64bit)
    
   
}