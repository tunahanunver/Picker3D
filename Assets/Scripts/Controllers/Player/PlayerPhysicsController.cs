using Controllers.Pool;
using DG.Tweening;
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

                DOVirtual.DelayedCall(3, ()=>
                {
                    var result = other.transform.parent.GetComponentInChildren<PoolController>().TakeResults(manager.StageValue);

                    if (result)
                    {
                        CoreGameSignals.Instance.onStageAreaSuccessful?.Invoke(manager.StageValue);
                        InputSignals.Instance.onEnableInput?.Invoke();
                    }
                    else CoreGameSignals.Instance.onLevelFailed?.Invoke();
                });
                return;
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

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            var transform1 = manager.transform;
            var position1 = transform1.position;

            Gizmos.DrawSphere(new Vector3(position1.x, position1.y + 1f, position1.z + 1), 1.35f);    
        }

        public void OnReset()
        {
            //Boş
        }
    }
}