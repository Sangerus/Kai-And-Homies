using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IDataPersistence
{
    public static GameManager instance;

    public Text starCountText;
    public Text pocketWatchCountText;

    private int currentStar = 0;
    private int totalStar = 3;
    private int currentPocketWatch = 0;
    private int totalPocketWatch = 1;
    public Portal portal;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Giữ lại khi đổi scene

        }
        else
        {
            Destroy(gameObject); // Xóa nếu có instance khác
            return;
        }

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
        starCountText = GameObject.Find("StarText")?.GetComponent<Text>();
        pocketWatchCountText = GameObject.Find("PocketWatchText")?.GetComponent<Text>();
        portal = FindObjectOfType<Portal>();
        if (portal != null && DataPersistenceManager.instance.gameData.pocketWatch == 0)
        {
            portal.gameObject.SetActive(false);
        }

        UpdateUI();
    }



    private void UpdateUI()
    {
        if (starCountText != null)
        {
            starCountText.text = $"{currentStar}/{totalStar}";
        }
        if (pocketWatchCountText != null)
        {
            pocketWatchCountText.text = $"{currentPocketWatch}/{totalPocketWatch}";
        }

    }

    public void UpdateStar(int star)
    {
        currentStar = star;
        starCountText.text = $"{currentStar}/{totalStar}";
    }

    public void UpdatePocketWatch(int pocketWatch)
    {
        currentPocketWatch = pocketWatch;
        pocketWatchCountText.text = $"{currentPocketWatch}/{totalPocketWatch}";

        DataPersistenceManager.instance.gameData.pocketWatch = pocketWatch;
        DataPersistenceManager.instance.SaveGame(); // Lưu trạng thái vào file

        if (currentPocketWatch == totalPocketWatch)
        {
            portal.ShowPortal();
        }
        
    }

    public int GetCurrentStar()
    {
        return currentStar;
    }

    public int GetCurrentPocketWatch()
    {
        return currentPocketWatch;
    }

    public void CollectStar(string starId)
    {
        if (!DataPersistenceManager.instance.gameData.starCollected.ContainsKey(starId))
        {
            DataPersistenceManager.instance.gameData.starCollected[starId] = true;
            UpdateStar(GetCurrentStar() + 1);

            // Lưu game ngay khi thu thập Star
            DataPersistenceManager.instance.SaveGame();
        }
    }

    public bool IsStarCollected(string starId)
    {
        return DataPersistenceManager.instance.gameData.starCollected.ContainsKey(starId) && DataPersistenceManager.instance.gameData.starCollected[starId];
    }

    public void ResetStar()
    {
        currentStar = 0;
        UpdateUI();
    }

    public void LoadData(GameData data)
    {
        currentPocketWatch = data.pocketWatch;
        currentStar = 0;

        if (data.starCollected.Count == 0)
        {
            currentStar = 0; // Nếu danh sách trống, reset số sao
        }
        else
        {
            // Cập nhật số sao dựa trên dữ liệu đã lưu
            foreach (var star in data.starCollected)
            {
                if (star.Value)
                {
                    currentStar++;
                }
            }
        }

        // Hiển thị lại UI nếu không có Star nào được lưu
        UpdateUI();

        // Xóa những sao đã thu thập khỏi màn chơi
        foreach (var star in FindObjectsOfType<Star>())
        {
            if (data.starCollected.ContainsKey(star.id) && data.starCollected[star.id])
            {
                Destroy(star.gameObject);
            }
        }

        // Nếu PocketWatch đã được thu thập, ẩn nó khỏi màn chơi
        PocketWatch pocketWatch = FindObjectOfType<PocketWatch>(true);
        if (currentPocketWatch == 1 && pocketWatch != null)
        {
            pocketWatch.gameObject.SetActive(false);
        }

        if (currentPocketWatch == totalPocketWatch && pocketWatchCountText != null)
        {
            portal.ShowPortal();
        }

        if (FindObjectOfType<Health>() != null)
        {
            FindObjectOfType<Health>().LoadData(data);
        }

        UpdateUI(); // Cập nhật lại giao diện sau khi load dữ liệu
    }





    public void SaveData(GameData data)
    {
        data.pocketWatch = this.currentPocketWatch;

        // Lưu trạng thái Star đã thu thập
        foreach (var star in FindObjectsOfType<Star>())
        {
            if (!star.gameObject.activeSelf) // Nếu Star đã bị ẩn (tức là đã lấy)
            {
                if (!data.starCollected.ContainsKey(star.id))
                {
                    data.starCollected[star.id] = true;
                }
            }
        }

        if (FindObjectOfType<Health>() != null)
        {
            FindObjectOfType<Health>().SaveData(data);
        }
    }

}
