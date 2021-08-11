using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Camera mainCamera;
    bool isCatching;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast((Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition)), Vector2.zero, 0);
            if (hit)
            {
                if (hit.collider.CompareTag("Player"))
                {
                    isCatching = true;
                }
            }
        }
        else if(Input.GetMouseButtonUp(0))
        {
            isCatching = false;
        }
        if (isCatching && Input.GetMouseButton(0))
        {
            Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0;
            transform.position = mouseWorldPos;   
        }
    }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Obstacle")
            {
                Destroy(gameObject);
            }

            if (collision.gameObject.tag == "Finish Target")
            {
                Debug.Log("you win ! ");
            }
        }
    }   
