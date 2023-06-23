using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class Test : MonoBehaviour
{
    [SerializeField] private GameObject gameObject;
    [SerializeField] private GameObject _gameObject;

    private void Start()
    {
        if (_gameObject != null)
        {
            Debug.Log("gameObject = " + gameObject);
        }
    }
}
