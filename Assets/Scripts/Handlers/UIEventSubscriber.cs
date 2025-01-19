using System;
using Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Handlers
{
    public class UIEventSubscriber : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private UIEventSubscribtionTypes type;
        [SerializeField] private Button button;
            
        #endregion

        #region Private Variables

        private UIManager _manager;
            
        #endregion
            
        #endregion

        private void Awake()
        {
            GetReferences();    
        }

        private void GetReferences()
        {
            _manager = FindObjectOfType<UIManager>();
        }

        private void OnEnable()
        {
            SubscribeEvents();    
        }

        private void SubscribeEvents()
        {
            switch (type)
            {
                case UIEventSubscribtionTypes.OnPlay:
                    button.onClick.AddListener(_manager.Play);
                    break;
                case UIEventSubscribtionTypes.OnNextLevel:
                    button.onClick.AddListener(_manager.NextLevel);
                    break;
                case UIEventSubscribtionTypes.OnRestartLevel:
                    button.onClick.AddListener(_manager.RestartLevel);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void UnSubscribeEvents()
        {
            switch (type)
            {
                case UIEventSubscribtionTypes.OnPlay:
                    button.onClick.RemoveListener(_manager.Play);
                    break;
                case UIEventSubscribtionTypes.OnNextLevel:
                    button.onClick.RemoveListener(_manager.NextLevel);
                    break;
                case UIEventSubscribtionTypes.OnRestartLevel:
                    button.onClick.RemoveListener(_manager.RestartLevel);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnDisable()
        {
            UnSubscribeEvents();    
        }
    }
}