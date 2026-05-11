using PartyGDK.Base.UI.Animations;
using System.Collections;
using UnityEngine;

namespace PartyGDK.Base.UI
{
    public class RoomDestroyedText : MonoBehaviour
    {
        [Min(0f), SerializeField] private float _duration = 10f;

        private BaseAnimationHelper _animationHelper;

        private void Start()
        {
            if (!TryGetComponent(out _animationHelper))
                Debug.LogError($"{GetType()} must have an animation helper on it!");

            if (RoomManager.RoomDestroyed)
            {
                RoomManager.RoomDestroyed = false;
                StartCoroutine(Show());
            }
            else
                gameObject.SetActive(false);
        }

        private IEnumerator Show()
        {
            _animationHelper.Show();
            yield return new WaitForSeconds(_duration);
            _animationHelper.Hide(() => gameObject.SetActive(false));
        }
    }
}