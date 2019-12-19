using UnityEngine;
using UnityEngine.EventSystems;


public class PointerListener : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    public bool isPressed = false;
    public int code;

    private PlayerMovement _playerMovement;


    private void Start()
    {
        _playerMovement = FindObjectOfType<PlayerMovement>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log ("OnPointer Down");
        isPressed = true;
        ChecKButtonEvents();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log ("OnPointer Enter");
        //Pressed = true;
        //  ChecKButtonEvents();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log ("OnPointer Exit");
        isPressed = false;
        ChecKButtonEvents();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
        ChecKButtonEvents();
    }

    void ChecKButtonEvents()
    {
        if(_playerMovement ==null)
        {
         //   _playerMovement = FindObjectOfType<PlayerMovement>();
        }
       /* if (isPressed) 
        //    _playerMovement.PlayerMovementInputBtnPressed(code);
        else
        //    _playerMovement.PlayerMovementInputBtnPressed(-1);

    */

    }


}
