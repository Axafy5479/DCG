using Command;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SceneInitializer : MonoBehaviour,WBTransition.ISceneInitializer
{
    [SerializeField] private PositionView[] positions;
    [SerializeField] private HeroView playerHero;
    [SerializeField] private HeroView rivalHero;
    [SerializeField] private bool playerAttackFirst = true;
    [SerializeField] private TurnButton turnButton;

    private Command_Initialize command_init;

    public void AfterOpenMask(Dictionary<string, object> args)
    {
        StartCoroutine(DrawInitialHand());
    }

    private IEnumerator DrawInitialHand()
    {
        for (int i = 0; i < 3; i++)
        {
            command_init.StartDraw();
            yield return new WaitWhile(() => AnimationUtility.IsAnimating);
            yield return new WaitForSeconds(0.11f);
        }

        AnimationUtility.ChangeTurn(true);
        command_init.GameStart();
    }

    public IEnumerator BeforeOpenMask(Dictionary<string, object> args)
    {

        //シングルトンクラスの初期化([?!要修正!?])
        Command.CommandInvoker.Reload();

        //サービスロケーターに登録
        positions.ForEach(p => p.Register());

        //カードDBの生成
        var allCharaJson = Resources.LoadAll<TextAsset>("CardBook/Character");
        CardBook_Chara[] allChara = Array.ConvertAll(allCharaJson, j => JsonUtility.FromJson<CardBook_Chara>(j.text));

        //初期デッキ決定
        var deck_player = System.Array.ConvertAll(PosViewLocator.I.Resolve<PositionView_Deck>(true).InitialDeck, x => System.Array.Find(allChara, c => c.BookId == x));
        var deck_rival = System.Array.ConvertAll(PosViewLocator.I.Resolve<PositionView_Deck>(false).InitialDeck, x => System.Array.Find(allChara, c => c.BookId == x));

        deck_player = deck_player.Shuffle().ToArray();
        deck_rival = deck_rival.Shuffle().ToArray();

        //Command_Initializeのコンストラクターの引数生成
        var initializeData = new Command.Command_Initialize.InitializeData(allChara, deck_player, deck_rival, playerAttackFirst, playerHero.MaxHp, rivalHero.MaxHp);

        //Command_Initializeのインスタンス化&実行
         command_init = new Command.Command_Initialize(true, initializeData);
        CommandInvoker.I.Invoke(command_init);

        //初期化(PositionViewのサービスロケーター登録)
        positions.ForEach(p => p.Initialize());

        //初期デッキ生成
        command_init.SetInitialDeck();


        playerHero.Initialize();
        rivalHero.Initialize();
        turnButton.Initialize();
        yield return new WaitWhile(() => AnimationUtility.IsAnimating);
    }


}
