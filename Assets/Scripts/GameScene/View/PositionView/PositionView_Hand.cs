using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class PositionView_Hand : PositionView
{
    [SerializeField] private TextMeshProUGUI manaText;

    public override void Initialize()
    {
        base.Initialize();
        var Mana = ((IHandPosition)PositionLocatorInfo.I.Resolve(IsPlayer, Pos.Hand)).Mana;
        Mana.Subscribe(
            m=> manaText.text = m.ToString()
        );
        manaText.text = Mana.Value.ToString();
    }
    public override Pos Pos => Pos.Hand;
}
