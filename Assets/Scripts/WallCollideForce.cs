using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollideForce : MonoBehaviour {

    public enum Wall { Left, Right, Up, Down };
    public Wall currentWall;

  //  public bool isLeftWall = false;
  
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject obj = collision.gameObject;
        Enemy_Behaviour enemy_behaviour = obj.GetComponent<Enemy_Behaviour>();

        if (enemy_behaviour == null)
        {
            return;
        }
        else
        {

            switch (currentWall)
            {
                case Wall.Left:
                    if (enemy_behaviour.GetVelocity().x > -enemy_behaviour.max_velocity)
                    {
                        enemy_behaviour.SetForce(new Vector2(-1.0f, 0.0f));
                        // Debug.Log("Left Wall Force change");
                    }
                    break;

                case Wall.Right:
                    if (enemy_behaviour.GetVelocity().x < enemy_behaviour.max_velocity)
                    {
                        //  Debug.Log("Right Wall Force change");
                        enemy_behaviour.SetForce(new Vector2(1.0f, 0.0f));
                    }
                    break;

                case Wall.Down:
                    break;
            }

            if (enemy_behaviour.isItBossFire)
            {
                enemy_behaviour.Die();
            }
                
        }

    }
}
