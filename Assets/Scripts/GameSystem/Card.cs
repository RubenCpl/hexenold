using DAE.HexSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


[Serializable]

class CardEventArgs : EventArgs
{
    public Card card { get; }

    public CardEventArgs(Card card)
        => Card = card;

    public ICard Card;
}


class Card : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler, ICard
{
    [SerializeField]
    private CardType _cardType;

    [SerializeField]
    private bool _cardActive;

    private Vector3 _startPosition;



    public bool EndDragged = true;

    public CardType CardType => _cardType;

    public bool CardActive => _cardActive;


    public event EventHandler<CardEventArgs> Dragged;

    public void OnBeginDrag(PointerEventData eventData)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        _startPosition = this.transform.position;
        _cardActive = true;
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        var views = FindObjectsOfType<PositionView>();
        OnDragged(this, new CardEventArgs(this));
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        _cardActive = false;
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        transform.position = _startPosition;
    }

    public void CardDestory()
    {
        if (_cardActive)
        {
            _cardActive = false;
            this.gameObject.SetActive(false);
        }
    }


    protected virtual void OnDragged(object source, CardEventArgs e)
    {
        var handle = Dragged;
        handle?.Invoke(this, e);
    }
}


