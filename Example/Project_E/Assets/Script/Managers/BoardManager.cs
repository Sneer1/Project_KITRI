using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoSingleton<BoardManager>
{
    Dictionary<BaseObject, List<BaseBoard>> DicBoard = new Dictionary<BaseObject, List<BaseBoard>>();

    GameObject BoardUI = null;

    private void Awake()
    {
        if (BoardUI == null)
        {
            BoardUI = GameObject.Find("BoardUI_Root");
            //BoardUI.layer = LayerMask.NameToLayer("UI");

            //Transform parent = GameObject.Find("Canvas").GetComponent<Canvas>().transform;

            

            //#1
            //BoardUI.transform.SetParent(parent, false);

            //#2
            //BoardUI.transform.parent = parent;
            //BoardUI.transform.localPosition = Vector3.zero;
            //BoardUI.transform.localScale = Vector3.one;

        }
    }

    private void Update()
    {
        //gameover

        BaseBoard destroyBoard = null;

        foreach (KeyValuePair<BaseObject, List<BaseBoard>> pair in DicBoard)
        {
            List<BaseBoard> listBoard = pair.Value;

            for (int i = 0; i < listBoard.Count; ++i)
            {
                listBoard[i].UpdateBoard();

                if (listBoard[i].CheckDestroyTime() == true)
                {
                    destroyBoard = listBoard[i];
                    listBoard.Remove(destroyBoard);
                    Destroy(destroyBoard.gameObject);
                }
            }
        }
    }

    public BaseBoard AddBoard(BaseObject keyObject, E_BOARDTYPE boardType)
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

    private BaseBoard MakeBoard(E_BOARDTYPE boardType)
    {
        BaseBoard boardData = null;

        switch (boardType)
        {
            case E_BOARDTYPE.BOARD_NONE:
                {
                    Debug.LogError("BoardNone");
                }
                break;
            case E_BOARDTYPE.BOARD_HP:
                {
                    GameObject hpBoard = Resources.Load(ConstValue.UI_PATH_HP) as GameObject;

                    //GameObject UI_HPBoard = NGUITools.AddChild(BoardUI, hpBoard);

                    GameObject UI_HPBoard = Instantiate(hpBoard, BoardUI.transform.position, Quaternion.identity) as GameObject;

                    UI_HPBoard.transform.SetParent(BoardUI.transform, false);
                    //UI_HPBoard.transform.parent = BoardUI.transform;

                    boardData = UI_HPBoard.GetComponent<HPBoard>();

                }
                break;
            case E_BOARDTYPE.BOARD_DAMAGE:
                {
                    GameObject DamageBoard = Resources.Load(ConstValue.UI_PATH_DAMAGE) as GameObject;

                    //GameObject UI_DamageBoard = NGUITools.AddChild(BoardUI, DamageBoard);

                    GameObject UI_DamageBoard = Instantiate(DamageBoard, BoardUI.transform.position, Quaternion.identity) as GameObject;

                    UI_DamageBoard.transform.SetParent(BoardUI.transform, false);
                    //UI_DamageBoard.transform.parent = BoardUI.transform;


                    boardData = UI_DamageBoard.GetComponent<DamageBoard>();

                }
                break;
        }
        return boardData;
    }

    public BaseBoard GetBoardData(BaseObject keyObject, E_BOARDTYPE boardType)
    {
        if (DicBoard.ContainsKey(keyObject) == false)
            return null;

        List<BaseBoard> listBoard = DicBoard[keyObject];

        for (int i = 0; i < listBoard.Count; ++i)
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
        for (int i = 0; i < listBoard.Count; ++i)
        {
            if (listBoard[i].gameObject.activeSelf != bEnable)
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
