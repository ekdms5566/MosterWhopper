using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioObject : MonoBehaviour
{
    public AudioClip opening;
    public AudioClip stage;
    public AudioClip game;
    bool SceneCheck1;
    bool SceneCheck2;
    bool SceneCheck3;
    AudioSource BGM;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "StartScene")
        {
            SceneCheck2 = false;
            SceneCheck3 = false;
        }
         
        else if (SceneManager.GetActiveScene().name == "StageScene")
        {
            SceneCheck1 = false;
            SceneCheck3 = false;
        }

        else if (SceneManager.GetActiveScene().name == "GameScene")
        {
            SceneCheck1 = false;
            SceneCheck2 = false;
        }

        BGM =gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!SceneCheck1 && SceneManager.GetActiveScene().name == "StartScene")
        {
            SceneCheck1 = true;
            SceneCheck2 = false;
            SceneCheck3 = false;
            BGM.clip = opening;
            BGM.loop = true;
            Play();
        }

        else if (!SceneCheck2 && SceneManager.GetActiveScene().name == "StageScene")
        {
            SceneCheck1 = false;
            SceneCheck2 = true;
            SceneCheck3 = false;
            BGM.clip = stage;
            BGM.loop = true;
            Play();
        }

        else if (!SceneCheck3 && SceneManager.GetActiveScene().name == "GameScene")
        {
            SceneCheck1 = false;
            SceneCheck2 = false;
            SceneCheck3 = true;
            BGM.clip = game;
            BGM.loop = true;
            Play();
        }

      
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Play()
    {
        BGM.Play();
    }

    public void Stop()
    {
        BGM.Stop();
    }
}
