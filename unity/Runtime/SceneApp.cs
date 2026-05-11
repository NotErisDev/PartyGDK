using UnityEngine;

namespace PartyGDK.Base
{
    public class SceneApp : MonoBehaviour
    {
        public static SceneApp Instance {  get; private set; }
        public static App App { get; private set; }

        [SerializeField] private App _app;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                App = _app;
            }
        }
    }
}