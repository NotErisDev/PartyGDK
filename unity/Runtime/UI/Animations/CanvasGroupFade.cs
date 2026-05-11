using System;
using UnityEngine;

namespace PartyGDK.Base.UI.Animations
{
    [RequireComponent(typeof(CanvasGroup))]
    public class CanvasGroupFade : BaseAnimationHelper
    {
        [Min(.001f)]
        [SerializeField] private float _duration = .5f;
        [SerializeField] private bool _showOnAwake;

        private float _lerpTime = 1f;
        private float _targetOpacity;

        private CanvasGroup _canvasGroup;

        public override bool IsPlaying => _lerpTime < 1f;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            if (_showOnAwake)
                Show();
        }

        private void FixedUpdate()
        {
            if (_lerpTime >= 1f)
                return;

            _lerpTime += Time.fixedDeltaTime / _duration;
            _canvasGroup.alpha = Mathf.Lerp(_canvasGroup.alpha, _targetOpacity, _lerpTime);
        }

        public void FadeIn(float opacity = 1f)
        {
            _lerpTime = 0f;
            _targetOpacity = opacity;
        }

        public void FadeOut()
        {
            _lerpTime = 0f;
            _targetOpacity = 0f;
        }

        public void ForceShow(float opacity = 1f)
        {
            _lerpTime = 1f;
            _canvasGroup.alpha = opacity;
        }

        public void ForceHide()
        {
            _lerpTime = 1f;
            _canvasGroup.alpha = 0f;
        }

        public override void Show(Action onShow = null)
        {
            _canvasGroup.alpha = 0f;
            FadeIn();
            base.Show(onShow);
        }

        public override void Hide(Action onHide = null)
        {
            FadeOut();
            base.Hide(onHide);
        }
    }
}