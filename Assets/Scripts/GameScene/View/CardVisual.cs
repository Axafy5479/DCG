using GameInfo;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class CardVisual : MonoBehaviour,IPointerClickHandler,IBeginDragHandler,IDragHandler,IDropHandler,IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject deckCardObjact, handCardObjact, fieldCardObjact, discardCardObject;
    [SerializeField] private CanvasGroup cg;
    [SerializeField] private SelectCardButton selectButton;
    [SerializeField] private TextMeshProUGUI name_text_hand,cost_text, gameId_text,descriptionText,name_text_field, descriptionText_field;
    [SerializeField] private GameObject playableAura;

    private Dictionary<Pos, GameObject> cardObjectMap = new Dictionary<Pos, GameObject>();


    private CardState_Base state;
    public ICardInfo CardInfo { get; private set; }

    protected bool Showing
    {
        get
        {
            if (CardInfo.IsPlayer)
            {
                return state.Pos == Pos.Hand || state.Pos == Pos.Field;
            }
            else
            {
                return state.Pos == Pos.Field;
            }
        }
    }

    public void Initialize(ICardInfo cardInfo)
    {
        CardInfo = cardInfo;

        cardObjectMap = new Dictionary<Pos, GameObject>()
        {
            {Pos.Deck,deckCardObjact },
            {Pos.Hand,handCardObjact },
            {Pos.Field,fieldCardObjact },
            {Pos.Discard,discardCardObject },
        };

        if (!CardInfo.IsPlayer)
        {
            cardObjectMap[Pos.Hand] = deckCardObjact;
            handCardObjact.SetActive(false);
        }

        View_Initialize();
        RX_Initialize();
    }

    protected virtual void View_Initialize()
    {
        state = GetState(CardInfo.Pos.Value);
        cost_text.text = CardInfo.Cost.Value.ToString();
        ChangeCardObject(CardInfo.Pos.Value);
        gameId_text.text = CardInfo.GameId.ToString();
        CheckPlayable(CardInfo.IsPlayable.Value);
        name_text_hand.text = CardInfo.CardBook.CardName;
        descriptionText.text = CardInfo.CardBook.Description;
        descriptionText_field.text = CardInfo.CardBook.Description;
        name_text_field.text = CardInfo.CardBook.CardName;
    }

    protected virtual void RX_Initialize()
    {

        //位置変更が検出されたとき
        CardInfo.Pos.Subscribe(p => {
            state = GetState(p);
            //pos_text.text = p.ToString();
            ChangeCardObject(p);
            AnimationUtility.SetMovingAnimation(this.transform, CardInfo);
            });

        //コスト変更が検出されたとき
        CardInfo.Cost.Subscribe(c =>
        {
           AnimationUtility.SetCostAnimation(cost_text, CardInfo);
        });

        //プレイ可能性が変更されたとき
        CardInfo.IsPlayable.Subscribe(CheckPlayable);
    }


    private void CheckPlayable(bool playable)
    {
        if (!playable)
        {
            playableAura.SetActive(false);
        }
        else {
            if (CardInfo.IsPlayer)
            {
                if(CardInfo.Pos.Value == Pos.Hand || CardInfo.Pos.Value == Pos.Field)
                {
                    playableAura.SetActive(true);
                }

            }
            else
            {
                if (CardInfo.Pos.Value == Pos.Field)
                {
                    playableAura.SetActive(true);
                }
            }

        }
    }


    private CardState_Base GetState(Pos p)
    {
        return p switch
        {
            Pos.Deck => new CardState_Deck(CardInfo),
            Pos.Hand => new CardState_Hand(CardInfo,(p,ability)=>StartCoroutine(new SelectingManager(p).StartSelectng(ability))),
            Pos.Field => new CardState_Field(CardInfo),
            Pos.Discard => new CardState_Discard(CardInfo),
            _ => throw new System.Exception($"{p}に対応するCardState_Baseは存在しません")
        };
    }
    public SelectCardButton SelectButton { get => selectButton;  }

    private void ChangeCardObject(Pos pos)
    {
        foreach (var item in cardObjectMap)
        {
            item.Value.SetActive(item.Key == pos);
        }
    }

    #region D&D

    private Vector3 PrevPos { get; set; }

    public void OnBeginDrag(PointerEventData eventData)
    {
        PrevPos = this.transform.position;
        if (CardInfo.IsPlayable.Value)
        {
            cg.blocksRaycasts = false;
            state.OnBeginDrag();

            if (state.Pos == Pos.Field)
            {
                ArrowController.CreateArrow(CardInfo.IsPlayer, eventData, this.transform);
            }

        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (CardInfo.IsPlayable.Value)
        {
      
            if (state.Pos == Pos.Hand)
            {
                this.transform.position = eventData.position;
            }
            state.OnDrag();
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        state.OnDrop();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!DragCardData.IsDragging || DragCardData.I.DropData == null)
        {
            this.transform.position = PrevPos;
        }

        if (CardInfo.IsPlayable.Value)
        {
            state.OnEndDrag();
        }


        
        cg.blocksRaycasts = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Showing)
        {
            CardDetailPanel.I.Show(CardInfo);
        }
        state.OnClick();
    }

    int initialIndex = 0;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (CardInfo.Pos.Value == Pos.Hand)
        {
            initialIndex = this.transform.GetSiblingIndex();
            this.transform.SetAsLastSibling();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (CardInfo.Pos.Value == Pos.Hand)
        {
            this.transform.SetSiblingIndex(initialIndex);
        }
    }
    #endregion
}
