using UnityEngine;
using UnityEngine.EventSystems;

public class MobileButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public enum ButtonType
    {
        Forward,
        Reverse,
        Left,
        Right
    }

    public ButtonType buttonType;
    public CarController car;

    public void OnPointerDown(PointerEventData eventData)
    {
        switch (buttonType)
        {
            case ButtonType.Forward:
                car.ForwardDown();
                break;
            case ButtonType.Reverse:
                car.ReverseDown();
                break;
            case ButtonType.Left:
                car.LeftDown();
                break;
            case ButtonType.Right:
                car.RightDown();
                break;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        switch (buttonType)
        {
            case ButtonType.Forward:
                car.ForwardUp();
                break;
            case ButtonType.Reverse:
                car.ReverseUp();
                break;
            case ButtonType.Left:
                car.LeftUp();
                break;
            case ButtonType.Right:
                car.RightUp();
                break;
        }
    }
}
