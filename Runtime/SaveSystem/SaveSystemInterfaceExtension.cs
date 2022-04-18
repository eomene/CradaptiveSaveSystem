using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cradaptive.SaveSystem
{
    public static partial class SaveSystemInterfaceExtension
    {
        public static void SaveData<T>(this ISaveSystem<T> saveSystem)
        {
            if (saveSystem.cache == null) return;
            string dataToSave = JsonConvert.SerializeObject(saveSystem.cache);
            Debug.LogError($"Saving data {saveSystem.saveKey} with key {dataToSave}");
            PlayerPrefs.SetString(saveSystem.saveKey, dataToSave);
        }


        public static void LoadData<T>(this ISaveSystem<T> saveSystem)
        {
            saveSystem.ResetData();
            string json = PlayerPrefs.GetString(saveSystem.saveKey);
            Debug.LogError($"Loading data {saveSystem.saveKey} with key {json}");
            if (!string.IsNullOrEmpty(json))
            {
                var data = JsonConvert.DeserializeObject<T>(json);
                if (data != null)
                {
                    saveSystem.cache = data;
                }

                if (saveSystem.cache == null)
                {
                    saveSystem.ResetData();
                }
            }
            else
            {
                saveSystem.ResetData();
            }
        }

    }
}





