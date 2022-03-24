using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Command {
    public class CommandInvoker
    {
        private static CommandInvoker instance;
        public static CommandInvoker I => instance ??= new CommandInvoker();
        private CommandInvoker() { }

        public static void Reload()
        {
            instance = new CommandInvoker();
        }

        /// <summary>
        /// 実行コマンドの履歴
        /// </summary>
        public List<CommandBase> History { get;private set; } = new List<CommandBase>();

        /// <summary>
        /// コマンドを実行
        /// </summary>
        /// <param name="command"></param>
        public void Invoke(CommandBase command)
        {
            //デバッグモードならすべてのコマンドを実行
            //デバッグもーどでないならば、デバッグコマンドは実行しない
            if(Settings.I.DebugMode || !(command is ICommandForDebugging))
            {
                //履歴に追加
                History.Add(command);

                //コマンドを実行
                command.Execute();

                if(Settings.I.DebugMode)
                {
                    Debug.Log(command.Result);
                }
            }
        }
    }
}
