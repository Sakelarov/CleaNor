using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed= 5;
    public float cleaningSpeed = 1;

    private float horizontalInput;
    private float verticalInput;
    private bool canMove = true;

    private GameObject trap;
    private SpriteRenderer rend;
    private StainSpawner spawner;
    private Transform camera;

    [SerializeField] private GameObject[] objectsToBeMoved;
    [SerializeField] private SpriteRenderer broom;

    [HideInInspector] public enum State { idle, walk };
    [HideInInspector] public State state = State.idle;
    private Animator anim;

    public GameObject pauseMenu;


    void Start()
    {
        anim = GetComponent<Animator>();
        spawner = GameObject.Find("StainsSpawner").GetComponent<StainSpawner>();
        camera = Camera.main.transform;
        rend = GetComponent<SpriteRenderer>();

        pauseMenu.SetActive(false);
    }

    
    void Update()
    {
        Move();
        anim.SetInteger("state", (int)state);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.Instance.Pause();
            pauseMenu.SetActive(!pauseMenu.activeSelf);
        }
    }

    private void Move()
    {
        if (canMove)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");

            transform.Translate(Vector2.right * horizontalInput * Time.deltaTime * speed);
            transform.Translate(Vector2.up * verticalInput * Time.deltaTime * speed);

            if (horizontalInput != 0 || verticalInput != 0)
            {
                state = State.walk;
            }
            else
            {
                state = State.idle;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "garbage":
                if (other.gameObject.transform.localScale.x < 0.8f)
                {
                    GameObject.Find("UIManagerGO").GetComponent<UIManager>().score += 3;
                    Destroy(other.gameObject);
                }
                break;
            case "shoes": StartCoroutine("IncreaseSpeed");
                Destroy(other.gameObject);
                break;
            case "bucket": StartCoroutine("IncreaseCleaningSpeed");
                other.gameObject.GetComponent<BucketController>().ResetBucket();
                break;
            case "trap": trap = other.gameObject;
                StartCoroutine("Trap");
                break;
            case "levelTrigger": StartCoroutine("ProceedToNextLevel");
                canMove = false;
                other.gameObject.SetActive(false);
                break;
            
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("garbage"))
        {
            if (other.gameObject.transform.localScale.x < 0.6f)
            {
                GameObject.Find("UIManagerGO").GetComponent<UIManager>().score += 3;
                Destroy(other.gameObject);
            }
        }
        
    }

    private IEnumerator IncreaseSpeed()
    {
        speed = 8;
        yield return new WaitForSeconds(7);
        speed = 7;
        yield return new WaitForSeconds(1);
        speed = 6;
        yield return new WaitForSeconds(1);
        speed = 5;
    }

    private IEnumerator Trap()
    {
        speed = 0;
        for (int i = 0; i < 20; i++)
        {
            if (trap.GetComponent<TrapController>().currentMouse != null)
            {
                Destroy(trap.GetComponent<TrapController>().currentMouse);
            }
            yield return new WaitForSeconds(0.2f);
            rend.color = rend.color == Color.white ? new Color(1, 0.6f, 0.6f, 1) : Color.white;
        }
        if (trap.GetComponent<TrapController>().currentMouse != null)
        {
            Destroy(trap.GetComponent<TrapController>().currentMouse);
        }
        rend.color = Color.white;
        speed = 5;
        StainSpawner.Instance.currentTraps.Remove(trap);
        Destroy(trap);
        spawner.SpawnTrap();
    }

    private IEnumerator IncreaseCleaningSpeed()
    {
        cleaningSpeed = 2;
        for (int i = 0; i < 80; i++)
        {
            yield return new WaitForSeconds(0.1f);
            broom.color = broom.color == Color.white ? new Color(0f, 1f, 1f, 1) : Color.white;
        }

        broom.color = Color.white;
        cleaningSpeed = 1;
    }

    private IEnumerator ProceedToNextLevel()
    {
        for (int i = 0; i < 19; i++)
        {
            yield return new WaitForSeconds(0.1f);
            camera.position = new Vector3(0, camera.position.y + 0.5f, -10);
            foreach (var o in objectsToBeMoved)
            {
                o.transform.position = new Vector3(o.transform.position.x, o.transform.position.y + 0.5f, o.transform.position.z);
            }
        }

        StainSpawner.Instance.StartNextLevel();
        canMove = true;
    }
}
