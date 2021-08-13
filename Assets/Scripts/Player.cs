
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool canBeUsed = true;
    
    private Animator animator;
    Camera mainCamera;
    bool isCatching;

    GameManager gameManager;
    SceneLoader sceneLoader;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        mainCamera = FindObjectOfType<Camera>();
        gameManager = FindObjectOfType<GameManager>();
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!canBeUsed)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast((Vector2) (Camera.main.ScreenToWorldPoint(Input.mousePosition)),
                Vector2.zero, 0);
            if (hit)
            {
                if (hit.collider.gameObject == this.gameObject)
                {
                    animator.SetTrigger("PickUp");
                    animator.SetBool("InAir", true);
                    isCatching = true;
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isCatching = false;
            animator.SetBool("InAir", false);
        }

        if (isCatching && Input.GetMouseButton(0))
        {
            Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0;
            transform.position = mouseWorldPos;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        {
            if (other.gameObject.tag == "Obstacle")
            {
                Die();
            }

            if (other.gameObject.tag == "Finish Target")
            {
                SaveDwarf();
            }
        }
    }

    private void SaveDwarf()
    {
        Debug.Log("Dwarf saved ");
        gameManager.AddSavedDwarf();
        isCatching = false;
        animator.SetBool("InAir", false);
        animator.ResetTrigger("Pickup");
        animator.SetTrigger("Finish");
        canBeUsed = false;
    }

    private void Die()
    {
        isCatching = false;
        animator.SetBool("InAir", false);
        animator.ResetTrigger("Pickup");
        animator.SetTrigger("Die");
        canBeUsed = false;
        StartCoroutine(gameManager.LoseGame());
    }
    

    private void DestroyOnAnimationFinish()
    {
        Destroy(gameObject);
    }
}

  
