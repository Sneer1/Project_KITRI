using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBoard : BaseBoard
{
    [SerializeField]
    Slider ProgressBar = null;
    [SerializeField]
    Text HPText = null;

    public override E_BOARDTYPE BoardType
    {
        get
        {
            return E_BOARDTYPE.BOARD_HP;
        }
    }

    public override void SetData(string strkey, params object[] datas)
    {
        if (strkey == ConstValue.SetData_HP)
        {
            double maxHp = (double)datas[0];
            double curHp = (double)datas[1];

            ProgressBar.value = (float)(curHp / maxHp);
            HPText.text = curHp.ToString() + " / " + maxHp.ToString();
        }
    }
}
