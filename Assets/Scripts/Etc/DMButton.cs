using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DMButton : MonoBehaviour
{
    private List<GameObject> _playerList = new List<GameObject>();

    [SerializeField] private float _startCaptureTimer = 5f, _countdownMultiplier = 1.4f;
    private float _captureTimer;

    private GameObject _currentPlayer, _healthBarObject;

    private HealthBar _healthBar;



    private void Start()
    {
        _captureTimer = _startCaptureTimer;

        SpawnHealthBar();

    }





    private void Update()
    {
        ButtonLogic();
        _healthBar.SetFill(_captureTimer, _startCaptureTimer);
    }







    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.TryGetComponent<PlayerInput>(out _) && !_playerList.Contains(col.gameObject))
        {
            _playerList.Add(col.gameObject);
        }
    }


    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.TryGetComponent<PlayerInput>(out _) && _playerList.Contains(col.gameObject))
        {
            _playerList.Remove(col.gameObject);
        }
    }





    private void ButtonLogic()
    {

        //if theres at least 1 player 
        if (_playerList.Count > 0)
        {

            // it's his progress
            if (_playerList[0] == _currentPlayer)
            {

                //if there's no one contesting that player
                if (_playerList.Count == 1)
                {
                    _captureTimer -= Time.deltaTime;
                }
            }

            //Else not his progress
            else
            {
                //Timer is not Maxed
                if (_captureTimer < _startCaptureTimer)
                {
                    //Reset Timer quickly
                    _captureTimer += Time.deltaTime * _countdownMultiplier;
                }
                else
                {
                    //Set Current player
                    _currentPlayer = _playerList[0];

                    //---------------------------------------------------Set color to the Player's Cape Color----------------------------------------------
                }
            }


        }

        //If no one is contesting
        else
        {
            //If timer is not maxed
            if (_captureTimer < _startCaptureTimer)
            {
                //slowly reset
                _captureTimer += Time.deltaTime;

            }
        }
    }


    private void SpawnHealthBar()
    {

        _healthBarObject = Resources.Load<GameObject>("HealthBarCanvas");
        GameObject _spawnedHealthBar = Instantiate(_healthBarObject, transform.position, Quaternion.identity);
        _healthBar = _spawnedHealthBar.GetComponentInChildren<HealthBar>();
        _healthBar.Target = gameObject;
    }
}
