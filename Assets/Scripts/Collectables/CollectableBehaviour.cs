using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectableBehaviour :Collectable {

    public GameObject bubbleObj;
    private Collider2D _collider2D;

    public SpriteRenderer _collectable_sprite;
 
    private bool _canMove = true;
    private SpriteRenderer spriteBubble;

    void Start()
    {
      //  isItPlayingCollectableAnimation = false;
       // isItCompletePlaingCollectAnimation = false;
        _collider2D = gameObject.GetComponent<Collider2D>();
        spriteBubble = bubbleObj.GetComponent<SpriteRenderer>();

        startLifeTime = Time.time;
        SetNextDestination();
    }

    void Update () {

        if (Time.time - startLifeTime >= lifeTime && hasLifeEnded == false)
        {
          //Debug.Log("Life Sesh");
            hasLifeEnded = true;
            SetNextDestination();
        }

        if(_canMove)
            Move();
    }

    public override void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetedPosition, speed * Time.deltaTime);

        if (transform.position == targetedPosition )
        {

            if (hasLifeEnded == false)
                Invoke("SetNextDestination", 0.1f);
            else
                Destroy(gameObject);


        }
    }


    public override void SetNextDestination()
    {
        Vector3 p1 = position1.transform.position;
        Vector3 p2 = position2.transform.position;

        if (hasLifeEnded == false)
        {
            targetedPosition = new Vector3(Random.Range(p1.x, p2.x), Random.Range(p1.y, p2.y), Random.Range(p1.z, p2.z));
        }
        else
        {
            targetedPosition = GetFinalDestination(p1, p2);
        }
    }

    private Vector3 GetFinalDestination(Vector3 p1, Vector3 p2)
    {
        Vector3 targetedPositionTemp;
        float offsetX = 5f;

        int randomInt = Random.Range(0, 2);
        if (randomInt == 0)
            targetedPositionTemp = p1;
        else
            targetedPositionTemp = p2;

        if (targetedPositionTemp.x <= 0)
            targetedPositionTemp.x = targetedPositionTemp.x - offsetX;
        else
            targetedPositionTemp.x = targetedPositionTemp.x + offsetX;

        targetedPositionTemp = new Vector3(targetedPositionTemp.x, Random.Range(p1.y, p2.y), Random.Range(p1.z, p2.z));
      //  Debug.Log("Final Destination  : " + targetedPosition);

        return targetedPositionTemp;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject obj = collision.gameObject;
       // Debug.Log("Collide With : " + obj.name);

        if (obj.tag == "player_weapon")
        {
            OnBubbleHitByPlayerWeapon();
        }
    }

    void OnBubbleHitByPlayerWeapon()
    {
        _canMove = false;
        _collider2D.enabled = false;
        spriteBubble.enabled = false;

       // bubbleObj.SetActive(false);
        Zem zem = GetComponentInChildren<Zem>();

        if (zem != null)
        {
            zem.StartZemActivity();
        }

    }

    public void PlayCollectableCollectAnimation(Vector3 zemPosition)
    {
        CollectableEffectController.instance.PlayCollectableCollectEffect(_collectable_sprite.sprite,type);
        Debug.Log("PlayCollectableCollectAnimation");
    }

  

}
