using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Boss:MonoBehaviour {

	public Transform position1,position2;
    protected Vector3 targetedPosition;
    public float speed;
   [HideInInspector] public int current_health = 100;
   [HideInInspector]  public bool isDie = false;

    public GameObject shootPrefab;
    public GameObject bulletParent;
    public GameObject weaponPosition;
    public int shootRate;
    protected float prev_shoot_time;

    public abstract void Shoot();
    public abstract void Move();
    public abstract void Hurt(int damage);
    public abstract void Die();
    public abstract void SetNextDestination();
}
