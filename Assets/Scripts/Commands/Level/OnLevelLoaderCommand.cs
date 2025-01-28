using UnityEngine;

namespace Commands.Level
{
    public class OnLevelLoaderCommand
    {
        private Transform _levelHolder;

        internal OnLevelLoaderCommand(Transform levelHolder)
        {
            _levelHolder = levelHolder;
        }

        internal void Execute(byte levelIndex)
        {
            Object.Instantiate(Resources.Load<GameObject>($"Resources/Prefabs/LevelPrefabs/level {levelIndex}"), _levelHolder, true);
        }
    }
}