using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSaver : MonoBehaviour
{
    public static GameSaver Instance;
    private void OnEnable()
    {
        Instance = this;
    }
    private void OnDisable()
    {
        Instance = null;
    }
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    public GameObject player;
    public void SaveGame()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogWarning("No player found to save!"); return;
        }
        string sceneName = SceneManager.GetActiveScene().name;
        float hp = player.GetComponent<UnitBase>().roleStat.dictStats[StatType.HP].GetValue();
        float atkDmg = player.GetComponent<UnitBase>().roleStat.dictStats[StatType.Atk].GetValue();
        GameProgress progress = new GameProgress(sceneName, new List<float> { hp, atkDmg }, player.GetComponent<SkillManager>().skills);

        // Serialize to JSON and save to file
        string json = JsonUtility.ToJson(progress, true);
        string path = Application.persistentDataPath + "/GameProgress.json";
        File.WriteAllText(path, json);
    }

    public void LoadGame()
    {
        string path = Application.persistentDataPath + "/GameProgress.json";
        if (!File.Exists(path))
        {
            Debug.LogWarning("No game progress file found!");
            return;
        }

        string json = File.ReadAllText(path);
        GameProgress progress = JsonUtility.FromJson<GameProgress>(json);

        SceneManager.LoadScene(progress.mapName);
        player.GetComponent<UnitBase>().roleStat.dictStats[StatType.HP].value = progress.playerData[0];
        player.GetComponent<UnitBase>().roleStat.dictStats[StatType.Atk].value = progress.playerData[1];
        player.GetComponent<SkillManager>().skills = progress.skills;
    }
}
public class GameProgress
{   
    public string mapName;
    public List<float> playerData; //default: hp, atk-dmg
    public List<SkillBase> skills;
    //about inventory (add later)

    public GameProgress(string mapName, List<float> playerData, List<SkillBase> skills)
    {
        this.mapName = mapName;
        this.playerData = playerData;
        this.skills = skills;
    }
}
