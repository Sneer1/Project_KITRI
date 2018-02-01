using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBoard : MonoBehaviour
{
    BaseObject TargetActor;
    Camera UICam = null;
    Camera WorldCam = null;

    Transform BoardTransform = null;

    [SerializeField]
    bool AttachBoard = true;
    Vector3 Position = Vector3.zero;

    [SerializeField]
    float DestroyTime = 0.0f;
    protected float CurTime = 0.0f;

    public virtual EBoardType BoardType
    {
        get { return EBoardType.Board_None; }
    }

    public BaseObject TargetComponent
    {
        set
        {
            TargetActor = value;
            BoardTransform = TargetActor.FindInChild("BoardPos");
        }
    }

    public Camera UI_CAM
    {
        get
        {
            if (UICam == null)
            {
                UICam = NGUITools.FindCameraForLayer(LayerMask.NameToLayer("UI"));
            }
            return UICam;
        }
    }

    public Camera WORLD_CAM
    {
        get
        {
            if (WorldCam == null)
                WorldCam = Camera.main;

            return WorldCam;
        }
    }

    public virtual void SetData(string strkey, params object[] datas)
    {

    }

    public virtual void UpdateBoard()
    {
        CurTime += Time.deltaTime;

        if (UI_CAM == null || WORLD_CAM == null)
        {
            Debug.LogError("Not Found Camera");
            return;
        }

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

        Vector3 viewPort = WORLD_CAM.WorldToViewportPoint(Position);
        Vector3 boardPosition = UI_CAM.ViewportToWorldPoint(viewPort);

        boardPosition.z = 0f;
        this.transform.position = boardPosition;
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
