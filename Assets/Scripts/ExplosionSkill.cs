using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSkill : BaseSkill
{
    public override void Init()
    {
        _skillSO = Resources.Load<BaseSkillSO>(typeof(ExplosionSkill).Name);
    }

    public override IEnumerator SkillActionCoroutine(Vector2 pos)
    {
        if (ResourceManager.Instance.CanAfford(_skillSO.CostArray))
        {
            ResourceManager.Instance.SpendResources(_skillSO.CostArray);

            GameObject effect = GameObject.Instantiate(_skillSO.EffectPrefab);
            effect.transform.position = pos;
            effect.transform.localScale = Vector3.one * _skillSO.Range * _skillSO.RangeOffset * 2;
            effect.transform.rotation = Quaternion.identity;

            yield return new WaitForSeconds(_skillSO.Delay);

            Collider2D[] col = Physics2D.OverlapCircleAll(effect.transform.position, _skillSO.Range);
            foreach (Collider2D collider in col)
            {
                Enemy enemy = collider.GetComponent<Enemy>();

                if (enemy != null)
                {
                    int damageAmount = 9999;
                    enemy.GetComponent<HealthSystem>().Damage(damageAmount);

                }
            }
        }
    }
}
