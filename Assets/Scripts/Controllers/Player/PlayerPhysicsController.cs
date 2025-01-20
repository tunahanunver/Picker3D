using Signals;
using UnityEngine;

namespace Controllers.Player
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerManager manager;
        [SerializeField] private new Collider collider;
        [SerializeField] private new Rigidbody rigidbody;
            
        #endregion

        #region Private Variables

        private readonly string _stageArea = "StageArea";
        private readonly string _finish = "FinishArea";
        private readonly string _miniGame = "MiniGameArea";
            
        #endregion
            
        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_stageArea))
            {
                manager.ForceCommand.Execute();
                CoreGameSignals.Instance.onStageAreaEntered?.Invoke();
                InputSignals.Instance.onEnableInput?.Invoke();

                //Stage Area Kontrol
            }
            if (other.CompareTag(_finish))
            {
                CoreGameSignals.Instance.onFinishAreaEntered?.Invoke();
                CoreGameSignals.Instance.onLevelSuccessful?.Invoke();
                InputSignals.Instance.onDisableInput?.Invoke();
                return;
            }

            if (other.CompareTag(_miniGame))
            {
                //MiniGame Mekanikleri
            }
        }

        public void OnReset()
        {
            //Boş
        }
    }
}