using UnityEngine;

public class Caster : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void CastSpell(Health health)
    {
        _animator.SetTrigger("Casted");
    }
}

public class Spell
{

}
