using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponMovement : MonoBehaviour {

    public float speed = 4.0f;
    public int damage_per_attack = 10;

    private AudioSource _audioSource;
    public ParticleSystem glowParticle,trailParticle;

    void Start () {
        _audioSource = GetComponent<AudioSource>();
        if (Utility.getIsSoundOn())
        {
            _audioSource.Play();
        }
        SetUp();
        Destroy(gameObject, 10.0f);
    }
	
   public void SetUp()
     {
        Color weaponColor = PlayerMovement.instance.weaponColor;
      //  gameObject.GetComponent<SpriteRenderer>().color = weaponColor;
        glowParticle.startColor = weaponColor;
        trailParticle.startColor = weaponColor;
     }
	void Update () {
        transform.Translate(Vector3.up*speed*Time.deltaTime);
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject gobj = collision.gameObject;
        //Debug.Log("CollidedObj : " + gobj.name);

        if (gobj.tag == "ball")
        {
            Collider2D collider = GetComponent<Collider2D>();
            collider.enabled = false ;
            Enemy_Behaviour enemyBehaviour= gobj.GetComponent<Enemy_Behaviour>();

            if (VariableUtilities.gameMode == VariableUtilities.GameMode.GameModeInfinity)
            {
                int value = enemyBehaviour.value;
                ScoreManager.instance.AddScore(value);
            }
            enemyBehaviour.Split();
            makeEnemyDie();
           
        }
        else if(gobj.tag == "boss")
        {
            Boss boss = gobj.GetComponent<Boss>();
            boss.Hurt(damage_per_attack);
        }
    }

    void makeEnemyDie()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (renderer != null)
            renderer.enabled = false;
        glowParticle.gameObject.SetActive(false);
        trailParticle.gameObject.SetActive(false);

        Destroy(gameObject,1.5f);
    }
}
