using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerSound : MonoBehaviour
{
    public AudioClip wrong;
    public AudioClip right;
    public AudioSource soundEffect;
    // Start is called before the first frame update
    void Start()
    {
        soundEffect = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        soundEffect.Play();
    }
}
