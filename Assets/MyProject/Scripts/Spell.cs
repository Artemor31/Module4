﻿using UnityEngine;

public abstract class Spell : ScriptableObject
{
    public float Damage;
    [field: SerializeField] public bool Completed { get; protected set; }
    public abstract void Cast(Vector3 startPos, Vector3 point);
    public abstract void Process();
}
