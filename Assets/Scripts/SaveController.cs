using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class SaveData
{
    public string scene;
    public Dictionary<string, string> yarnStrings = new Dictionary<string, string>();
    public Dictionary<string, float> yarnFloats = new Dictionary<string, float>();
    public Dictionary<string, bool> yarnBools = new Dictionary<string, bool>();
}

public class SaveController : MonoBehaviour
{
    InMemoryVariableStorage inMemoryVariableStorage;
    public SaveData saveData;
    public string startScene;

#if UNITY_EDITOR
    string saveFilePath = "./save.json";
    Formatting formatting = Formatting.Indented;
#else
    string saveFilePath = Application.persistentDataPath + "/save.json";
    Formatting formatting = Formatting.None;
#endif

    void Start()
    {
        inMemoryVariableStorage = FindObjectOfType<InMemoryVariableStorage>();
        saveData = new SaveData();

        if (!File.Exists(saveFilePath))
        {
            saveData.scene = startScene;
            Save();
        }

        saveData = JsonConvert.DeserializeObject<SaveData>(File.ReadAllText(saveFilePath));

        try
        {
            inMemoryVariableStorage.SetAllVariables(saveData.yarnFloats, saveData.yarnStrings, saveData.yarnBools);
        }
        catch { }

        SceneManager.LoadScene(saveData.scene);
    }

    public void Save()
    {
        var variables = inMemoryVariableStorage.GetAllVariables();

        try
        {
            saveData.yarnFloats = variables.Item1;
            saveData.yarnStrings = variables.Item2;
            saveData.yarnBools = variables.Item3;
        }
        catch { }

        File.WriteAllText(saveFilePath, JsonConvert.SerializeObject(saveData, formatting));
    }
}
