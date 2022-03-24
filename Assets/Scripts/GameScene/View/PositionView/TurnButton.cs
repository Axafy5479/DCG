using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GameInfo;


public class TurnButton : MonoBehaviour
{
    [SerializeField] private Button button;
    private Image buttonImage;
    private TextMeshProUGUI buttonText;

    public void Initialize()
    {
        buttonImage = GetComponent<Image>();
        buttonText = transform.GetComponentInChildren<TextMeshProUGUI>();
        TurnInfo.I.Turn.Subscribe(ChangeButtonState);
    }

    public void ButtonClicked()
    {
        Command.CommandInvoker.I.Invoke(new Command.Command_TurnChange(true));
        StartCoroutine(new AI().Run());
    }

    private void ChangeButtonState(bool isPlayerTurn)
    {
        button.enabled = isPlayerTurn || Settings.I.DebugMode;
        buttonImage.color = isPlayerTurn ? Color.white : Color.gray;
        buttonText.text = isPlayerTurn ? "ターン終了" : "相手のターン";
        AnimationUtility.ChangeTurn(isPlayerTurn);
    }
}
