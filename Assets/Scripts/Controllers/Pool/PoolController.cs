using System;
using System.Collections.Generic;
using Data.ValueObjects;
using DG.Tweening;
using Signals;
using TMPro;
using UnityEngine;

namespace Controllers.Pool
{
    public class PoolController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private List<DOTweenAnimation> tweens = new List<DOTweenAnimation>();
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
            CoreGameSignals.Instance.onStageAreaSuccessful += OnActivateTweens;
            CoreGameSignals.Instance.onStageAreaSuccessful += OnChangePoolColor;
        }

        private void OnActivateTweens(byte stageValue)
        {
            if (stageValue != stageID) return;
            foreach (var tween in tweens)
            {
                tween.DoPlay();
            }
        }

        private void OnChangePoolColor(byte stageValue)
        {
            if (stageValue != stageID) return;
            renderer.material.DOColor(new Color(0.1607842f, 0.6039216f, 01766218f), 1).SetEase(Ease.Linear);
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

    internal class DOTweenAnimation
    {
        internal void DoPlay()
        {
            //Animasyon
        }
    }
}