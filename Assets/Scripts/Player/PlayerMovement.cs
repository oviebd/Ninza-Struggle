using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{

    public static PlayerMovement instance;
    public VariableUtilities.PlayerNamesEnum playerType;
    public string playerName;
    private Rigidbody2D _rb;
    private float _coolDownTime = .5f;
    private float _spawnTime = 0.0f;

    float player_speed = 8.0f;
    float movement;

    private Animator _anim;
    string _anim_player_speed = "player_speed";
    string _anim_is_shoot = "is_shoot";
    string _anim_is_death = "is_death";

    public GameObject weaponPrefab;
    public GameObject weaponParentObj;

    bool isPlayerDeath = false;
    public bool isPlayerWin = false;
    float inputData;
    float prevFrameInput_input = 0;
    float multiplier = 0;
    bool isGoRight = true;
    Joystick joyStick;

    private AudioSource _audioSource;
    public AudioClip dieClip;

    public Color weaponColor;

    private Collider2D _collider;
    private bool _canCollide = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        _canCollide = false;
        _spawnTime = Time.time;
        joyStick = FindObjectOfType<FloatingJoystick>();
        //  joyStick = FindObjectOfType<FixedJoystick>();
        _collider = GetComponent<Collider2D>();
        //  _collider.enabled = true;
        isPlayerWin = false;
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        // joystick = FindObjectOfType<Joystick>();
    }

    public void SetInputData(float data)
    {
        inputData = data;
        movement = data;
        // Debug.Log("Movement " + movement);
    }


    // Update is called once per frame
    void Update()
    {

      /*if ((Time.time - _spawnTime) >= _coolDownTime)
        {
            _canCollide = true;
            // _collider.enabled = true;
        }
        else
            return;*/

        if (joyStick == null)
            return;

        if ((Time.time - _spawnTime) >= _coolDownTime)
        {
            _canCollide = true;
            // _collider.enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isPlayerDeath == false)
        {
            Shoot();
        }

        float inputData = joyStick.Horizontal;

        // Debug.Log("Input Data : " + inputData);

        if ((inputData >= -0.08 && inputData <= 0) || (inputData <= 0.08 && inputData >= 0))
        {
            _anim.SetFloat(_anim_player_speed, 0.0f);
            Debug.Log("Stop");
            return;

        }

        float diff = inputData - prevFrameInput_input;
        Debug.Log("curr : " + inputData + " - prev " + prevFrameInput_input + "  = diff : " + diff);
        if (diff > 0.05f)
        {
            isGoRight = true;
            Debug.Log("go Right");
            prevFrameInput_input = inputData;
        }
        else if (diff < -.05f)
        {
            isGoRight = false;
            Debug.Log("go Left");
            prevFrameInput_input = inputData;
        }
     
        if (isGoRight)
        {
            Debug.Log("Final destination : Right");
            multiplier = 1;
        }
        else
        {
            multiplier = -1;
            Debug.Log("Final destination : left");
        }
          movement = multiplier * player_speed;
           prevFrameInput_input = inputData;
           //   Debug.Log("Input data : " + inputData + "  Movement = " +- movement);
           _anim.SetFloat(_anim_player_speed, movement);
           MovePlayer();
    }

    public void Shoot()
    {
        if (isPlayerDeath == false)
        {
            _anim.SetTrigger(_anim_is_shoot);
            InstantiateWeapon();
        }

    }

    public void InstantiateWeapon()
    {
        //  Debug.Log("Weapon Instantiate......");
        GameObject weaponObj = Instantiate(weaponPrefab, weaponPrefab.transform.position, Quaternion.identity);
        // weaponObj.GetComponent<weaponMovement>().SetUp(weaponColor);

        weaponObj.transform.SetParent(weaponParentObj.transform, false);
        weaponObj.transform.localRotation = weaponPrefab.transform.rotation;

        weaponObj.transform.parent = null;

    }



    void MovePlayer()
    {
        if (isPlayerDeath == false)
        {
            if (isPlayerWin == true)
            {
                movement = 1 * player_speed;
            }
            _rb.MovePosition(_rb.position + new Vector2(movement * Time.deltaTime, 0f));

            /*   if (movement < 0)
               {
                  // transform.Rotate(transform.rotation.x, 180, transform.rotation.z);
               }
               if(movement>0)
                   //transform.Rotate(transform.rotation.x, 0, transform.rotation.z);*/

        }
    }

    public void OnPlayerWin()
    {
        player_speed = 6.0f;
        isPlayerWin = true;
        gameObject.GetComponent<Collider2D>().isTrigger = true;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject gobj = collision.gameObject;
        OnPlayerHit(gobj);
    }


    private void OnPlayerHit(GameObject hitObj)
    {
        if (hitObj.tag == "ball" && isPlayerDeath == false && _canCollide == true)
        {
            Destroy(hitObj);
            _rb.isKinematic = true;
            _rb.velocity = Vector2.zero;
            _collider.enabled = false;

            isPlayerDeath = true;
            _anim.SetTrigger(_anim_is_death);
            //Die animation length is 0.48s . so we wait for .8 for finishing the animation and restart the game...

            if (dieClip != null && Utility.getIsSoundOn())
            {
                _audioSource.clip = dieClip;
                _audioSource.Play();
            }
            Invoke("Die", 1.0f);
        }
    }

    private void Die()
    {
        FindObjectOfType<GameManager>().EndGame();
        Destroy(gameObject);
    }

    public void OnCollectableCollected(Collectable collectable)
    {
        SoundManager.instance.PlayCollectableCollectSound();
        Utility.IncreaseSpecificZemNo(collectable.type);
        //   Debug.Log("Player Collected Collectable type : " + collectable.type + " Value : " +collectable.value );
    }
}
