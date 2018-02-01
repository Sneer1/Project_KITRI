using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObject : MonoBehaviour
{
    Dictionary<string, UnityEngine.Component> DicComponent = new Dictionary<string, Component>();

    BaseObject _TargetComponent = null;
    public BaseObject TargetComponent
    {
        get { return _TargetComponent; }
        set { _TargetComponent = value; }
    }

    EBaseObjectState _ObjectState = EBaseObjectState.ObjectState_Normal;
    public EBaseObjectState ObjectState
    {
        get
        {
            if (TargetComponent != null)
                return TargetComponent.ObjectState;
            else
                return this._ObjectState;
        }

        set
        {
            if (TargetComponent != null)
                TargetComponent.ObjectState = value;
            else
                this._ObjectState = value;
        }
    }

    public GameObject SelfObject
    {
        get
        {
            if (TargetComponent != null)
                return TargetComponent.SelfObject;
            else
                return this.gameObject;
        }
    }

    public Transform SelfTransform
    {
        get
        {
            if (TargetComponent != null)
                return TargetComponent.SelfTransform;
            else
                return this.transform;
        }
    }

    public virtual object GetData(string keyData, params object[] datas)
    {
        return null;   
    }

    public virtual void ThrowEvent(string keyData, params object[] datas)
    {
    }

    // transform.GetChild(int)
    // transform.Find(string) // path
    public Transform FindInChild(string strName) // name
    {
        return _FindInChild(strName, SelfTransform);
    }
    
    public Transform _FindInChild(string strName, Transform trans)
    {
        if (trans.name == strName)
            return trans;

        // 자식 전체 순환
        for(int i  = 0; i < trans.childCount; i++)
        {
            Transform returntrans = _FindInChild(strName, trans.GetChild(i));
            if (returntrans != null)
                return returntrans;
        }
        return null;
    }

    public T SelfComponent<T>() where T : UnityEngine.Component
    {
        string objectName = string.Empty;
        string typeName = typeof(T).ToString();
        T tempComponent = default(T);

        if(TargetComponent != null)
        {
            objectName = TargetComponent.SelfObject.name;
            tempComponent = TargetComponent.SelfComponent<T>();
        }
        else
        {
            objectName = this.gameObject.name;
            if(DicComponent.ContainsKey(typeName) == true)
            {
                tempComponent = DicComponent[typeName] as T;
            }
            else
            {
                // 현재 컴포넌트를 한번도 호출한적이 없다.
                tempComponent = this.GetComponent<T>();
                if(tempComponent == null)
                {
                    Debug.LogError("Objectname : " + objectName + ", Missing Component : " + typeName);
                    tempComponent = this.gameObject.AddComponent<T>();
                }
                else
                {
                    DicComponent.Add(typeName, tempComponent);
                }
            }
        }
        return tempComponent;
    }
}