using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuSet : MonoBehaviour
{
    GameObject menu;
    GameObject slider;
    GameObject opacity;
    Button resume;
    // Start is called before the first frame update
    void Start()
    {
        menu = GameObject.Find("MenuSet");
        slider = GameObject.Find("Slider");
        if (menu.transform.Find("resume"))
            resume = menu.transform.Find("resume").GetComponent<Button>();
        if (menu.transform.Find("opacity"))
            opacity = menu.transform.Find("opacity").gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        if (menu.transform.Find("resume") && resume != null)
            resume.onClick.AddListener(ActiveFalse);

        if (SceneManager.GetActiveScene().name == "GameScene" && Input.GetKeyDown(KeyCode.Escape))
        {
            if (isActive())
                ActiveFalse();

            else
                ActiveTrue();
        }
    }

    bool isActive()
    {
        bool check = true;
        for (int i = 0; i < 5; i++)
        {
            if (!menu.transform.GetChild(i).gameObject.activeSelf)
            {
                check = false;
                return false;
            }
        }
        for (int i = 0; i < 3; i++)
        {
            if (!slider.transform.GetChild(i).gameObject.activeSelf)
            {
                check = false;
                return false;
            }
        }

        return check;
    }
    public void ActiveFalse()
    {
        for (int i = 0; i < 5; i++)
            menu.transform.GetChild(i).gameObject.SetActive(false);
        for (int i = 0; i < 3; i++)
            slider.transform.GetChild(i).gameObject.SetActive(false);
        if (menu.transform.Find("opacity"))
            opacity.SetActive(false);
        if (SceneManager.GetActiveScene().name == "GameScene")
        Time.timeScale = 1;

    }

    public void ActiveTrue()
    {
        if (SceneManager.GetActiveScene().name == "GameScene")
            Time.timeScale = 0;
        for (int i = 0; i < 5; i++)
            menu.transform.GetChild(i).gameObject.SetActive(true);
        for (int i = 0; i < 3; i++)
            slider.transform.GetChild(i).gameObject.SetActive(true);
        if (menu.transform.Find("opacity"))
            opacity.SetActive(true);
    }
}