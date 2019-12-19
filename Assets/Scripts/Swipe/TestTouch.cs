using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TestTouch : MonoBehaviour {
    public Transform player;
    public float speed = 5.0f;
    private bool touchStart = false;
    private Vector2 pointA;
    private Vector2 pointB;

    public Transform circle;
    public Transform outerCircle;

    public PlayerMovement playerMovement;
    bool isLeft=false;
    //bool isJoyStick=false;
    bool isUiClicked=false;

    public GraphicRaycaster raycaster;
    int uiLayer;

    public Rigidbody2D _rb;

    int left_touch_index = -1;

    private void Start()
    {
      uiLayer=   LayerMask.NameToLayer("UI");
      Debug.Log("Ui Layer + " + uiLayer);
      //  circle.transform.position = new Vector2(200,200);
    }

    // Update is called once per frame
    void Update () {

        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.touches[i];
            Vector3 tP = Camera.main.ScreenToWorldPoint(touch.position);
            Debug.DrawLine(Vector3.zero, tP, Color.red);

            if (touch.position.x < Screen.width / 2)
            {
                //Left
                left_touch_index = i;
                isLeft = true;

                if(touch.phase == TouchPhase.Began)
                {
                    pointA = Camera.main.ScreenToWorldPoint(new Vector3(Input.touches[i].position.x, Input.touches[i].position.y, Camera.main.transform.position.z));

                    circle.transform.position = pointA;
                    outerCircle.transform.position = pointA;
                    circle.GetComponent<SpriteRenderer>().enabled = true;
                    outerCircle.GetComponent<SpriteRenderer>().enabled = true;
                }

               else if(touch.phase == TouchPhase.Moved)
                {
                    touchStart = true;
                    pointB = Camera.main.ScreenToWorldPoint(new Vector3(Input.touches[i].position.x, Input.touches[i].position.y, Camera.main.transform.position.z));
                }
                else
                {
                    isLeft = false;
                    touchStart = false;
                    left_touch_index = -1;
                }

            }
            else if (touch.position.x > Screen.width / 2)
            {
                //Right
                isLeft = false;
                left_touch_index = -1;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            

        }

      /*  if (Input.GetMouseButton(0))
        {
            // if ( isUiClicked== true)
            //return;
            touchStart = true;
            //pointB = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
            if (left_touch_index != -1)
            {
                pointB = Camera.main.ScreenToWorldPoint(new Vector3(Input.touches[left_touch_index].position.x, Input.touches[left_touch_index].position.y, Camera.main.transform.position.z));
            }

        }
        else
        {
            isLeft = false;
            touchStart = false;
            left_touch_index = -1;
        }*/

     //   Debug.Log("Left touch " + left_touch_index);
    }

   private void FixedUpdate()
    {
        if (touchStart)
        {
            Vector2 offset = pointB - pointA;
            Vector2 direction = Vector2.ClampMagnitude(offset, 1.0f);
            moveCharacter(direction * 1);

            circle.transform.position = new Vector2(pointA.x + direction.x, pointA.y);
          //  circle.transform.position = new Vector2(pointA.x + direction.x, pointA.y + direction.y);
        }
        else
        {
            circle.GetComponent<SpriteRenderer>().enabled = false;
            outerCircle.GetComponent<SpriteRenderer>().enabled = false;
            moveCharacter(Vector2.zero);
        }

    }

    void moveCharacter(Vector2 direction)
    {
        direction.y = 0;
       // Debug.Log("XXUnity Direction : " + direction);
        _rb.MovePosition(_rb.position + new Vector2(direction.x * speed *  Time.deltaTime, 0f));
        //   player.Translate(direction * speed * Time.deltaTime);
        //playerMovement.SetInputData(direction.x);
        player.GetComponentInChildren<PlayerMovement>().SetInputData(direction.x);
    }
}
