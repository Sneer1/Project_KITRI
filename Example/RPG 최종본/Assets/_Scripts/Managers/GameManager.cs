using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
	bool IsInit = false;
	public Actor PlayerActor;

	public int SelectStage = 0;
	StageInfo SelectStageInfo = null;

	bool IsGameOver = true;
	public bool GAME_OVER { get { return IsGameOver; } }
	float StackTime = 0.0f;
	int KillCount = 0;

	//// Delete Code [ Test Code ]
	//private void Start()
	//{
	//	GameInit();
	//	LoadGame();
	//}

	public void GameInit()
	{
		if (IsInit == true)
			return;

		StageManager.Instance.StageInit();
		ItemManager.Instance.ItemInit();

		IsInit = true;
	}

	public void LoadGame()
	{
		// Init
		StackTime = 0.0f;
		KillCount = 0;
		IsGameOver = false;

		// StageLoad
		SelectStageInfo = StageManager.Instance.LoadStage(SelectStage);
		// PlayerLoad
		PlayerActor = ActorManager.Instance.PlayerLoad();
		// Player Item Setting
		foreach(KeyValuePair<eSlotType,ItemInstance> pair in 
			ItemManager.Instance.DIC_EQUIP)
		{
			StatusData status = pair.Value.ITEM_INFO.STATUS;
			PlayerActor.SELF_CHARACTER.CHARACTER_STATUS.AddStatusData(
				pair.Key.ToString(), status);
		}

		PlayerActor.SELF_CHARACTER.IncreaseCurrentHP(99999999999999);

		BaseBoard hpBoard = BoardManager.Instance.GetBoardData(
			PlayerActor, eBoardType.BOARD_HP);
		if(hpBoard != null)
		{
			hpBoard.SetData(ConstValue.SetData_HP,
				PlayerActor.GetStatusData(eStatusData.MAX_HP),
				PlayerActor.SELF_CHARACTER.CURRENT_HP);
		}
		// Clear Setting
		if(SelectStageInfo.CLEAR_TYPE == eClearType.CLEAR_TIME)
		{
			UIManager.Instance.SetText(false,
				(float)SelectStageInfo.CLEAR_FINISH - StackTime);
		}
		else
		{
			UIManager.Instance.SetText(true,
				(float)SelectStageInfo.CLEAR_FINISH - KillCount);
		}

		// Camera Setting
		CameraManager.Instance.CameraInit(PlayerActor);
	}

	//void Start ()
	//{
	//	GameObject Player = GameObject.Find("Player");
	//	Actor playerScripts = Player.GetComponent<Actor>();
	//	CameraManager.Instance.CameraInit(playerScripts);
	//}
	
	void Update ()
	{
		if (IsGameOver == true)
			return;

		// Scene != GameScene return;
		if (Scene_Manager.Instance.CURRENT_SCENE != eSceneType.SCENE_GAME)
			return;

		if (SelectStageInfo.CLEAR_TYPE == eClearType.CLEAR_TIME)
		{
			StackTime += Time.deltaTime;
			// UISetting
			UIManager.Instance.SetText(false,
				(float)SelectStageInfo.CLEAR_FINISH - StackTime);

			if (SelectStageInfo.CLEAR_FINISH < StackTime)
			{
				IsGameOver = true;
				SetGameOver();
			}
		}
	}

	public void KillCheck(Actor _dieActor)
	{
		if (IsGameOver == true)
			return;

		// Scene != GameScene return;
		if (Scene_Manager.Instance.CURRENT_SCENE != eSceneType.SCENE_GAME)
			return;

		if (SelectStageInfo.CLEAR_TYPE != eClearType.CLEAR_KILLCOUNT)
			return;

		// Player Check
		if (PlayerActor.TEAM_TYPE == _dieActor.TEAM_TYPE)
			return;

		KillCount++;
		// UISetting
		UIManager.Instance.SetText(true,
				(float)SelectStageInfo.CLEAR_FINISH - KillCount);

		if (SelectStageInfo.CLEAR_FINISH <= KillCount)
		{
			IsGameOver = true;
			SetGameOver();
		}
	}

	void SetGameOver()
	{
		Time.timeScale = 0.1f;
		Debug.Log("GameOver");
		Invoke("GoLobby", 0.5f);
	}

	void GoLobby()
	{
		Time.timeScale = 1f;
		// Scene Load Lobby
		Scene_Manager.Instance.LoadScene(eSceneType.SCENE_LOBBY);
	}
}
