using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGM : MonoBehaviour
{
    [SerializeField] Slider Slider;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("haha", 0.001f);
    }

    public void ChangeVolume()
    {
        AudioListener.volume = Slider.value;
        GameObject.FindGameObjectWithTag("SaveCtrl").GetComponent<DataController>().gameDate.sound = Slider.value;
        Save();
    }

    private void Load()
    {
        Slider.value = GameObject.FindGameObjectWithTag("SaveCtrl").GetComponent<DataController>().gameDate.sound;
        GameObject.FindGameObjectWithTag("SaveCtrl").GetComponent<DataController>().LoadGameData();
    }
    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", Slider.value);
        GameObject.FindGameObjectWithTag("SaveCtrl").GetComponent<DataController>().SaveGameData();
    }

    public void haha()
    {
        PlayerPrefs.SetFloat("musicVolume", GameObject.FindGameObjectWithTag("SaveCtrl").GetComponent<DataController>().gameDate.sound);
        Load();
    }
}