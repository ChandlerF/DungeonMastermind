using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class WeaponHandler : MonoBehaviour
{
     private Weapon _weapon = null;
    [SerializeField] private GameObject _weaponObject;
    [SerializeField] private Transform _weaponPostion;
    [SerializeField] private bool _startsWithWeapon = true;

    private void Start()
    {
        if (_startsWithWeapon)
        {
            _weapon = _weaponObject.GetComponent<Weapon>();
            SetupWeapon(_weaponObject);
        }
    }

    public void Fire1(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if(_weapon != null)
            {
                _weapon.WeaponAttack();
            }
        }
    }

    public void SetupWeapon(GameObject weapon)
    {
        _weapon = weapon.GetComponent<Weapon>();
        _weapon.HostileLayers = GetComponent<Entity>().HostileLayers;

        weapon.transform.position = _weaponPostion.position;
        _weapon.gameObject.transform.parent = _weaponPostion;


        GameObject fx = _weapon.transform.GetChild(0).gameObject;

        fx.transform.parent = _weaponPostion;
    }
}
