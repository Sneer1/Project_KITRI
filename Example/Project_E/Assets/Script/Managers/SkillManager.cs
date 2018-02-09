using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class SkillManager : MonoSingleton<SkillManager>
{
    Dictionary<BaseObject, List<BaseSkill>> DicUseSkill = new Dictionary<BaseObject, List<BaseSkill>>();

    Dictionary<string, SkillData> DicSkillData = new Dictionary<string, SkillData>();

    Dictionary<string, SkillTemplate> DicSkillTemplate = new Dictionary<string, SkillTemplate>();

    Dictionary<E_SKILLMODETYPE, GameObject> DicModel = new Dictionary<E_SKILLMODETYPE, GameObject>();

    private void Awake()
    {
        LoadSkillData(ConstValue.SkillDataPath);
        LoadSkillTemplate(ConstValue.SkillTemplatePath);
        LoadSkillModel();
    }

    private void LoadSkillData(string skillDataPath)
    {
        TextAsset skillAssetData = Resources.Load(skillDataPath) as TextAsset;

        if (skillAssetData == null)
        {
            Debug.LogError("스킬 데이터 로드 실패");
            return;
        }
        JSONNode rootNode = JSON.Parse(skillAssetData.text);
        if (rootNode == null)
            return;

        JSONObject skillDataNode = rootNode[ConstValue.SkillDataKey] as JSONObject;

        foreach (KeyValuePair<string, JSONNode> pair in skillDataNode)
        {
            SkillData skillData = new SkillData(pair.Key, pair.Value);
            DicSkillData.Add(pair.Key, skillData);
        }



    }

    private void LoadSkillTemplate(string skillTemplatePath)
    {
        TextAsset skillAssetTemplate = Resources.Load(skillTemplatePath) as TextAsset;

        if (skillAssetTemplate == null)
        {
            Debug.LogError("스킬 템플릿 로드 실패");
            return;
        }
        JSONNode rootNode = JSON.Parse(skillAssetTemplate.text);
        if (rootNode == null)
            return;

        JSONObject skillTemplateNode = rootNode[ConstValue.SkillTemplateKey] as JSONObject;

        foreach (KeyValuePair<string, JSONNode> pair in skillTemplateNode)
        {
            SkillTemplate skillTemplate = new SkillTemplate(pair.Key, pair.Value);
            DicSkillTemplate.Add(pair.Key, skillTemplate);
        }
    }

    public SkillData GetSkillData(string strKey)
    {
        SkillData skillData = null;
        DicSkillData.TryGetValue(strKey, out skillData);
        return skillData;

    }

    public SkillTemplate GetSkillTemplate(string strKey)
    {
        SkillTemplate skillTemplate = null;
        DicSkillTemplate.TryGetValue(strKey, out skillTemplate);
        return skillTemplate;

    }

    public void RunSkill(BaseObject keyObject, string strSkillTemplateKey)
    {
        SkillTemplate template = GetSkillTemplate(strSkillTemplateKey);

        if (template == null)
        {
            Debug.LogError("Not Founded SkillTemplate");
            return;
        }

        BaseSkill runSkill = CreateSkill(keyObject, template);

        RunSkill(keyObject, runSkill);
    }

    public void RunSkill(BaseObject keyObject, BaseSkill runSkill)
    {
        List<BaseSkill> listSkill = null;
        if (DicUseSkill.ContainsKey(keyObject) == false)
        {
            listSkill = new List<BaseSkill>();
            DicUseSkill.Add(keyObject, listSkill);
        }
        else
        {
            listSkill = DicUseSkill[keyObject];
        }
        listSkill.Add(runSkill);
    }

    BaseSkill CreateSkill(BaseObject owner, SkillTemplate skillTemplate)
    {
        BaseSkill makeSkill = null;
        GameObject skillObject = new GameObject();

        Transform parentTransform = null;
        switch (skillTemplate.SkillType)
        {
            case E_SKILLTEMPLATETYPE.TARGET_ATTACK:
                makeSkill = skillObject.AddComponent<MeleeSkill>();
                parentTransform = owner.SelfTransform;
                break;
            case E_SKILLTEMPLATETYPE.RANGE_ATTACK:
                makeSkill = skillObject.AddComponent<RangeSkill>();

                parentTransform = owner.FindInChild("FirePos");

                if(parentTransform == null)
                {
                    parentTransform = owner.SelfTransform;
                }

                makeSkill.ThrowEvent(ConstValue.EventKey_SelectModel, GetModel(E_SKILLMODETYPE.BOX));
                break;

            case E_SKILLTEMPLATETYPE.STUN_CROWDCONTROL:

                break;
            case E_SKILLTEMPLATETYPE.GRAVITY_CROWDCONTROL:

                break;
            case E_SKILLTEMPLATETYPE.DODGE_BUFF:

                break;
        }
        skillObject.name = skillTemplate.SkillType.ToString();
        if (makeSkill != null)
        {
            makeSkill.transform.position = parentTransform.position;
            makeSkill.transform.rotation = parentTransform.rotation;

            makeSkill.Owner = owner;
            makeSkill.Template = skillTemplate;
            makeSkill.Target = owner.GetData(ConstValue.ActorData_GetTarget) as BaseObject;

            makeSkill.InitSkill();
        }

        switch (skillTemplate.RangeType)
        {
            case E_SKILLRANGETYPE.RANGE_BOX:
                {
                    BoxCollider collider = skillObject.AddComponent<BoxCollider>();
                    collider.size = new Vector3(skillTemplate.RangeData_1, 1, skillTemplate.RangeData_2);

                    collider.center = new Vector3(0, 0, skillTemplate.RangeData_2 * 0.5f);

                    collider.isTrigger = true;
                }
                break;
            case E_SKILLRANGETYPE.RANGE_SPHERE:
                {
                    SphereCollider collider = skillObject.AddComponent<SphereCollider>();
                    collider.radius = skillTemplate.RangeData_1;
                    collider.isTrigger = true;
                }
                break;
        }

        return makeSkill;
    }

    private void Update()
    {
        foreach (KeyValuePair<BaseObject, List<BaseSkill>> pair in DicUseSkill)
        {
            List<BaseSkill> list = pair.Value;

            for (int i = 0; i < list.Count; ++i)
            {
                BaseSkill updateSkill = list[i];
                updateSkill.UpdateSkill();
                if (updateSkill.End)
                {
                    list.Remove(updateSkill);
                    Destroy(updateSkill.gameObject);
                }
            }
        }
    }

    public void ClearSkill()
    {
        foreach (KeyValuePair<BaseObject, List<BaseSkill>> pair in DicUseSkill)
        {
            List<BaseSkill> list = pair.Value;

            for (int i = 0; i < list.Count; ++i)
            {
                BaseSkill updateSkill = list[i];
                list.Remove(updateSkill);
                Destroy(updateSkill.gameObject);

            }
        }
        DicUseSkill.Clear();
    }

    public void LoadSkillModel()
    {
        for(int i = 0; i < (int)E_SKILLMODETYPE.MAX; ++i)
        {
            GameObject go = Resources.Load("Prefabs/Skill_Models/" + ((E_SKILLMODETYPE)i).ToString()) as GameObject;
            if(go == null)
            {
                Debug.LogError("프리팹 스킬모델 로드 실패");
                continue;

            }
            DicModel.Add((E_SKILLMODETYPE)i, go);
        }
    }

    public GameObject GetModel(E_SKILLMODETYPE type)
    {
        if (DicModel.ContainsKey(type))
        {
            return DicModel[type];
        }
        else
        {
            Debug.LogError("모델 로드 실패");
            return null;
        }
    }

}