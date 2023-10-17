using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterSplat : MonoBehaviour
{
    private AudioSource _AudioSource;

    // Start is called before the first frame update
    void Start()
    {
        _AudioSource = GetComponent<AudioSource>();
        _AudioSource.Play();
        Destroy(gameObject, 1.0f);
    }

}
