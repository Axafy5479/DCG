using RX;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

[System.Serializable]
public class CardStatus_Chara : CardStatus,ICardMover,IChangeStatus
{
    public CardStatus_Chara(CardBook book, Pos pos, bool isPlayer, int gameId) : base(book, pos, isPlayer, gameId)
    {
        CardBook_Chara charaBook = (CardBook_Chara)book;
        atk = new Subject<int>( charaBook.Atk);
        maxHp = charaBook.Hp;
        hp = new Subject<int>( charaBook.Hp); //ゲーム開始時、体力は最大体力とする
        initialAttackNum = 1;//攻撃可能回数は1(後々複数攻撃可能なカードを作るときは変更)
        attackNumber = new Subject<int>( 0);//召喚したターンは攻撃不能
        statusEffects = new List<StatusEffect>();
    }

    #region カードが持つ性質
    [SerializeField] protected Subject<int> hp;
    [SerializeField] protected Subject<int> atk;
    [SerializeField] protected Subject<int> attackNumber;
    [SerializeField] protected int maxHp;
    [SerializeField] protected List<StatusEffect> statusEffects;

    public virtual IObservable<int> Hp { get => hp;}
    public virtual IObservable<int> Atk { get => atk; }
    public virtual IObservable<int> AttackNumber { get => attackNumber;  }
    public int MaxHp { get => maxHp; protected set => maxHp = value; }

    public override CardType Type => CardType.Chara;

    public ReadOnlyCollection<StatusEffect> StatusEffects => statusEffects.AsReadOnly();
    #endregion

    #region public methods
    /// <summary>
    /// ダメージを与え、HPの差分を返す
    /// </summary>
    /// <param name="damage">与えるダメージ</param>
    /// <returns>加えたダメージを返す</returns>
    public int Damage(int damage)
    {
        //Hp変更前の値を保持
        int prevHp = Hp.Value;

        //Hpを更新(0以下にはしない)
        hp.OnNext(Mathf.Max(0,Hp.Value - damage));

        if (Hp.Value <= 0)
        {
            attackNumber.OnNext(0);
            AddStatusEffect(StatusEffect.Dead);
        }

        //Hpの変化を返す
        return prevHp - Hp.Value;
    }

    /// <summary>
    /// 攻撃可能回数を戻す
    /// </summary>
    public void ResetAttackNum()
    {
        attackNumber.OnNext(initialAttackNum);
    }

    public void OnDead()
    {
        //捨て札に移動
        Debug.Log("死亡");
    }

    #endregion

    #region private

    /// <summary>
    /// 1ターンに何回攻撃できるか (通常は1回)
    /// </summary>
    private int initialAttackNum = 1;

    /// <summary>
    /// 状態異常の追加
    /// </summary>
    /// <param name="statusEffect"></param>
    public void AddStatusEffect(StatusEffect statusEffect)
    {
        statusEffects.Add(statusEffect);
    }

    /// <summary>
    /// 状態異常の除去
    /// </summary>
    /// <param name="statusEffect"></param>
    public void RemoveStatusEffect(StatusEffect statusEffect)
    {
        statusEffects.Remove(statusEffect);
    }

    public void AttackNumDecrement()
    {
        int n = AttackNumber.Value;
        if (n < 1)
        {
            throw new System.Exception("攻撃可能回数が0です");
        }
        attackNumber.OnNext(n - 1);

    }

    /// <summary>
    /// ATKを変更。ただし0未満にはならない
    /// </summary>
    /// <param name="delta"></param>
    public void ChangeAtk(int delta)
    {
        int temp = Mathf.Max(0, Atk.Value + delta);
        atk.OnNext(temp);
    }

    /// <summary>
    /// Hpを変更。ただし追加のみ
    /// </summary>
    /// <param name="delta"></param>
    public void AddHp(uint delta)
    {
        int temp = Mathf.Max(0, Hp.Value + (int)delta);
        hp.OnNext(temp);
    }
    #endregion
}

