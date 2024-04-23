using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Progression", menuName = "Configs/Progression", order = 0)]
public class Progression : ScriptableObject
{
    public List<LevelData> Levels;

    public int ExpFor(int level) => Levels.First(d => d.Level == level).NextLevelExp;
    public float HealthFor(int level) => Levels.First(d => d.Level == level).HealthBonus;
}

[Serializable]
public class LevelData
{
    public int Level;
    public int NextLevelExp;
    public int HealthBonus;
}