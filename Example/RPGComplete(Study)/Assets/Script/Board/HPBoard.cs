using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBoard : BaseBoard
{
    [SerializeField]
    UIProgressBar ProgressBar = null;
    [SerializeField]
    UILabel HPLabel = null;

    public override EBoardType BoardType
    {
        get
        {
            return EBoardType.Board_HP;
        }
    }

    public override void SetData(string strkey, params object[] datas)
    {
        if(strkey == ConstValue.SetData_HP)
        {
            double maxHp = (double)datas[0];
            double curHp = (double)datas[1];

            ProgressBar.value = (float)(curHp / maxHp);
            HPLabel.text = curHp.ToString() + " / " + maxHp.ToString();

        }
    }

}
