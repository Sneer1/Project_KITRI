using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageBoard : BaseBoard
{
    [SerializeField]
    Text DamageText = null;

    public override E_BOARDTYPE BoardType
    {
        get
        {
            return E_BOARDTYPE.BOARD_DAMAGE;
        }
    }

    public override void SetData(string strkey, params object[] datas)
    {
        if (strkey == ConstValue.SetData_Damage)
        {
            double damage = (double)datas[0];
            DamageText.text = damage.ToString();

            base.UpdateBoard(); //위치값 초기화
        }
    }

    public override void UpdateBoard()
    {
        CurTime += Time.deltaTime;
        transform.position += Vector3.up * Time.deltaTime * 0.5f;
    }

}
