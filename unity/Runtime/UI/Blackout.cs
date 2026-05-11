using System;
using PartyGDK.Base.UI.Animations;
using UnityEngine;

namespace PartyGDK.Base.UI
{
    public class Blackout : MonoBehaviour
    {
        public static Blackout Instance { get; private set; }

        [SerializeField] private bool _hideOnAwake;
        [SerializeField] private bool _disableOnHide = true;

        private BaseAnimationHelper _animationHelper;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;

            _animationHelper = GetComponent<BaseAnimationHelper>();

            if (_hideOnAwake)
                gameObject.SetActive(false);
        }

        public void Show(Action onShow = null)
        {
            gameObject.SetActive(true);
            _animationHelper.Show(onShow);
        }

        public void Hide(Action onHide = null)
        {
            _animationHelper.Hide(() =>
            {
                if (_disableOnHide)
                    gameObject.SetActive(false);

                onHide?.Invoke();
            });
        }
    }
}