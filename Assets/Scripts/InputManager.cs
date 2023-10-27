using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private bool _setEnemy = false;
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

            if (_setEnemy)
            {
                p1.gameObject.layer = 9;
                p1.gameObject.GetComponent<Entity>().HostileLayers |= 0x1 << 8;
            }
            _hasSpawnedArrowPlayer = true;
        }
    }
}
