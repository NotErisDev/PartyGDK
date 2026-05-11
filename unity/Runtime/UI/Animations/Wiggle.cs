using UnityEngine;

namespace PartyGDK.Base.UI.Animations
{
    public class Wiggle : MonoBehaviour
    {
        [Header("Randomness")]
        public int RandomSeed;
        public bool SetSeedOnAwake;

        [Header("Properties")]
        [Min(0f)] public float PositionAmplitude;
        [Min(0f)] public float RotationAmplitude;
        public float Speed = 1f;

        private Vector3 _positionOffset;
        private Vector3 _rotationOffset;

        private void Awake()
        {
            if (SetSeedOnAwake)
                GenerateRandomSeed();
        }

        [ContextMenu("Generate Random Seed")]
        public void GenerateRandomSeed()
        {
            RandomSeed = Random.Range(-1000, 1000);
        }

        private void Update()
        {
            if (Speed == 0f)
                return;

            float t = Time.time * Speed + RandomSeed;
            if (PositionAmplitude > 0f)
            {
                float offsetX = NegativeOnePerlinNoise(t, 0f);
                float offsetY = NegativeOnePerlinNoise(0f, t);

                Vector3 positionOffset = new Vector2(offsetX, offsetY) * PositionAmplitude;
                transform.localPosition += -_positionOffset + positionOffset;
                _positionOffset = positionOffset;
            }
            if (RotationAmplitude > 0f)
            {
                Vector3 rotationOffset = new Vector3(0f, 0f, NegativeOnePerlinNoise(t, 0f)) * RotationAmplitude;
                transform.localEulerAngles += -_rotationOffset + rotationOffset;

                _rotationOffset = rotationOffset;
            }
        }

        private float NegativeOnePerlinNoise(float x, float y)
        {
            return Mathf.PerlinNoise(x, y) * 2f - 1f;
        }
    }
}