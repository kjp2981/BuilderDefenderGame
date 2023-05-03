using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillGhost : MonoBehaviour
{
    private GameObject spriteGameObject;

    private void Awake()
    {
        spriteGameObject = transform.Find("sprite").gameObject;
    }

    private void Start()
    {
        Hide();
        SkillManager.Instance.onSkillChanged += SkillManager_onSkillChanged;
    }

    private void Update()
    {
        transform.position = UtilClass.GetMouseWorldPosition();
    }

    private void SkillManager_onSkillChanged(object sender, SkillManager.onSkillEventArgs e)
    {
        if (e.skill == null)
        {
            Hide();
        }
        else
        {
            Show(e.skill);
        }
    }

    private void Hide()
    {
        spriteGameObject.SetActive(false);
    }

    private void Show(BaseSkill skill)
    {
        spriteGameObject.SetActive(true);
        spriteGameObject.transform.localScale = Vector3.one * skill.SkillSO.Range * 2;
        //spriteGameObject.GetComponent<SpriteRenderer>().sprite = ghostSprite;
    }

}
