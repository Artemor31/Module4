using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Progression", menuName = "Configs/Progression")]
public class Progression : ScriptableObject
{
    public List<LevelData> LevelDatas;

    internal int ExpForLevel(int level, int exp)
    {
        var levelData = LevelDatas.First(l => l.Level == level);

        return levelData.NextLevel - exp;
    }

    internal float HealthFor(int level)
    {
        var levelData = LevelDatas.First(l => l.Level == level);
        return levelData.Health;
    }

    internal int NeedExpFor(int level)
    {
        var levelData = LevelDatas.First(l => l.Level == level);
        return levelData.NextLevel;
    }
}

[Serializable]
public class LevelData
{
    public int Level;
    public int NextLevel;
    public float Health;
}