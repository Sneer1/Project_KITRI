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

        else if (strkey == ConstValue.SetData_DamageText)
        {
            DamageText.text = (string)datas[0];

            base.UpdateBoard();
        }
    }

    public override void UpdateBoard()
    {
        CurTime += Time.deltaTime;

        GetRectTran.anchoredPosition += Vector2.up * Time.deltaTime * 30f;
    }

}
