using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataController : MonoBehaviour
{
    static GameObject _container;
    static GameObject Container
    {
        get
        {
            return _container;
        }
    }
    static DataController _instance;
    public static DataController Instance
    {
        get
        {
            if (!_instance)
            {
                _container = new GameObject();
                _container.name = "DataController";
                _instance =
                    _container.AddComponent(typeof(DataController)) as DataController;
                DontDestroyOnLoad(_container);
            }
            return _instance;
        }
    }

    public string GameDataFileName = "GalaxyWhopper.json";
    public GameData _gameData;
    public GameData gameDate
    {
        get
        {
            if (_gameData == null)
            {
                LoadGameData();
                _gameData.sound = 1f;
                SaveGameData();
            }
            return _gameData;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
            PlayerPrefs.SetFloat("musicVolume", 1);
        LoadGameData();
        SaveGameData();
    }

    public void LoadGameData()
    {
        string filePath = Application.persistentDataPath + GameDataFileName;

        if (File.Exists(filePath))
        {
            print("불러오기 성공");
            string FromJsonData = File.ReadAllText(filePath);
            _gameData = JsonUtility.FromJson<GameData>(FromJsonData);
        }

        else
        {
            newGame();
        }
    }

    public void newGame()
    {
        print("새로운 파일 생성");
        _gameData = new GameData();
        _gameData.sound = 1f;
        PlayerPrefs.SetFloat("musicVolume", _gameData.sound);
        SaveGameData();
    }

    public void SaveGameData()
    {
        string ToJsonData = JsonUtility.ToJson(gameDate);
        string filePath = Application.persistentDataPath + GameDataFileName;

        File.WriteAllText(filePath, ToJsonData);

        print("저장완료");
        int sum = 1;
        if (gameDate.isClear1) sum++;
        if (gameDate.isClear2) sum++;
        if (gameDate.isClear3) sum++;
        if (gameDate.isClear4) sum++;
        print("현재 " + sum + "단계까지 열렸습니다.");
        print("게임 사운드는 " + gameDate.sound);
        print("스테이지별 별 획득 개수: "+gameDate.starNum1+", "+ gameDate.starNum2 + ", "+ gameDate.starNum3 + ", "+ gameDate.starNum4 + ", "+ gameDate.starNum5);
    }

    private void OnApplicationQuit()
    {
        SaveGameData();
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}