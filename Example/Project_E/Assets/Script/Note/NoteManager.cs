using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteManager : MonoSingleton<NoteManager>
{
    List<GameObject> MyNoteList = new List<GameObject>();

    ESCORETYPE EMyScore;

    GameObject NoteCheckObject;
    GameObject NoteGage;

    float MaxScore = 100f;
    float CurScore = 5f;

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

        Slider NoteScore = NoteGage.GetComponent<Slider>();
        
        if (GetNearNoteTrans().GetComponent<Note>().NoteIsOver() == true)
        {
            RemoveNote();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (distance < 10000)
            {
                if (distance > 5000)
                {
                    EMyScore = ESCORETYPE.Score_Miss;
                    Debug.Log("Score_Miss");
                }
                else if (distance > 3000)
                {
                    EMyScore = ESCORETYPE.Score_Good;
                    Debug.Log("Score_Good");
                    CurScore += 0.3f;
                }
                else if (distance > 1000)
                {
                    EMyScore = ESCORETYPE.Score_Great;
                    Debug.Log("Score_Great");
                    CurScore += 0.5f;
                }
                else if (distance >= 500)
                {
                    EMyScore = ESCORETYPE.Score_Perpect;
                    Debug.Log("Score_Perpect");
                    CurScore += 1f;
                }
                RemoveNote();
            }
        }
        NoteScore.value = CurScore / MaxScore;
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


 //       notetrans = Instantiate(Resources.Load<GameObject>("Prefabs/Note/" + selectmusic), trans).transform;

        for (int i = 0; i < trans.childCount; ++i)
        {
            if (trans.GetChild(i).name.Equals("NoteCheck"))
                NoteCheckObject = trans.GetChild(i).gameObject;

            if (trans.GetChild(i).name.Equals("Gage"))
                NoteGage = trans.GetChild(i).gameObject;
        }

        for (int i = 0; i < notetrans.childCount; ++i)
        {
            MyNoteList.Add(notetrans.GetChild(i).gameObject);
        }

        if (MyNoteList.Count == 0)
        {
            Debug.LogError("노트 오브젝트를 찾지 못했습니다");
        }

        SortNoteTrans();
    }

    void SortNoteTrans()
    {
        Vector3 Origin = NoteCheckObject.transform.localPosition;
        MyNoteList.Sort(delegate (GameObject A, GameObject B)
        {
            float ADistance = Vector3.SqrMagnitude(Origin - A.transform.localPosition);
            float BDistance = Vector3.SqrMagnitude(Origin - B.transform.localPosition);

            if (ADistance > BDistance) return 1;
            else if (ADistance < BDistance) return -1;
            return 0;
        });
    }

    Transform GetNearNoteTrans()
    {
        if (MyNoteList.Count == 0)
        {
            //Debug.LogError("리스트가 비어있습니다");
            return null;
        }

        Transform targetTrans = MyNoteList[0].transform;
        return targetTrans;
    }

    void RemoveNote()
    {
        if (MyNoteList.Count == 0)
        {
            Debug.Log("노트 없음");
            return;
        }

        Destroy(GetNearNoteTrans().gameObject);
        MyNoteList.Remove(GetNearNoteTrans().gameObject);
    }
}