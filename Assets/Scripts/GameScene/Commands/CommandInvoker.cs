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
        /// ���s�R�}���h�̗���
        /// </summary>
        public List<CommandBase> History { get;private set; } = new List<CommandBase>();

        /// <summary>
        /// �R�}���h�����s
        /// </summary>
        /// <param name="command"></param>
        public void Invoke(CommandBase command)
        {
            //�f�o�b�O���[�h�Ȃ炷�ׂẴR�}���h�����s
            //�f�o�b�O���[�ǂłȂ��Ȃ�΁A�f�o�b�O�R�}���h�͎��s���Ȃ�
            if(Settings.I.DebugMode || !(command is ICommandForDebugging))
            {
                //�����ɒǉ�
                History.Add(command);

                //�R�}���h�����s
                command.Execute();

                if(Settings.I.DebugMode)
                {
                    Debug.Log(command.Result);
                }
            }
        }
    }
}
