using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBoard : MonoBehaviour
{
    BaseObject TargetActor;

    Camera Cam = null;

    Transform BoardTransform = null;

    bool AttachBoard = true;

    Vector3 Position = Vector3.zero;

    RectTransform rectTransform;

    float DestroyTime = 0.0f;
    protected float CurTime = 0.0f;

    public virtual E_BOARDTYPE BoardType
    {
        get { return E_BOARDTYPE.BOARD_NONE; }
    }

    public BaseObject TargetComponent
    {
        set
        {
            TargetActor = value;
            BoardTransform = TargetActor.FindInChild("HUD_Pos");
        }
    }

    public Camera WORLD_CAM
    {
        get
        {
            if (Cam == null)
                Cam = Camera.main;

            return Cam;
        }
    }

    public RectTransform GetRectTran
    {
        get
        {
            if (rectTransform == null)
                rectTransform = this.GetComponent<RectTransform>();

            return rectTransform;
        }
    }

    public virtual void SetData(string strkey, params object[] datas)
    {

    }

    public virtual void UpdateBoard()
    {
        CurTime += Time.deltaTime;

        if (BoardTransform == null)
        {
            Debug.LogError("Not Found BoardPos in Actor");
            return;
        }

        if (AttachBoard == true)
        {
            Position = BoardTransform.position;
        }
        else
        {
            if (Position == Vector3.zero)
                Position = BoardTransform.position;
        }

        //Vector2 viewPort = WORLD_CAM.WorldToViewportPoint(BoardTransform.position);
        //Vector3 boardPosition = UI_CAM.ViewportToWorldPoint(viewPort);

        Vector2 viewPos = WORLD_CAM.WorldToViewportPoint(BoardTransform.position);

        GetRectTran.anchorMin = viewPos;
        GetRectTran.anchorMax = viewPos;
        GetRectTran.anchoredPosition = BoardTransform.position;

     //   Vector3 viewPos = WORLD_CAM.ScreenToViewportPoint(pos);
     //   viewPos.z = 0;

        //Vector3 uiscreenPos = 

        //Vector3 aa = WORLD_CAM.WorldToScreenPoint(BoardTransform.position);

        //RectTransform recttrans = this.GetComponent<RectTransform>();

        //boardPosition.z = 0f;
        //this.transform.position = boardPosition;
        //recttrans.anchoredPosition = pos;

    }

    public bool CheckDestroyTime()
    {
        if (DestroyTime == 0.0f)
            return false;

        if (DestroyTime <= CurTime)
            return true;

        return false;

    }
}
