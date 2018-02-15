using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoSingleton<NoteManager>
{
    List<GameObject> MyNoteList = new List<GameObject>();

    ESCORETYPE EMyScore;

    GameObject NoteCheckObject;
    private void Awake()
    {
        if (Instance == null)
        {
            Debug.LogError("매니저 생성실패");
            return;
        }
        SetNoteToList();
    }

    private void Update()
    {
        NoteCheck();
    }

    void NoteCheck()
    {
        if (MyNoteList.Count == 0)
            return;
        float distance;
        if (GetNearNoteTrans() == null)
        {
            Debug.LogError("가까운 노트를 찾지 못했습니다");
            return;
        }

        distance = Vector3.SqrMagnitude(transform.localPosition - GetNearNoteTrans().localPosition);

        Debug.Log(distance);

        if (MyNoteList[0].GetComponent<Note>().NoteIsOver() == true)
        {
            RemoveNote();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (distance < 5000)
            {
                if (distance > 15)
                {
                    EMyScore = ESCORETYPE.Score_Miss;
                    Debug.Log("Score_Miss");
                }
                else if (distance > 10)
                {
                    EMyScore = ESCORETYPE.Score_Good;
                    Debug.Log("Score_Good");
                }
                else if (distance > 5)
                {
                    EMyScore = ESCORETYPE.Score_Great;
                    Debug.Log("Score_Great");
                }
                else if (distance >= 0)
                {
                    EMyScore = ESCORETYPE.Score_Excellent;
                    Debug.Log("Score_Excellent");
                }
                RemoveNote();
            }
        }
    }

    void SetNoteToList()
    {
        GameObject testObject;
        testObject = Instantiate(Resources.Load<GameObject>("Prefabs/Note/NoteUI"), transform);



        Transform trans = null;
        for (int i = 0; i < testObject.transform.childCount; ++i)
        {
            if (testObject.transform.GetChild(i).name.Equals("NotePanel"))
                trans = testObject.transform.GetChild(i);
        }



        Transform notetrans = null;
        for (int i = 0; i < trans.childCount; ++i)
        {
            if (trans.GetChild(i).name.Equals("Note"))
                notetrans = trans.GetChild(i);

            if (trans.GetChild(i).name.Equals("NoteCheck"))
                NoteCheckObject = trans.GetChild(i).gameObject;
        }

        for (int i = 0; i < notetrans.childCount; ++i)
        {
            MyNoteList.Add(notetrans.GetChild(i).gameObject);
            //Debug.Log(trans.GetChild(i).GetComponent<GameObject>().name);
        }

        if (MyNoteList.Count == 0)
        {
            Debug.LogError("노트 오브젝트를 찾지 못했습니다");
        }
    }

    Transform GetNearNoteTrans()
    {
        if (MyNoteList.Count == 0)
        {
            //Debug.LogError("리스트가 비어있습니다");
            return null;
        }
        Transform targetTrans = null;
        Vector3 Origin = NoteCheckObject.transform.localPosition;

        float distance;
        float min = Vector3.SqrMagnitude(Origin - MyNoteList[0].transform.localPosition);

        for (int i = 0; i < MyNoteList.Count; ++i)
        {
            distance = Vector3.SqrMagnitude(Origin - MyNoteList[i].transform.localPosition);
            if (distance < min)
            {
                min = Vector3.SqrMagnitude(Origin - MyNoteList[i].transform.localPosition);
                targetTrans = MyNoteList[i].transform;
            }
        }

        return targetTrans;
    }

    void RemoveNote()
    {
        if (MyNoteList.Count == 0)
        {
            Debug.Log("노트 없음");
            return;
        }
        Destroy(MyNoteList[0]);
        MyNoteList.RemoveAt(0);
    }
}