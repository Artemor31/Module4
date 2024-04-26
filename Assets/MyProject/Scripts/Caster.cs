using UnityEngine;

public class Caster : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Spell _spell;

    internal void CastSpell(Vector3 point)
    {
        _animator.SetTrigger("Casted");
        _spell.Cast(transform.position + Vector3.up, point);
    }

    private void Update()
    {
        if (_spell.Completed == false) 
        {
            _spell.Process();
        }
    }
}
