using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    bool isOver = false;
    float _notespeed = 0f;
    //float _radius = 0f;
    Vector3 _notemoveTrans = Vector3.zero;
    Vector3 _noteDir = Vector3.right;

    private void Awake()
    {
        Init(5f);
    }
    private void FixedUpdate()
    {
        NoteMove();

    }
    
    public bool NoteIsOver()
    {
        if (NoteDir == Vector3.right)
        {
            if (transform.position.x > 5)
                return true;
        }
        else
        {
            if (transform.position.x < -5)
                return true;
        }
        return false;
    }
    /*public float NoteRadius
    {
        get { return _radius; }
        set { _radius = value; }
    }*/

    public Vector3 NoteMoveTrans
    {
        get { return _notemoveTrans; }
        set { _notemoveTrans = value; }
    }
    public float NoteSpeed
    {
        get { return _notespeed; }
        set { _notespeed = value; }
    }
    public Vector3 NoteDir
    {
        get { return _noteDir; }
        set { _noteDir = value; }
    }

    //노트 이동 노트의 속도와 방향 좌우에서 나타나는 노트의 이동를 구현
    public void NoteMove()
    {
        NoteMoveTrans = NoteSpeed * NoteDir * Time.deltaTime;
        transform.Translate(NoteMoveTrans);
    }

    public GameObject GetMyGameObject()
    {
        return gameObject;
    }

    //노트의 속도변화
    public void SetNoteSpeed(float notespeed)
    {
        NoteSpeed = notespeed;
    }

    public void NoteDirSet()
    {
        if (transform.position.x > 0)
        {
            NoteDir = -NoteDir;
        }
    }

    public void Init(float speed)
    {
        SetNoteSpeed(speed);
        NoteDirSet();
    }
}