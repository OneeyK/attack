using Core.StatSystem.Enums;
using UnityEngine;

namespace Core.StatSystem
{
    public interface IStatValueGiver
    {
        float GetStatValue(StatType statType);
    }
}