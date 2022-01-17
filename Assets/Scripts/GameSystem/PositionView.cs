using DAE.HexSystem;
using DAE.GameSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class PositionEventArgs : EventArgs
{
     public Position Position { get; }

    public PositionEventArgs(Position position)
    {
        Position = position;
    }

}


public class PositionView : MonoBehaviour, IDropHandler, IPointerEnterHandler
{
    [SerializeField]
    private UnityEvent OnActivate;

    [SerializeField]
    private UnityEvent OnDeactivate;


    public event EventHandler<PositionEventArgs> Dropped;

    public event EventHandler<PositionEventArgs> Hovered;


    public RectTransform Deck;

    private Position _model;
    public void OnDrop(PointerEventData eventData)
        => OnDropped(new PositionEventArgs(Model));

    public void OnPointerEnter(PointerEventData eventData)
                => OnHover(new PositionEventArgs(Model));

    public Position Model
    {
        set
        {
            if (_model != null)
            {
                _model.Activated -= PositionActivated;
                _model.Deactivated -= PositionDeactivated;
            }

            _model = value;

            if (_model != null)
            {
                _model.Activated += PositionActivated;
                _model.Deactivated += PositionDeactivated;
            }

        }
        get
        {
            return _model;
        }
    }

    //public void Destory()
    //{
       
    //}

    private void PositionDeactivated(object sender, EventArgs e)
        => OnDeactivate.Invoke();

    private void PositionActivated(object sender, EventArgs e)
        => OnActivate.Invoke();



    protected virtual void OnDropped(PositionEventArgs eventArgs)
    {
        var handler = Dropped;
        handler?.Invoke(this, eventArgs);
    }

    protected virtual void OnHover(PositionEventArgs eventArgs)
    {
        var handler = Hovered;
        handler?.Invoke(this, eventArgs);
    }
}
