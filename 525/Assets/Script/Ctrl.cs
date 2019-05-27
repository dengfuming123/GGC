using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl : MonoBehaviour {

    [HideInInspector]
    public Model model;
    [HideInInspector]
    public View view;



    private void Awake()
    {
        model = GameObject.FindGameObjectWithTag("Model").GetComponent<Model>();
        view = GameObject.FindGameObjectWithTag("View").GetComponent<View>();
    }

    private void Update()
    {
        SkillStart();
    }

    void SkillStart()
    {
        if (model.skillNum == 1)
        {
            view.skill_0.SetActive(true);
            //释放一技能
            Invoke("SkillEnd", 2);//技能效果将在2秒后结束
        }
        if (model.skillNum == 2)
        {
            view.skill_1.SetActive(true);
            Invoke("SkillEnd", 2);
        }
        if (model.skillNum == 3)
        {
            view.skill_2.SetActive(true);
            Invoke("SkillEnd", 2);
        }
        if (model.skillNum == 4)
        {
            view.skill_3.SetActive(true);
            Invoke("SkillEnd", 2);
        }
    }

    void SkillEnd()
    {
        if (view.skill_0)
        {
            view.skill_0.SetActive(false);
            //一技能效果结束
            model.skillNum = 0;
        }
        if (view.skill_1)
        {
            view.skill_1.SetActive(false);
            model.skillNum = 0;
        }
        if (view.skill_2)
        {
            view.skill_2.SetActive(false);
            model.skillNum = 0;
        }
        if (view.skill_3)
        {
            view.skill_3.SetActive(false);
            model.skillNum = 0;
        }
    }
}

