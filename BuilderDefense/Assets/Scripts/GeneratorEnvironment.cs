using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GeneratorEnvironment : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private int amount;
    [SerializeField] private List<GameObject> bushes;

    private void Start()
    {
        for (int i = 0; i < amount; i++)
        {
            var randomIndex = Random.Range(0, bushes.Count);
            var randomBush = bushes[randomIndex];
            var randomWidth = Random.Range(-width, width);
            var randomHeight = Random.Range(-height, height);
            Instantiate(randomBush, new Vector3(randomWidth, randomHeight, 0),Quaternion.identity,transform);
        }
    }
}