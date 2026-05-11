using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace PartyGDK.Base.Settings
{
    public class RoomSettingsManager : MonoBehaviour, ISetting
    {
        [SerializeField] private TMP_InputField _roomPasswordField;
        [SerializeField] private TMP_InputField _moderationPasswordField;

        [SerializeField] private Toggle _audienceToggle;
        [SerializeField] private Toggle _hideCodeToggle;
        [SerializeField] private Toggle _skipTutorialToggle;
        [SerializeField] private Toggle _obsceneFilterToggle;

        public void Load()
        {
            RoomSettings.Load();

            _roomPasswordField.text = RoomSettings.Password;
            _moderationPasswordField.text = RoomSettings.ModerationPassword;
            _audienceToggle.isOn = RoomSettings.AudienceEnabled;
            _hideCodeToggle.isOn = RoomSettings.HideCode;

            if (_skipTutorialToggle != null)
                _skipTutorialToggle.isOn = RoomSettings.SkipTutorial;
            if (_obsceneFilterToggle != null)
                _obsceneFilterToggle.isOn = RoomSettings.ObsceneFilter;
        }

        public void Save()
        {
            RoomSettings.Password = _roomPasswordField.text;
            RoomSettings.ModerationPassword = _moderationPasswordField.text;
            RoomSettings.AudienceEnabled = _audienceToggle.isOn;
            RoomSettings.HideCode = _hideCodeToggle.isOn;

            if (_skipTutorialToggle != null)
                RoomSettings.SetSkipTutorial(_skipTutorialToggle.isOn);
            if (_obsceneFilterToggle != null)
                RoomSettings.ObsceneFilter = _obsceneFilterToggle.isOn;

            RoomSettings.Save();
        }

        public void Apply() {  }
    }
}