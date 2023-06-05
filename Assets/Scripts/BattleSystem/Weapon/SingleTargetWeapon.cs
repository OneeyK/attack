using Unity.VisualScripting;
using UnityEngine;

namespace BattleSystem.Weapon
{
    public class SingleTargetWeapon : BaseWeapon
    {
        private readonly Attacker _attacker;
        private float _damage;
        public SingleTargetWeapon(Attacker attacker)
        {
            /*attacker.TryGetComponent(out PolygonCollider2D collider)
                Object.Destroy(collider);

                
            attacker.TryGetComponent().
            attacker.AddComponent<PolygonCollider2D>().isTrigger = true;*/
           
            _attacker = attacker;
        }
        public override void Attack(float damage)
        {
            _attacker.Reset();
            _attacker.gameObject.SetActive(true);
            _damage = damage;
        }

        public override void EndAttack()
        {
            if(_attacker.Targets.Count < 1)
                return;
            
            _attacker.Targets[0].TakeDamage(_damage);
        }
    }
}