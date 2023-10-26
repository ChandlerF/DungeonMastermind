using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterRotation : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    void Update()
    {
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, _target.transform.rotation.z * -1.0f);
    }
}
