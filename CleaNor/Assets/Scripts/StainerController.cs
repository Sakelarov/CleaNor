using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StainerController : MonoBehaviour
{
    [HideInInspector] public enum State { idle, garbage, shoot };
    [HideInInspector] public State state = State.idle;
    private Animator anim;

    //[SerializeField] private GameObject bulletsHolder;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Image bulletLoader;
    [SerializeField] private float loadSpeed;
    [SerializeField] private GameObject garbage;
    private bool isGarbageThrown = false;

    private float fillAmount = 0;

    private StainSpawner stainSpawner;

    private float timeBeforeCreatingStain = 1.5f;
    private Vector3 stainPosition;
    private bool isOnSamePosition = false;
    private StainController currentStain;

    void Start()
    {
        anim = GetComponent<Animator>();
        stainSpawner = GameObject.Find("StainsSpawner").GetComponent<StainSpawner>();
    }

    void Update()
    {
        MakeStains();
        ShootBullets();
        ThrowGarbage();

        anim.SetInteger("state", (int)state);
    }

    private void MakeStains()
    {
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0 && StainSpawner.Instance.isGameRunning)
        {
            if (!isOnSamePosition)
            {
                timeBeforeCreatingStain -= Time.deltaTime;

                if (timeBeforeCreatingStain < 0)
                {
                    stainPosition = new Vector3(this.transform.position.x, this.transform.position.y - 1.5f);
                    currentStain = stainSpawner.SpawnStainInPosition(stainPosition);

                    timeBeforeCreatingStain = 1;
                    isOnSamePosition = true;
                }
            }
            else
            {
                currentStain.GetDirty(Time.deltaTime);
            }

        }
        else
        {
            timeBeforeCreatingStain = 1;
            isOnSamePosition = false;
        }
    }

    private void ShootBullets()
    {
        fillAmount += Time.deltaTime * loadSpeed;
        bulletLoader.fillAmount = fillAmount;

        if (fillAmount >= 1 && StainSpawner.Instance.isGameRunning)
        {
            fillAmount = 0;
            isGarbageThrown = false;
            state = State.shoot;
        }
    }

    private void ThrowGarbage()
    {
        if (!isGarbageThrown && fillAmount >= 0.5f && StainSpawner.Instance.isGameRunning)
        {
            state = State.garbage;
        }
    }

    public void Garbage() // called from animation state garbage
    {
        Instantiate(garbage).transform.position = this.transform.position;
        isGarbageThrown = true;
        state = State.idle;
    }

    public void Shoot() // called from animation state shoot
    {
        for (int i = 1; i < 6; i++)
        {
            Instantiate(bulletPrefab).GetComponent<BulletController>().SetupBullet(i, transform.position);
        }

        state = State.idle;
    }
}
