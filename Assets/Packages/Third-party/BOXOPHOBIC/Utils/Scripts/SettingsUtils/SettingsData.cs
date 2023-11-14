// Cristian Pop - https://boxophobic.com/

using UnityEngine;

namespace Boxophobic.Utils
{
    [CreateAssetMenu(fileName = "Data", menuName = "BOXOPHOBIC/Settings Data",order = 51)]
    public class SettingsData : ScriptableObject
    {
        [Space]
        public string data = "";
    }
}