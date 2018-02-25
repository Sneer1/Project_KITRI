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

    //Stage Text
    E_TEXTTYPE e_StageText = E_TEXTTYPE.STAGE1_S;
    

    GameObject TargetUI;
    float StackTime = 0.0f;

    public E_SCENETYPE CURRENT_SCENE
    {
        get { return CurrentState; }
    }

    public void LoadScene(E_SCENETYPE _type, bool _async = true)
    {
        if (CurrentState == _type)
            return;

        NextState = _type;
        IsAsyc = _async;
    }

    void Update()
    {
        if (Operation != null)
        {
            // Loding UI Set
            // UI_Tools.Instance.ShowLoadingUI(Operation.progress);
            if(TargetUI)
            StackTime = TargetUI.GetComponent<UI_Loading>().GetSlider.value = Operation.progress + 0.1f;

            // if (Operation.isDone == true)
            if (Operation.isDone == true && StackTime >= 1.0f)
            {
                CurrentState = NextState;
                ComplateLoad(CurrentState);

                Operation = null;
                NextState = E_SCENETYPE.SCENE_NONE;

                // Loding UI 삭제
                UI_Tools.Instance.HideUI(E_UITYPE.PF_UI_LOADING);
            }
            else
                return;
        }

        if (CurrentState == E_SCENETYPE.SCENE_NONE)
            return;

        if (NextState != E_SCENETYPE.SCENE_NONE
            && CurrentState != NextState)
        {
            DisableScene(CurrentState);

            if (IsAsyc)
            { // 비동기 로드
                Operation = SceneManager.LoadSceneAsync(
                    NextState.ToString("F"));

                // Loading UI Set
                TargetUI = UI_Tools.Instance.ShowUI(E_UITYPE.PF_UI_LOADING);
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
                UI_Tools.Instance.ShowUI(E_UITYPE.PF_UI_INTRO);
                break;
            case E_SCENETYPE.SCENE_CONVERSATION:
                UI_Conversation conversation = UI_Tools.Instance.ShowUI(E_UITYPE.PF_UI_CONVERSATION).GetComponent<UI_Conversation>();
                conversation.Init(e_StageText);
                e_StageText++;
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
