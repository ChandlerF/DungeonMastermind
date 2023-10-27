using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Networking.Types;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    //Add Colors
    //Add List of Indexed Players?    I think it's already in Input Manager
    //Consider how determining the DM will go

    //  TODO:
    //Find Attack Animations
    //Implement Ability System
    //Add Sound
    //Add Death
    //Then Add Enemies
    //Learn: Unity and C# Events, Delegates, Classes, Structs

    private void Awake()
    {
        if(GameManager.Instance == null)
        {
            GameManager.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }
    }


    
}
