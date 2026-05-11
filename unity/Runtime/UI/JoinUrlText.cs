using TMPro;
using UnityEngine;

namespace PartyGDK.Base.UI
{
    [RequireComponent(typeof(TMP_Text))]
    public class JoinUrlText : MonoBehaviour
    {
        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
            _text.text = _text.text.Replace("{url}", ConnectionSettings.JoinUrl);
        }
    }
}