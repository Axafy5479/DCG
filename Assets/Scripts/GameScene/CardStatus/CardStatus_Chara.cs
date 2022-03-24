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
        hp = new Subject<int>( charaBook.Hp); //�Q�[���J�n���A�̗͍͂ő�̗͂Ƃ���
        initialAttackNum = 1;//�U���\�񐔂�1(��X�����U���\�ȃJ�[�h�����Ƃ��͕ύX)
        attackNumber = new Subject<int>( 0);//���������^�[���͍U���s�\
        statusEffects = new List<StatusEffect>();
    }

    #region �J�[�h��������
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
    /// �_���[�W��^���AHP�̍�����Ԃ�
    /// </summary>
    /// <param name="damage">�^����_���[�W</param>
    /// <returns>�������_���[�W��Ԃ�</returns>
    public int Damage(int damage)
    {
        //Hp�ύX�O�̒l��ێ�
        int prevHp = Hp.Value;

        //Hp���X�V(0�ȉ��ɂ͂��Ȃ�)
        hp.OnNext(Mathf.Max(0,Hp.Value - damage));

        if (Hp.Value <= 0)
        {
            attackNumber.OnNext(0);
            AddStatusEffect(StatusEffect.Dead);
        }

        //Hp�̕ω���Ԃ�
        return prevHp - Hp.Value;
    }

    /// <summary>
    /// �U���\�񐔂�߂�
    /// </summary>
    public void ResetAttackNum()
    {
        attackNumber.OnNext(initialAttackNum);
    }

    public void OnDead()
    {
        //�̂ĎD�Ɉړ�
        Debug.Log("���S");
    }

    #endregion

    #region private

    /// <summary>
    /// 1�^�[���ɉ���U���ł��邩 (�ʏ��1��)
    /// </summary>
    private int initialAttackNum = 1;

    /// <summary>
    /// ��Ԉُ�̒ǉ�
    /// </summary>
    /// <param name="statusEffect"></param>
    public void AddStatusEffect(StatusEffect statusEffect)
    {
        statusEffects.Add(statusEffect);
    }

    /// <summary>
    /// ��Ԉُ�̏���
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
            throw new System.Exception("�U���\�񐔂�0�ł�");
        }
        attackNumber.OnNext(n - 1);

    }

    /// <summary>
    /// ATK��ύX�B������0�����ɂ͂Ȃ�Ȃ�
    /// </summary>
    /// <param name="delta"></param>
    public void ChangeAtk(int delta)
    {
        int temp = Mathf.Max(0, Atk.Value + delta);
        atk.OnNext(temp);
    }

    /// <summary>
    /// Hp��ύX�B�������ǉ��̂�
    /// </summary>
    /// <param name="delta"></param>
    public void AddHp(uint delta)
    {
        int temp = Mathf.Max(0, Hp.Value + (int)delta);
        hp.OnNext(temp);
    }
    #endregion
}

