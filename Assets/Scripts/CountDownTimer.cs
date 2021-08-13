using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CountDownTimer : MonoBehaviour
{
    public float startTime = 10f;
    public Text countText;

    float currentTime = 0f;
    SceneLoader sceneLoader;


    // Start is called before the first frame update
    void Start()
    {
        currentTime = startTime;
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        countText.text = currentTime.ToString("0");

        if(currentTime <= 3 )
        {
            countText.color = Color.red;
        }

        if(currentTime <= 0 )
        {
            currentTime = 0;

            sceneLoader.LoadDeadScene();
        }
    }
}
