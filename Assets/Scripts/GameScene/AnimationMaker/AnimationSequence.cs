using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnimationSequence
{
    public void Play()
    {

    }


}

namespace WBTween
{


    public abstract class TweenBase
    {
        public bool Completed { get; private set; } = false;
        public IEnumerator Play()
        {
            yield return _play();
            Completed = true;
        }
        protected abstract IEnumerator _play();
    }


    public class AppendCallback : TweenBase
    {
        private Action Action { get; }
        public AppendCallback(Action action)
        {
            Action = action;
        }
        protected override IEnumerator _play()
        {
            Action();
            yield return null;
        }
    }

    public class Wait : TweenBase
    {
        private float WaitTime { get; }
        public Wait(float waitTime)
        {
            WaitTime = waitTime;
        }

        protected override IEnumerator _play()
        {
            yield return new WaitForSeconds(WaitTime);
        }
    }

    public class Tween : TweenBase
    {
        private Transform Trn { get; }
        private Vector3 Target { get; }
        private Vector3 InitialPos { get; }

        private float AnimationTime { get; }
        private bool LocalPos { get; }

        public Tween(Transform trn, Func<Vector3> target, float time, bool localPos)
        {
            InitialPos = localPos ? trn.localPosition : trn.position;
            Trn = trn;
            Target = target();
            AnimationTime = time;
            LocalPos = localPos;
        }

        protected override IEnumerator _play()
        {
            float deltaT = AnimationTime / 30;

            for (int i = 0; i < 30; i++)
            {
                if (LocalPos)
                {
                    Trn.localPosition = InitialPos + (Target - InitialPos) * (i + 1) / 30;
                }
                else
                {
                    Trn.position = InitialPos + (Target - InitialPos) * (i + 1) / 30;
                }
                yield return new WaitForSeconds(deltaT);
            }

            if (LocalPos)
            {
                Trn.localPosition = Target;
            }
            else
            {
                Trn.position = Target;
            }
        }
    }


    public class Sequence
    {
        public bool Completed { get; private set; } = false;

        private Queue<List<TweenBase>> Tweens { get; }
        private MonoBehaviour Mono { get; }
        public Sequence(MonoBehaviour mono)
        {
            Mono = mono;
            Tweens = new Queue<List<TweenBase>>();
        }

        public Sequence Append(TweenBase t)
        {
            Tweens.Enqueue(new List<TweenBase>() { t });
            return this;
        }

        public Sequence AppendCallback(Action action)
        {
            this.Append(new AppendCallback(action));
            return this;
        }

        public Sequence Join(TweenBase t)
        {
            if (Tweens.Count > 0)
            {
                Tweens.Last().Add(t);
            }
            else
            {
                Append(t);
            }
            return this;
        }

        public IEnumerator Play()
        {
            while (Tweens.Count > 0)
            {
                var ts = Tweens.Dequeue();

                //Joinê¨ï™ÇÕï¿óÒèàóù
                for (int i = ts.Count - 1; i >= 1; i--)
                {
                    Mono.StartCoroutine(ts[i].Play());
                }

                //Appendê¨ï™Ç…ä÷ÇµÇƒÇÃÇ›ÅAèIóπÇë“Ç¬
                yield return ts[0].Play();
            }
            Completed = true;
        }
    }
}
