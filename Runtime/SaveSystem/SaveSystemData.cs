using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using UnityEngine.SceneManagement;

namespace Cradaptive.SaveSystem
{
    [CreateAssetMenu(fileName = "SaveSystemData", menuName = "Create/SaveSystemData")]
    public class SaveSystemData : ScriptableObject, ISaveSystem<Dictionary<string, string>>
    {
        public string saveKey => "com.cradaptive.savesystemkey";
        public Dictionary<string, string> cache { get; set; }
        Dictionary<string,ISaveComponent> componentsWithSaveSupport = new Dictionary<string,ISaveComponent>();
        
        public void LoadSavedData()
        {
            this.LoadData();
            GameObject[] allObjects = SceneManager.GetActiveScene().GetRootGameObjects();
            foreach (GameObject obj in allObjects)
            {
                ISaveComponent[] loadData = obj.GetComponentsInChildren<ISaveComponent>();
                foreach (ISaveComponent data in loadData)
                {
                    if (!componentsWithSaveSupport.ContainsKey(data.SaveData.saveKey))
                        componentsWithSaveSupport.Add(data.SaveData.saveKey,data);

                    if (GetSavedData(data.SaveData.saveKey, out string json))
                    {
                        if (data is ILoadData load)
                            load.OnLoadedData(json);
                        //  Debug.LogError($"key {data.saveKey} and value {json}");
                    }
                }
            }
        }

        public bool GetSavedData(string key, out string data)
        {
            data = "";
            if (cache.ContainsKey(key))
            {
                data = cache[key];
            }

            return !string.IsNullOrEmpty(data);
        }

        public void SaveData(ISaveData saveData)
        {
            SaveData(saveData.saveKey, saveData.dataToSave);
        }

        private void SaveData(string key, string value)
        {
            cache[key] = value;
            this.SaveData();
        }
        
        public void ResetData()
        {
            cache = new Dictionary<string, string>();
        }
    }
}