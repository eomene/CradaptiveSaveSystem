using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

namespace Cradaptive.SaveSystem
{
    public class SaveSystemManager : MonoBehaviour
    {
        public SaveSystemData saveSystemData;
        void Awake()
        {
            saveSystemData?.LoadSavedData();
        }

    }
}
