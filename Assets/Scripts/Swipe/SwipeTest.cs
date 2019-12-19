using UnityEngine;

public class SwipeTest : MonoBehaviour
{

    public SwipeHandler swipeControls;
    //public Transform player;
    private Vector3 desiredPosition;



    // Update is called once per frame
    void Update()
    {
        if (swipeControls.SwipeLeft)
        {
            Debug.Log("Swipe Left");
        }
      
        if (swipeControls.SwipeRight)
        {
            Debug.Log("Swipe Right");
        }
        if (swipeControls.SwipeUp)
        {
            Debug.Log("Swipe Up");
        }

        if (swipeControls.SwipeDown)
        {
            Debug.Log("Swipe Down");
        }
        if (swipeControls.Tap)
            Debug.Log("Tap!");
    }
}
