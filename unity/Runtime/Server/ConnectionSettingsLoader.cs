using UnityEngine;

namespace PartyGDK.Base.Server
{
    public class ConnectionSettingsLoader : MonoBehaviour
    {
        private void Awake()
        {
            ConnectionSettings.LoadSettings();
        }
    }
}