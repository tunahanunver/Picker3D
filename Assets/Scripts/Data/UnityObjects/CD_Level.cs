using System.Collections.Generic;
using UnityEngine;

namespace Data.ValueObjects
{
    [CreateAssetMenu(fileName = "CD_Level", menuName = "Picker3D/CD_Level", order = 0)]
    public class CD_Level : ScriptableObject
    {
        public List<LevelData> Levels;
    }
}