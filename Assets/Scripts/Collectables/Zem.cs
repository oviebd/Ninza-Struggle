using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zem : MonoBehaviour {

    [HideInInspector]  public CollectableBehaviour collectableBehaviour;
    private bool isZemFreeToFall = false;
    float downSpeed = 5.0f;

    Collider2D _collider;


	void Start () {
        _collider = GetComponent<Collider2D>();
        collectableBehaviour = GetComponentInParent<CollectableBehaviour>();
    }

    private void Update()
    {
        if(isZemFreeToFall)
            transform.Translate(Vector3.down * downSpeed * Time.deltaTime);
    }

    public void StartZemActivity()
    {
        isZemFreeToFall = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
     
        //   Debug.Log("Zem Collided : "  + obj.name);

        switch (obj.tag)
        {
            case "Player":
                OnPlayerCollided(obj);
                break;

            case "Wall":
                DestroyParent();
                break;
        }
    }


    void OnPlayerCollided(GameObject playerObj)
    {
        isZemFreeToFall = false;
    //    _collider.enabled = false;
      //  Debug.Log("Player Collected ZEM");
        PlayerMovement playerMovement =  playerObj.GetComponent<PlayerMovement>();

        if (playerMovement != null)
        {
            playerMovement.OnCollectableCollected(collectableBehaviour);
        }

        collectableBehaviour.PlayCollectableCollectAnimation(gameObject.transform.localPosition);

        DestroyParent();
    }
    void DestroyParent()
    {
        Destroy(gameObject.transform.parent.gameObject, 0.1f);
    }
}
