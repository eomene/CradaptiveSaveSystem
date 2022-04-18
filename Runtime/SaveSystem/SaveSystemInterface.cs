using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cradaptive.SaveSystem
{
    public interface ISaveSystem<T>
    {
        string saveKey { get; }
        T cache { get; set; }
        void ResetData();
    }

    public interface ISaveData
    {
        string dataToSave { get; }
        string saveKey { get; }
    }
    
    public interface ISaveComponent
    {
        ISaveData SaveData { get; }
    }

    public interface ILoadData
    {
        void OnLoadedData(string data);
    }

    public interface ISaveControl
    {
        bool allowSave { get; }
    }
}