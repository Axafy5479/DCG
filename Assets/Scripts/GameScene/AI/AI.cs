using Command;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

public class AI
{
    private ReadOnlyCollection<ICardInfo> PlayerFieldCards => PositionLocatorInfo.I.Resolve(true, Pos.Field).Cards;
    private ReadOnlyCollection<ICardInfo> AiFieldCards => PositionLocatorInfo.I.Resolve(false, Pos.Field).Cards;
    private ReadOnlyCollection<ICardInfo> AiHandCards => PositionLocatorInfo.I.Resolve(false, Pos.Hand).Cards;

    private List<ICardInfo> PlayerFieldPlayableCards => PositionLocatorInfo.I.GetPlayableCards(true, Pos.Field);
    private List<ICardInfo> AiFieldPlayableCards => PositionLocatorInfo.I.GetPlayableCards(false, Pos.Field);
    private List<ICardInfo> AiHandPlayableCards => PositionLocatorInfo.I.GetPlayableCards(false, Pos.Hand);

    private int GetCurrentMana => ((IHandPosition)PositionLocatorInfo.I.Resolve(false, Pos.Hand)).Mana.Value;


    public IEnumerator Run()
    {
        yield return new WaitForSeconds(0.5f);
        SelectPlayCards().ForEach(c=>PlayCard(c));

        yield return new WaitForSeconds(0.5f);

        yield return Attack();
        yield return AttackHero();

        CommandInvoker.I.Invoke(new Command_TurnChange(false));


    }

    /// <summary>
    /// 現状、コストが高いものからプレイする
    /// </summary>
    /// <returns></returns>
    private List<ICardInfo> SelectPlayCards()
    {
        List<ICardInfo> selected = new List<ICardInfo>();
        List<ICardInfo> option = new List<ICardInfo>(AiHandPlayableCards);
        int mana = GetCurrentMana;


        option.Sort((a,b)=>b.Cost.Value-a.Cost.Value);

        foreach (var c in option)
        {
            mana -= c.Cost.Value;
            if (mana >= 0) selected.Add(c);
        }

        return selected;
    }

    /// <summary>
    /// カードをプレイする
    /// </summary>
    /// <param name="card"></param>
    private void PlayCard(ICardInfo card)
    {
        CommandBase command = null;

        //選択式アビリティーが存在するか否かで場合分け
        if (card.AbilityBook is AbilityBook_Selectable)
        {
            //現段階ではCostが最も大きいカードを選択する
            ICardInfo target = PlayerFieldCards.FindMax(c => c.Cost.Value);
            if (target != null)
            {
                command = new Command_Play_WithSelecting(false, card.GameId, 0, target.GameId);
            }
        }
        else
        {
            command = new Command_Play(false, card.GameId, 0);
        }

        if (command != null)
        {
            //コマンドの実行
            CommandInvoker.I.Invoke(command);
        }
    }

    private IEnumerator Attack()
    {
        //攻撃順を順列並べ替えにより決定(インデックスを使用)
        var AllAiChara = AiFieldPlayableCards.ConvertAll(c => c as IBattlerInfo).NonNull().ToList(); 
        var indices = new int[AllAiChara.Count];
        for (int i = 0; i < indices.Length; i++) indices[i] = i;
        var attackCardsPattern = AllPermutation(indices);

        int score = 0;
        List<CommandBase> selectedCommands = null;

        foreach (var attackCards in attackCardsPattern)
        {
            //攻撃順にカードを並べなおす
            List<IBattlerInfo> attackers = new List<IBattlerInfo>();
            foreach (var card in attackCards) attackers.Add(AllAiChara[card]);


            //Playerフィールド上のキャラクターカードを集める
            var playerBattlers = PlayerFieldCards.ConvertAll(c=>c as IBattlerInfo).NonNull().ToList();

            int subScore = 0;

            //ターゲットのパターンをビット全探索
            //最適なターゲット選択を、(撃破したプレイヤーカードのコスト*2 - 死亡したAIカードのコスト)の最大化により計算
            for (int bits = 1; bits < 1<< playerBattlers.Count; bits++)
            {
                List<CommandBase> selectedCommands_sub = new List<CommandBase>();

                List<IBattlerInfo> targets = new List<IBattlerInfo>();
                List<int> hps = new List<int>();
                int targetIndex = 0;

                for (int i = 0; i < playerBattlers.Count; i++)
                {
                    if (((bits >> i) & 1) == 1)
                    {
                        targets.Add(playerBattlers[i]);
                        hps.Add(playerBattlers[i].Hp.Value);
                    }
                }

                foreach (var attacker in attackers)
                {
                    hps[targetIndex] -= attacker.Atk.Value;
                    selectedCommands_sub.Add(new Command_Attack(false,attacker.GameId, targets[targetIndex].GameId)) ;
                    
                    //AIカードが死亡するか否か
                    if (targets[targetIndex].Atk.Value >= attacker.Hp.Value)
                    {
                        //死亡したら減点
                        subScore -= attacker.Cost.Value;
                    }
                    
                    //プレイヤーカードを撃破できるか
                    if (hps[targetIndex] <= 0)
                    {
                        //撃破出来たら加点
                        subScore += (targets[targetIndex]).Cost.Value*2;

                        //以降ターゲットの変更
                        targetIndex++;

                        //プレイヤーカードを全滅できたなら終了
                        if (targetIndex >= targets.Count) break;
                    }

                    
                }

                //スコアが更新出来たなら攻撃パターンを上書き
                if (score < subScore)
                {
                    score = subScore;
                    selectedCommands = selectedCommands_sub;
                }

            }

        }

        if (selectedCommands != null)
        {
            foreach (var item in selectedCommands)
            {
                CommandInvoker.I.Invoke(item);
                yield return new WaitForSeconds(0.5f);
            }
        }
    }


    private IEnumerator AttackHero()
    {

        while (AiFieldPlayableCards.Count>0)
        {
            CommandInvoker.I.Invoke(new Command_AttackHero(false, AiFieldPlayableCards[0].GameId));
            yield return new WaitForSeconds(0.5f);
        }
    }






    /// <summary>
    /// 順列の計算
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="array"></param>
    /// <returns></returns>
    public List<T[]> AllPermutation<T>(params T[] array) where T : IComparable
    {
        var a = new List<T>(array).ToArray();
        var res = new List<T[]>();
        res.Add(new List<T>(a).ToArray());
        var n = a.Length;
        var next = true;
        while (next)
        {
            next = false;

            // 1
            int i;
            for (i = n - 2; i >= 0; i--)
            {
                if (a[i].CompareTo(a[i + 1]) < 0) break;
            }
            // 2
            if (i < 0) break;

            // 3
            var j = n;
            do
            {
                j--;
            } while (a[i].CompareTo(a[j]) > 0);

            if (a[i].CompareTo(a[j]) < 0)
            {
                // 4
                var tmp = a[i];
                a[i] = a[j];
                a[j] = tmp;
                Array.Reverse(a, i + 1, n - i - 1);
                res.Add(new List<T>(a).ToArray());
                next = true;
            }
        }
        return res;
    }



}
