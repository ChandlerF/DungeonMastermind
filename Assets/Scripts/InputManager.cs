using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.iOS;

public class InputManager : MonoBehaviour
{
    private bool _hasSpawnedArrowPlayer = false;



    private void Update()
    {
        ArrowKeysCheck();
    }


    //If Arrows get pressed, spawn a new Hero
    private void ArrowKeysCheck()
    {
        if (_hasSpawnedArrowPlayer) return;

        if(Input.GetKeyDown(KeyCode.UpArrow) ||
           Input.GetKeyDown(KeyCode.DownArrow) ||
           Input.GetKeyDown(KeyCode.LeftArrow) ||
           Input.GetKeyDown(KeyCode.RightArrow))
        {
            //Spawning new Player Prefab, Setting it's control scheme and had problems setting it's device
            var p1 = PlayerInput.Instantiate(GetComponent<PlayerInputManager>().playerPrefab, controlScheme: "ArrowsKeyboard");
            p1.SwitchCurrentControlScheme("ArrowsKeyboard", Keyboard.current, Mouse.current);
            _hasSpawnedArrowPlayer = true;
        }
    }
}
