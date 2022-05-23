using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class SaveData
{
    public string scene = "MainScene";
    public Dictionary<string, string> yarnStrings = new Dictionary<string, string>();
    public Dictionary<string, float> yarnFloats = new Dictionary<string, float>();
    public Dictionary<string, bool> yarnBools = new Dictionary<string, bool>();
}

public class SaveController : MonoBehaviour
{
    InMemoryVariableStorage inMemoryVariableStorage;
    public SaveData saveData;

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

        if (!File.Exists(saveFilePath))
        {
            Save();
        }

        saveData = JsonConvert.DeserializeObject<SaveData>(File.ReadAllText(saveFilePath));
        inMemoryVariableStorage.SetAllVariables(saveData.yarnFloats, saveData.yarnStrings, saveData.yarnBools);
        SceneManager.LoadScene(saveData.scene);
    }

    public void Save()
    {
        var variables = inMemoryVariableStorage.GetAllVariables();
        saveData.yarnFloats = variables.Item1;
        saveData.yarnStrings = variables.Item2;
        saveData.yarnBools = variables.Item3;
        File.WriteAllText(saveFilePath, JsonConvert.SerializeObject(saveData, formatting));
    }
}
