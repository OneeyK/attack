using System.Collections.Generic;
using UnityEngine;

namespace Core.StatSystem
{
    [CreateAssetMenu(fileName = "StatsStorage", menuName = "Stats/StatsStorage")]
    public class StatsStorage : ScriptableObject
    {
        [field: SerializeField] public List<Stat> Stats { get; private set; }
    }
}