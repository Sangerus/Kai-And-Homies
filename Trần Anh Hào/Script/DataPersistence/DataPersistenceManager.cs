using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("Play from any scene")]
    [SerializeField] private bool initializeDataIfNull = false;

    [Header("File Storage Config")]

    [SerializeField] private string fileName;

    public GameData gameData;

    private List<IDataPersistence> dataPersistencesObjects;
    private FileDataHandler dataHandler;

    public static DataPersistenceManager instance { get; private set; }

    public void Awake()
    {
        if (instance != null)
        {
            Debug.Log("There are more than one Data Persistence Manager in scene");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);    

    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.dataPersistencesObjects = FindAllDataPersistenceObjects();
        LoadGame();
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            ResetGameDataKeep();
            SaveGame();
        }

        
    }


    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void SaveGame()
    {
        if (this.gameData == null)
        {
            Debug.LogWarning("No data found. New game need to start first");
            return;
        }

        foreach (IDataPersistence dataPersistenceObj in dataPersistencesObjects)
        {
            dataPersistenceObj.SaveData(gameData);
        }
        dataHandler.Save(gameData);

    }

    public void LoadGame()
    {
        this.gameData = dataHandler.Load();

        if (this.gameData == null && initializeDataIfNull == true)
        {
            NewGame();
        }

        if (this.gameData == null)
        {
            Debug.Log("No data found. New game must be start first");
            return;
        }

        foreach (IDataPersistence dataPersistenceObj in dataPersistencesObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }

    }

    public void ResetGameDataKeep()
    {
        if (gameData == null) return;

        // Đảm bảo các dữ liệu khác được xóa
        gameData.starCollected.Clear();
        GameManager.instance.ResetStar();
        
    }



    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>(true).OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }

    public bool HasGameData()
    {
        return gameData != null;
    }
}
