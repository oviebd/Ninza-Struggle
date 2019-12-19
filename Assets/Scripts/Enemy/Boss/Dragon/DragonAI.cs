using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragonAI : Boss {

    private InitializeGameStage _initializeStage;
    public Vector2 force;

    Vector2 tempForce;
    private bool canMove;
    LevelManager _levelManager;

    AudioSource _roarAudio;
    public int numberOfCelaAtATime;
    public List<GameObject> celeBela;
    GameObject _current_cela;

    SpriteRenderer _spriteRenderer;

    public Animator  effect_animator;

   // [SerializeField] private Image healthImage;

    void Start()
    {
        tempForce = force;
        canMove = true;
        prev_shoot_time = Time.time;
        SetNextDestination();
        _roarAudio = GetComponent<AudioSource>();
        _levelManager = FindObjectOfType<LevelManager>();
        _initializeStage = FindObjectOfType<InitializeGameStage>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        UiManager.instance.ShowBossHealthPanel();

    }

    void Update()
    {
        if(canMove)
            Move();

        if (Time.time - prev_shoot_time >= shootRate)
        {
            StartCoroutine(PrepareForShoot());
        }
    }

    IEnumerator PrepareForShoot()
    {
        prev_shoot_time = Time.time;
        canMove = false;
        Roar();
        yield return new WaitForSeconds(0.5f);
        Shoot();
        yield return new WaitForSeconds(0.5f);
        canMove = true;
    }

    public override void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetedPosition, speed * Time.deltaTime);

        if (transform.position == targetedPosition)
        {
            Invoke("SetNextDestination",0.1f);
            //SetNextDestination();
        }
    }

    public override void SetNextDestination()
    {
        Vector3 p1 = position1.transform.position;
        Vector3 p2 = position2.transform.position;

        targetedPosition = new Vector3(Random.Range(p1.x, p2.x), Random.Range(p1.y, p2.y), Random.Range(p1.z, p2.z));
      
        if(transform.position.x < targetedPosition.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            tempForce.x = force.x;
        }
        else
        {
            //180
            transform.rotation = Quaternion.Euler(0, 180, 0);
            tempForce.x = -1 * force.x;
        }
    }

    public override void Shoot()
    {
        // Debug.Log("Shoot");
        

        GameObject enemyObj = Instantiate(shootPrefab, shootPrefab.transform.position, Quaternion.identity);
        enemyObj.transform.SetParent(bulletParent.transform, false);
        enemyObj.GetComponent<Enemy_Behaviour>().SetForce(tempForce);
        enemyObj.transform.parent = null;
    }


    void Roar()
    {
        _roarAudio.Play();

        for (int i = 0; i < numberOfCelaAtATime; i++)
        {
            int prefab_index = Random.Range(0, celeBela.Count);
            GameObject cela_prefab = celeBela[prefab_index];

            _initializeStage.InstantiateBossCelaBela(cela_prefab);
        }
    }

   
    public override void Hurt(int damage_amount)
    {
        current_health = current_health - damage_amount;
        BossHealthBarController.instance.SetBossHealthBar(current_health, 100);
        //healthImage.fillAmount = current_health*1.00f / 100.00f;

        if (current_health <= 0 && isDie == false)
        {
            Destroy(effect_animator.gameObject);
            Die();
        }
        else
        {
            effect_animator.SetTrigger("is_hit");
        }
    }

    public override void Die()
    {
        isDie = true;
        _spriteRenderer.enabled = false;
        _initializeStage.ClearAllEnemy();
        Destroy(gameObject,0.5f);
        _levelManager.GoNextLevel();
    }

  

}
