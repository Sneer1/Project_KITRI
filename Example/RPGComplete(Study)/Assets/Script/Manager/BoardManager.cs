using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoSingleton<BoardManager>
{
    Dictionary<BaseObject, List<BaseBoard>> DicBoard = new Dictionary<BaseObject, List<BaseBoard>>();

    GameObject BoardUI = null;

    private void Awake()
    {
        if(BoardUI == null)
        {
            BoardUI = new GameObject("BoardUI_Root");
            BoardUI.layer = LayerMask.NameToLayer("UI");

            Transform parent = UICamera.mainCamera.transform;

            //#1
            //BoardUI.transform.SetParent(parent, false);

            //#2
            BoardUI.transform.parent = parent;
            BoardUI.transform.localPosition = Vector3.zero;
            BoardUI.transform.localScale = Vector3.one;

        }
    }

    private void Update()
    {
        //gameover

        BaseBoard destroyBoard = null;

        foreach(KeyValuePair<BaseObject, List<BaseBoard>> pair in DicBoard)
        {
            List<BaseBoard> listBoard = pair.Value;

            for(int i = 0; i < listBoard.Count; ++i)
            {
                listBoard[i].UpdateBoard();

                if(listBoard[i].CheckDestroyTime() == true)
                {
                    destroyBoard = listBoard[i];
                    listBoard.Remove(destroyBoard);
                    Destroy(destroyBoard.gameObject);
                }
            }
        }
    }

    public BaseBoard AddBoard(BaseObject keyObject, EBoardType boardType)
    {
        List<BaseBoard> listBoard = null;

        if (DicBoard.ContainsKey(keyObject) == false)
        {
            listBoard = new List<BaseBoard>();
            DicBoard.Add(keyObject, listBoard);
        }
        else
            listBoard = DicBoard[keyObject];

        BaseBoard boardData = MakeBoard(boardType);
        boardData.TargetComponent = keyObject;
        listBoard.Add(boardData);
        return boardData;

    }

    private BaseBoard MakeBoard(EBoardType boardType)
    {
        BaseBoard boardData = null;

        switch (boardType)
        {
            case EBoardType.Board_None:
                {
                    Debug.LogError("BoardNone");
                }
                break;
            case EBoardType.Board_HP:
                {
                    GameObject hpBoard = Resources.Load(ConstValue.UI_Path_HP) as GameObject;

                    GameObject UI_HPBoard = NGUITools.AddChild(BoardUI, hpBoard);

                    boardData = UI_HPBoard.GetComponent<HPBoard>();

                }
                break;
            case EBoardType.Board_Damage:
                {
                    GameObject DamageBoard = Resources.Load(ConstValue.UI_Path_DAMAGE) as GameObject;

                    GameObject UI_DamageBoard = NGUITools.AddChild(BoardUI, DamageBoard);

                    boardData = UI_DamageBoard.GetComponent<DamageBoard>();

                }
                break;
        }
        return boardData;
    }

    public BaseBoard GetBoardData(BaseObject keyObject, EBoardType boardType)
    {
        if (DicBoard.ContainsKey(keyObject) == false)
            return null;

        List<BaseBoard> listBoard = DicBoard[keyObject];

        for(int i = 0; i < listBoard.Count; ++i)
        {
            if (listBoard[i].BoardType == boardType)
                return listBoard[i];
        }

        return null;
    }

    public void ShowBoard(BaseObject keyObject, bool bEnable = true)
    {
        if (DicBoard.ContainsKey(keyObject) == false)
            return;

        List<BaseBoard> listBoard = DicBoard[keyObject];
        for(int i = 0; i < listBoard.Count; ++i)
        {
            if(listBoard[i].gameObject.activeSelf != bEnable)
            {
                listBoard[i].gameObject.SetActive(bEnable);
            }
        }
    }

    public void ClearBoard(BaseObject keyObject)
    {
        if (DicBoard.ContainsKey(keyObject) == false)
            return;

        List<BaseBoard> listBoard = DicBoard[keyObject];
        for (int i = 0; i < listBoard.Count; ++i)
        {
            if (listBoard[i] != null)
                Destroy(listBoard[i].gameObject);
        }
        listBoard.Clear();
        DicBoard.Remove(keyObject);
    }
}
