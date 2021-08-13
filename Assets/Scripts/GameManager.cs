using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] int savedDwarf = 0;
    
    public int dwarfNum; 
    

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

    
}
