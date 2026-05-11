using System;
using System.Collections;
using UnityEngine;

namespace PartyGDK.Base.UI.Animations
{
    public abstract class BaseAnimationHelper : MonoBehaviour
    {
        public abstract bool IsPlaying { get; }

        public virtual void Show(Action onShow = null)
        {
            if (onShow != null)
                StartCoroutine(InvokeAfterPlaying(onShow));
        }

        public virtual void Hide(Action onHide = null)
        {
            if (onHide != null)
                StartCoroutine(InvokeAfterPlaying(onHide));
        }

        public IEnumerator InvokeAfterPlaying(Action action)
        {
            yield return null;
            yield return new WaitWhile(() => IsPlaying);

            action?.Invoke();
        }
    }
}