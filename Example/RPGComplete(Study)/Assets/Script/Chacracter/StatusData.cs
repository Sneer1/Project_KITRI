using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusData
{
    Dictionary<EStatusData, double> DicData = new Dictionary<EStatusData, double>();

    public void InitData()
    {
        DicData.Clear();
    }

    public void Copy(StatusData data)
    {
        foreach(KeyValuePair<EStatusData, double> pair in data.DicData)
        {
            IncreaseData(pair.Key, pair.Value);
        }
    }

    public void IncreaseData(EStatusData statusData, double valueData)
    {
        double preValue = 0.0;
        DicData.TryGetValue(statusData, out preValue);
        DicData[statusData] = preValue + valueData;
    }

    public void DecreaseData(EStatusData statusData, double valueData)
    {
        double preValue = 0.0;
        DicData.TryGetValue(statusData, out preValue);
        DicData[statusData] = preValue - valueData;
    }

    public void SetData(EStatusData statusData, double valueData)
    {
        DicData[statusData] = valueData;
    }

    public void RemoveData(EStatusData statusData)
    {
        if (DicData.ContainsKey(statusData))
            DicData.Remove(statusData);

    }

    public double GetStatusData(EStatusData statusData)
    {
        double preValue = 0.0;
        DicData.TryGetValue(statusData, out preValue);
        return preValue;
    }
}