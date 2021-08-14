
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool canBeUsed = true;
    bool isCatching;
    private bool hasCalledPlayerForCheating = false;

    Animator animator; 
    Camera mainCamera;
    GameManager gameManager;
    AudioSource audioSource;

    [SerializeField] private AudioClip[] pickupSounds;
    [SerializeField] private AudioClip[] cheatingSounds;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip poofSound;
    [SerializeField] private AudioClip winSound;
    
    
    
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        mainCamera = FindObjectOfType<Camera>();
        gameManager = FindObjectOfType<GameManager>();
        audioSource = GetComponent<AudioSource>();
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
                    PlayRandomSound(pickupSounds);
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

            if (other.gameObject.tag == "Openings")
            {
                Debug.Log("Cheater");
                CallPlayerForCheating();
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
        audioSource.Stop();
        audioSource.PlayOneShot(winSound);
    }

    private void Die()
    {
        gameManager.LoseGame();
        
        isCatching = false;
        animator.SetBool("InAir", false);
        animator.ResetTrigger("Pickup");
        animator.SetTrigger("Die");
        audioSource.Stop();
        audioSource.PlayOneShot(deathSound);
        canBeUsed = false;
    }

    private void PlayRandomSound(AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        audioSource.Stop();
        audioSource.PlayOneShot(clips[randomIndex]);
    }

    private void CallPlayerForCheating()
    {
        if (hasCalledPlayerForCheating)
        {
            return;
        }

        hasCalledPlayerForCheating = true;
        PlayRandomSound(cheatingSounds);
    }

    private void PlayPoofSound()
    {
        audioSource.PlayOneShot(poofSound);
    }


    private void DestroyOnAnimationFinish()
    {
        Destroy(gameObject);
    }
}

  
