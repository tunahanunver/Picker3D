using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Signals;
using Data.ValueObjects;

namespace Controllers.Pool
{
    public class PoolController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        // Animasyonu çalıştırılacak tüm objeleri liste olarak alıyoruz
        [SerializeField] private List<Animation> animatedObjects = new List<Animation>(); 
        [SerializeField] private TextMeshPro poolText;
        [SerializeField] private byte stageID;
        [SerializeField] private new Renderer renderer;

        #endregion

        #region Private Variables

        private PoolData _data;
        private byte _collectedCount;

        private readonly string _collectable = "Collectable";

        #endregion

        #endregion

        private void Awake()
        {
            _data = GetPoolData();
        }

        private PoolData GetPoolData()
        {
            return Resources.Load<CD_Level>("Data/CD_Level").Levels[(int)CoreGameSignals.Instance.onGetLevelValue?.Invoke()].Pools[stageID];
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onStageAreaSuccessful += OnActivateAnimations;
            CoreGameSignals.Instance.onStageAreaSuccessful += OnChangePoolColor;
        }

        private void OnActivateAnimations(byte stageValue)
        {
            if (stageValue != stageID) return;

            // Listedeki tüm objelerin animasyonlarını aynı anda başlat
            PlayAllAnimations();
        }

        private void PlayAllAnimations()
        {
            foreach (var animatedObject in animatedObjects)
            {
                if (animatedObject != null) // Her ihtimale karşı null kontrolü
                {
                    animatedObject.Play(); // Animasyonu başlat
                }
            }
        }

        private void OnChangePoolColor(byte stageValue)
        {
            if (stageValue != stageID) return;
            renderer.material.color = new Color(0.1607842f, 0.6039216f, 0.1766218f);
        }

        private void Start()
        {
            SetRequiredAmountText();
        }

        private void SetRequiredAmountText()
        {
            poolText.text = $"0/{_data.RequiredObjectCount}";
        }

        public bool TakeResults(byte managerStageValue)
        {
            if (stageID == managerStageValue)
            {
                return _collectedCount >= _data.RequiredObjectCount;
            }

            return false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(_collectable)) return;
            IncreaseCollectedAmount();
            SetCollectedAmountToPool();
        }

        private void IncreaseCollectedAmount()
        {
            _collectedCount++;
        }

        private void SetCollectedAmountToPool()
        {
            poolText.text = $"{_collectedCount}/{_data.RequiredObjectCount}";
        }

        private void DecreaseCollectedAmount()
        {
            _collectedCount--;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag(_collectable)) return;
            DecreaseCollectedAmount();
            SetCollectedAmountToPool();
        }
    }
}