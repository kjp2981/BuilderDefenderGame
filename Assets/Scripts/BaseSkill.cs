using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class BaseSkill
{
    protected BaseSkillSO _skillSO;
    public BaseSkillSO SkillSO => _skillSO;

    public abstract void Init();

    public abstract IEnumerator SkillActionCoroutine(Vector2 pos);
}
