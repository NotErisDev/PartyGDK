using System;
using UnityEngine;

namespace PartyGDK.Base.UI.Animations
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorHelper : BaseAnimationHelper
    {
        private Animator _animator;

        public Animator Animator => _animator;
        public override bool IsPlaying
        {
            get
            {
                for (int layer = 0; layer < _animator.layerCount; layer++)
                {
                    if (_animator.GetCurrentAnimatorStateInfo(layer).normalizedTime < 1
                        || _animator.IsInTransition(layer))
                        return true;
                }

                return false;
            }
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public bool HasParameter(string paramName)
        {
            foreach (AnimatorControllerParameter param in _animator.parameters)
            {
                if (param.name == paramName)
                    return true;
            }

            return false;
        }

        public override void Show(Action onShow = null)
        {
            if (HasParameter("Show"))
                _animator.SetTrigger("Show");
            else if (HasParameter("Shown"))
                _animator.SetBool("Shown", true);

            base.Show(onShow);
        }

        public override void Hide(Action onHide = null)
        {
            if (HasParameter("Hide"))
                _animator.SetTrigger("Hide");
            else if (HasParameter("Shown"))
                _animator.SetBool("Shown", false);

            if (gameObject.activeSelf)
                base.Hide(onHide);
        }
    }
}