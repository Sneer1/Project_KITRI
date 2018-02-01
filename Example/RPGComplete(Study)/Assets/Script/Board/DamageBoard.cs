using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBoard : BaseBoard
{
    [SerializeField]
    UILabel DamageLabel = null;

    public override EBoardType BoardType
    {
        get
        {
            return EBoardType.Board_Damage;
        }
    }

    public override void SetData(string strkey, params object[] datas)
    {
        if(strkey == ConstValue.SetData_Damage)
        {
            double damage = (double)datas[0];
            DamageLabel.text = damage.ToString();

            base.UpdateBoard(); //위치값 초기화
        }
    }

    public override void UpdateBoard()
    {
        CurTime += Time.deltaTime;
        transform.position += Vector3.up * Time.deltaTime * 0.5f;
    }
}
