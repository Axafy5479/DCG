using RX;
using UnityEngine;

namespace Position
{
    /// <summary>
    /// マナの機能を実装するクラス
    /// </summary>
    public class Mana
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="manaText"></param>
        public Mana()
        {
            CurrentMana = new Subject<int>(0);
            InitialMana = 0;
        }

        /// <summary>
        /// 現在所持しているマナ
        /// </summary>
        public Subject<int> CurrentMana
        {
            get => currentMana;
            protected set
            {
                currentMana = value;
            }
        }
        private Subject<int> currentMana;

        /// <summary>
        /// マナは1ターンに1ずつ上昇するが、10を超えて上昇しない
        /// </summary>
        private int LimitMana => 10;

        /// <summary>
        /// ターン初めのマナの量(1ターンに1ずつ上昇)
        /// </summary>
        private int InitialMana { get; set; }

        /// <summary>
        /// 引数分のマナを消費するかを確かめ、
        /// 消費できる場合は消費してtrueを返す
        /// 不可能ならば消費せずにfalseを返す
        /// </summary>
        /// <param name="cost"></param>
        /// <returns></returns>
        public bool TryUseMana(int cost)
        {
            if (CurrentMana.Value < cost)
            {
                return false;
            }
            else
            {
                CurrentMana.OnNext(CurrentMana.Value- cost);
                return true;
            }
        }

        /// <summary>
        /// ターン開始時に呼ぶ
        /// initialManaを1増やし、マナを全回復する
        /// </summary>
        public void NewTurn()
        {
            InitialMana = Mathf.Min(LimitMana, InitialMana + 1);
            CurrentMana.OnNext(InitialMana);
        }
    }

    /// <summary>
    /// デバッグ機能を備えたマナ
    /// </summary>
    public class Mana_Debug : Mana
    {

        /// <summary>
        /// 所持マナを強制的に1増やす
        /// </summary>
        public void AddCurrrentMana()
        {
            CurrentMana.OnNext(CurrentMana.Value+1);
        }
    }
}