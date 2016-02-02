using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class LeftMovementButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IUpdateSelectedHandler
{
    bool IsPressed = false;
    public void OnPointerDown(PointerEventData data)
    {
        IsPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IsPressed = false;
    }

    public void OnUpdateSelected(BaseEventData eventData)
    {
        if (IsPressed)
            Managers.TurnManager.PlayerTank.GetComponent<Tank_Movement>().MoveLeft();
    }
}
