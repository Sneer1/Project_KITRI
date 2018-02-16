using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoSingleton<Scene_Manager>
{
    bool IsAsyc = false;
    AsyncOperation Operation = null;

    E_SCENETYPE CurrentState = E_SCENETYPE.SCENE_TITLE;
    E_SCENETYPE NextState = E_SCENETYPE.SCENE_NONE;

    float StackTime = 0.0f;

    public E_SCENETYPE CURRENT_SCENE
    {
        get { return CurrentState; }
    }

    public void LoadScene(E_SCENETYPE _type, bool _async = false)
    {
        if (CurrentState == _type)
            return;

        NextState = _type;
        IsAsyc = _async;
    }

    void Update()
    {
        //if (Operation != null)
        //{
        //    StackTime += Time.deltaTime;
        //    // Loding UI Set
        //    // UI_Tools.Instance.ShowLoadingUI(Operation.progress);
        //    UI_Tools.Instance.ShowLoadingUI(StackTime / 2f);

        //    // if (Operation.isDone == true)
        //    if (Operation.isDone == true && StackTime >= 2.0f)
        //    {
        //        CurrentState = NextState;
        //        ComplateLoad(CurrentState);

        //        Operation = null;
        //        NextState = E_SCENETYPE.SCENE_NONE;

        //        // Loding UI 삭제
        //        UI_Tools.Instance.HideUI(E_UITYPE.PF_UI_LOADING, true);
        //    }
        //    else
        //        return;
        //}

        if (CurrentState == E_SCENETYPE.SCENE_NONE)
            return;

        if (NextState != E_SCENETYPE.SCENE_NONE
            && CurrentState != NextState)
        {
            DisableScene(CurrentState);

            if (IsAsyc)
            { // 비동기 로드
                //Operation = SceneManager.LoadSceneAsync(
                //    NextState.ToString("F"));
                //StackTime = 0.0f;
                //// Loading UI Set
                //UI_Tools.Instance.ShowLoadingUI(0.0f);
            }
            else
            { // 동기 로드
                SceneManager.LoadScene(NextState.ToString("F"));
                CurrentState = NextState;
                NextState = E_SCENETYPE.SCENE_NONE;
                ComplateLoad(CurrentState);
            }
        }
    }

    void ComplateLoad(E_SCENETYPE _type)
    {
        switch (_type)
        {
            case E_SCENETYPE.SCENE_NONE:
                break;
            case E_SCENETYPE.SCENE_TITLE:
                break;
            case E_SCENETYPE.SCENE_INTRO:
                break;
            case E_SCENETYPE.SCENE_STAGE_1:
                break;
            case E_SCENETYPE.SCENE_END:
                break;
            default:
                break;
        }
    }

    void DisableScene(E_SCENETYPE _type)
    {
        // 신 유아이 삭제
        UI_Tools.Instance.Clear();

        switch (_type)
        {
            case E_SCENETYPE.SCENE_NONE:
                break;
            case E_SCENETYPE.SCENE_TITLE:
                break;
            case E_SCENETYPE.SCENE_INTRO:                
                break;
            case E_SCENETYPE.SCENE_STAGE_1:
                break;
            case E_SCENETYPE.SCENE_END:
                break;
            default:
                break;

                //				SkillManager.Instance.ClearSkill();
                //				LobbyManager.Instance.DisableLobby();
        }
    }
}
