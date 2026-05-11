using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace PartyGDK.Base.UI.Animations
{
    [Serializable]
    public class NamedTimeline
    {
        public string Name;
        public TimelineAsset Asset;
    }

    [RequireComponent(typeof(PlayableDirector))]
    public class TimelineHelper : BaseAnimationHelper
    {
        private PlayableDirector _director;
        [SerializeField] private NamedTimeline[] _timelines;

        public PlayableDirector Director => _director;
        public override bool IsPlaying => _director.state == PlayState.Playing;

        private void Awake()
        {
            _director = GetComponent<PlayableDirector>();
            GoToFirstFrame();
        }

        public void GoToFirstFrame() => StartCoroutine(ToFirstFrame());

        public void Play(Action onFinishPlaying = null)
        {
            _director.Play();

            if (onFinishPlaying != null)
                StartCoroutine(InvokeAfterPlaying(onFinishPlaying));
        }

        public void PlayTimeline(string name, Action onFinishPlaying = null)
        {
            NamedTimeline timeline = GetTimeline(name) ?? throw new ArgumentException("Timeline not found");

            _director.playableAsset = timeline.Asset;
            _director.Play();

            if (onFinishPlaying != null)
                StartCoroutine(InvokeAfterPlaying(onFinishPlaying));
        }

        public NamedTimeline GetTimeline(string name)
        {
            return _timelines.FirstOrDefault(t => t.Name == name);
        }

        public override void Show(Action onShow = null)
        {
            PlayTimeline("Show");
            base.Show(onShow);
        }

        public override void Hide(Action onHide = null)
        {
            PlayTimeline("Hide");
            base.Hide(onHide);
        }

        private IEnumerator ToFirstFrame()
        {
            _director.time = 0f;
            _director.Play();
            yield return null;
            _director.Pause();
        }
    }
}