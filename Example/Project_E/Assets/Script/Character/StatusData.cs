using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusData
{
    Dictionary<E_STATUSDATA, double> DicData = new Dictionary<E_STATUSDATA, double>();

    public void InitData()
    {
        DicData.Clear();
    }

    public void Copy(StatusData data)
    {
        foreach(KeyValuePair<E_STATUSDATA, double> pair in data.DicData)
        {
            IncreaseData(pair.Key, pair.Value);
        }
    }

    public void IncreaseData(E_STATUSDATA statusData, double valueData)
    {
        double preValue = 0.0;
        DicData.TryGetValue(statusData, out preValue);
        DicData[statusData] = preValue + valueData;
    }

    public void DecreaseData(E_STATUSDATA statusData, double valueData)
    {
        double preValue = 0.0;
        DicData.TryGetValue(statusData, out preValue);
        DicData[statusData] = preValue - valueData;
    }

    public void SetData(E_STATUSDATA statusData, double valueData)
    {
        DicData[statusData] = valueData;
    }

    public void RemoveData(E_STATUSDATA statusData)
    {
        if (DicData.ContainsKey(statusData))
            DicData.Remove(statusData);

    }

    public double GetStatusData(E_STATUSDATA statusData)
    {
        double preValue = 0.0;
        DicData.TryGetValue(statusData, out preValue);
        return preValue;
    }
}