using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WBTween;

/// <summary>
/// �A�j���[�V�����𑖂点��N���X
/// </summary>
public class AnimationQueue:MonoSingleton<AnimationQueue>
{
    /// <summary>
    /// ���������Ă���ۂ�true�ɂ���
    /// </summary>
    private bool Blocked { get; set; } = false;

    Queue<Sequence> Queue { get; } = new Queue<Sequence>();


    private bool IsRunning { get; set; }
    public bool IsAnimating=>Queue.Count > 0 || IsRunning;

    /// <summary>
    /// �A�j���[�V������ǉ�����
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
    /// �Q�[���Ɍ������t�����ۂɌĂ�
    /// ����ȏ�A�j���[�V�����𑝂₳�Ȃ�
    /// </summary>
    public void BlockAddQueue()
    {
        Blocked = true;
    }

    /// <summary>
    /// �A�j���[�V�����L���[�̏����𑖂点��
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
