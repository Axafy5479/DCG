using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Position;

namespace Command
{
    /// <summary>
    /// デバッグ時にのみ使用可能なコマンドが実装するインターフェース
    /// </summary>
    public interface ICommandForDebugging { }

    /// <summary>
    /// すべてのコマンドクラスはこの抽象クラスを継承する
    /// </summary>
    public abstract class CommandBase
    {
        /// <summary>
        /// プレイヤーのコマンドか否か
        /// </summary>
        public bool IsPlayer => isPlayer;
        [SerializeField] private bool isPlayer;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="isPlayer"></param>
        public CommandBase(bool isPlayer)
        {
            this.isPlayer = isPlayer;
        }

        internal void Execute()
        {
            _execute();
            PositionLocator.LI.Judge();
        }

        /// <summary>
        /// コマンドを実行する
        /// </summary>
        protected abstract void _execute();

        /// <summary>
        /// コマンドの名前
        /// </summary>
        public string Result { get; protected set; } = "コマンド実行前";
    }


    [System.Serializable]
    public class Command_Draw : CommandBase, ICommandForDebugging
    {
        public Command_Draw(bool isPlayer) : base(isPlayer) { }

        protected override void _execute()
        {
            ICardInfo card = PositionLocator.LI.Resolve<Deck>(IsPlayer).Draw();
            Result = $"[ForDebugging]{(IsPlayer ? "プレイヤー" : "相手")}: {card}をドロー";
        }
    }

    [System.Serializable]
    public class Command_AddMana :CommandBase, ICommandForDebugging
    {
        public Command_AddMana(bool isPlayer):base(isPlayer)
        {
        }

        protected override void _execute()
        {
            if(PositionLocator.LI.Resolve<Hand>(IsPlayer).ManaManager is Mana_Debug mana)
            {
                mana.AddCurrrentMana();
                Result = $"[ForDebugging]{(IsPlayer ? "プレイヤー" : "相手")}: マナを1追加";
            }
        }
    }
}