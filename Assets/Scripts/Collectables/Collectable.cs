using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectable :MonoBehaviour {

    public Transform position1, position2,position3;
    protected Vector3 targetedPosition;
    public float speed;
    public float lifeTime;
    protected float startLifeTime;
    protected bool hasLifeEnded = false;

    public VariableUtilities. collectableType type;
    public int value;

    public abstract void Move();
    public abstract void SetNextDestination();
}
