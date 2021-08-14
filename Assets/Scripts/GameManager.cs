using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float gameOverTime = 1.5f;
    
    private int dwarfNum; 
    private int savedDwarf = 0;
    
    SceneLoader sceneLoader;

    // Start is called before the first frame update
    void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        dwarfNum = FindObjectsOfType<Player>().Length;
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public void AddSavedDwarf()
    {
        savedDwarf++;
        if(savedDwarf == dwarfNum)
        {
            sceneLoader.LoadWinScene();
        }
    }

    public void LoseGame()
    {
        StartCoroutine(LoseGameRoutine());
    }

    private IEnumerator LoseGameRoutine()
    {
        yield return new WaitForSeconds(gameOverTime);
        sceneLoader.LoadDeadScene();
    }


}
