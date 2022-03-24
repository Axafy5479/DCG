using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WBTween;

/// <summary>
/// アニメーションを走らせるクラス
/// </summary>
public class AnimationQueue:MonoSingleton<AnimationQueue>
{
    /// <summary>
    /// 決着がついている際にtrueにする
    /// </summary>
    private bool Blocked { get; set; } = false;

    Queue<Sequence> Queue { get; } = new Queue<Sequence>();


    private bool IsRunning { get; set; }
    public bool IsAnimating=>Queue.Count > 0 || IsRunning;

    /// <summary>
    /// アニメーションを追加する
    /// </summary>
    /// <param name="anim"></param>
    public void Add(Sequence anim)
    {
        if(!Blocked)Queue.Enqueue(anim);
    }

    private Coroutine Coroutine { get; set; }

    private void Start()
    {
        Coroutine = StartCoroutine(RunAnimation());
    }

    /// <summary>
    /// ゲームに決着が付いた際に呼ぶ
    /// これ以上アニメーションを増やさない
    /// </summary>
    public void BlockAddQueue()
    {
        Blocked = true;
    }

    /// <summary>
    /// アニメーションキューの処理を走らせる
    /// </summary>
    /// <returns></returns>
    private IEnumerator RunAnimation()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (Queue.Count > 0)
            {
                IsRunning = true;

                var s = Queue.Dequeue();
                yield return s.Play();
            }
            else
            {
                IsRunning = false;
            }
        }
    }



}
