using System.Linq;
using TMPro;
using UnityEngine;

namespace PartyGDK.Base.UI
{
    [RequireComponent(typeof(TMP_Text))]
    public class InputsToModerateText : MonoBehaviour
    {
        private TMP_Text _text;

        private void OnEnable()
        {
            if (_text == null)
                _text = GetComponent<TMP_Text>();

            RoomEvents.Instance.OnInputModerated.AddListener(OnInputModerated);
            UpdateText();
        }

        private void OnDisable()
        {
            RoomEvents.Instance.OnInputModerated.RemoveListener(OnInputModerated);
        }

        private void UpdateText()
        {
            _text.text = RoomManager.Instance.CurrentRoom.PlayerInputs
                .Count(i => i.Status == PlayerInput.ModerationStatus.Pending && i.CanBeModerated()).ToString();
        }

        private void OnInputModerated(PlayerInput input)
        {
            UpdateText();
        }
    }
}