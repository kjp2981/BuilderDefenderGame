using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowSkill : BaseSkill
{
    public override void Init()
    {
        _skillSO = Resources.Load<BaseSkillSO>(typeof(SlowSkill).Name);
    }

    public override IEnumerator SkillActionCoroutine(Vector2 pos)
    {
        GameObject effect = GameObject.Instantiate(_skillSO.EffectPrefab);
        effect.transform.position = pos;
        effect.transform.rotation = Quaternion.identity;

        Collider2D[] col = Physics2D.OverlapCircleAll(effect.transform.position, _skillSO.Range);
        foreach (Collider2D collider in col)
        {
            Enemy enemy = collider.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.moveSpeed /= 2;
            }
        }

        yield return new WaitForSeconds(_skillSO.Duration);

        foreach (Collider2D collider in col)
        {
            Enemy enemy = collider.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.moveSpeed *= 2;
            }
        }
    }
}
