using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PartyGDK.Base
{
    public class RoomSettings
    {
        public const char AppSeparator = ',';
        private static List<string> _skipTutorialApps = new();

        public static bool AudienceEnabled;
        public static bool HideCode;
        public static bool ObsceneFilter;

        public static string Password;
        public static string ModerationPassword;

        public static bool SkipTutorial
        {
            get
            {
                if (SceneApp.App == null)
                {
                    Debug.LogWarning("No SceneApp found.");
                    return false;
                }

                return _skipTutorialApps.Contains(SceneApp.App.Tag);
            }
        }

        public static void Save()
        {
            PlayerPrefs.SetInt("AudienceEnabled", Convert.ToInt32(AudienceEnabled));
            PlayerPrefs.SetInt("HideCode", Convert.ToInt32(HideCode));
            PlayerPrefs.SetInt("ObsceneFilter", Convert.ToInt32(ObsceneFilter));
            PlayerPrefs.SetString("SkipTutorialApps", string.Join(AppSeparator, _skipTutorialApps));
            PlayerPrefs.SetString("Password", Password);
            PlayerPrefs.SetString("ModerationPassword", ModerationPassword);
        }

        public static void Load()
        {
            AudienceEnabled = Convert.ToBoolean(PlayerPrefs.GetInt("AudienceEnabled"));
            HideCode = Convert.ToBoolean(PlayerPrefs.GetInt("HideCode"));
            ObsceneFilter = Convert.ToBoolean(PlayerPrefs.GetInt("ObsceneFilter"));
            _skipTutorialApps = PlayerPrefs.GetString("SkipTutorialApps").Split(AppSeparator).ToList();
            Password = PlayerPrefs.GetString("Password");
            ModerationPassword = PlayerPrefs.GetString("ModerationPassword");
        }

        public static void SetSkipTutorial(bool value)
        {
            string tag = SceneApp.App.Tag;

            if (value && !_skipTutorialApps.Contains(tag))
                _skipTutorialApps.Add(tag);
            else if (!value && _skipTutorialApps.Contains(tag))
                _skipTutorialApps.Remove(tag);
        }
    }
}