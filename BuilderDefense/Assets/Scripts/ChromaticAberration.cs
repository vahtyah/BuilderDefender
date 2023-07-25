using System;
using UnityEngine;
using UnityEngine.Rendering;

public class ChromaticAberration : MonoBehaviour
{
    public static ChromaticAberration Instance { get; private set; }
    private Volume _volume;

    private void Awake()
    {
        if (Instance) Destroy(gameObject);
        else Instance = this;
        _volume = GetComponent<Volume>();
    }

    private void Update()
    {
        if (_volume.weight > 0)
        {
            var decreaseSpeed = 1f;
            _volume.weight -= Time.deltaTime * decreaseSpeed;
        }
    }

    public void SetWeight(float weight)
    {
        _volume.weight = weight;
    }
}