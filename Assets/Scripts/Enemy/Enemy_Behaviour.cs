using UnityEngine;

public class Enemy_Behaviour : MonoBehaviour {

	public Vector2 startForce;
	public GameObject nextBall;
    public float max_velocity = 4.0f;
    public int value;
    private GameObject enemyParent;

    public bool isItBossFire=false;
    public bool isItBossCela = false;

    public bool canItSpilit;

    Rigidbody2D rb;


    void Start () {
     //   canItSpilit = true;
        enemyParent = GameObject.FindGameObjectWithTag("EnemySpawnPoint");
        rb = GetComponent<Rigidbody2D>();
    }

	public void Split ()
	{

       // if (canItSpilit == false)
          //  return;

        InitializeGameStage initializeGameStage = FindObjectOfType<InitializeGameStage>();

        if (nextBall != null && canItSpilit ==true)
		{
            Vector2 position;
            Vector2 initForce = nextBall.GetComponent<Enemy_Behaviour>().startForce;

            if(VariableUtilities.gameMode == VariableUtilities.GameMode.GameModeInfinity)
            {
                Debug.Log("Infinity Mode");
                nextBall.GetComponent<Enemy_Behaviour>().canItSpilit = false;
            }
            else
            {
                nextBall.GetComponent<Enemy_Behaviour>().canItSpilit = true;
            }

            if (rb.position.y <= 2.5)
            {
                // Debug.Log("Split pos : " + rb.position);
                initForce.y = initForce.y * 3.0f;
            }

            for (int i = 0; i < 2; i++)
            {
                if ((i % 2) == 0)
                {
                    position = rb.position + Vector2.right / 4f;
                }
                else
                {
                    position = rb.position + Vector2.left / 4f;
                    initForce.x = -1 * initForce.x;
                }

                initializeGameStage.InstantiateEnemy(position, nextBall, initForce);
                
            }

        }
        else
        {
            if (isItBossCela == false && isItBossFire == false)
            {
                FindObjectOfType<LevelManager>().CheckForStageCleared();
            }
        }

		Destroy(gameObject);
	}

    public Vector2 GetVelocity()
    {
        return rb.velocity;
    }

    public void SetForce(Vector2 force)
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        rb.AddForce(force, ForceMode2D.Impulse);
    }

    public void Die()
    {
        rb.velocity = Vector2.zero;
        Destroy(gameObject, 0.1f);
    }
}
