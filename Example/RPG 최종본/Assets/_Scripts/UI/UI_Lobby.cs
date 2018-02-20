using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Lobby : BaseObject
{
	UIButton StageBtn = null;
	UIButton GachaBtn = null;
	UIButton InvenBtn = null;

	private void Awake()
	{
		Transform trans = FindInChild("StageBtn");
		if(trans == null)
		{
			Debug.LogError("StageBtn is Not Founded");
			return;
		}
		StageBtn = trans.GetComponent<UIButton>();
		EventDelegate.Add(StageBtn.onClick,
			new EventDelegate(this, "ShowStage"));

		trans = FindInChild("GachaBtn");
		if (trans == null)
		{
			Debug.LogError("GachaBtn is Not Founded");
			return;
		}
		GachaBtn = trans.GetComponent<UIButton>();
		EventDelegate.Add(GachaBtn.onClick,
			() =>
			{
				ItemManager.Instance.Gacha();
			});

		trans = FindInChild("InventoryBtn");
		if (trans == null)
		{
			Debug.LogError("InventoryBtn is Not Founded");
			return;
		}
		InvenBtn = trans.GetComponent<UIButton>();
		EventDelegate.Add(InvenBtn.onClick,
			new EventDelegate(this, "ShowInventory"));
	}

	void ShowStage()
	{
		GameObject go = UI_Tools.Instance.ShowUI(eUIType.PF_UI_STAGE);
		UI_Stage stage = go.GetComponent<UI_Stage>();
		stage.Init();
	}

	void ShowInventory()
	{
		GameObject go = UI_Tools.Instance.ShowUI(eUIType.PF_UI_INVENTORY);
		UI_Inventory inven = go.GetComponent<UI_Inventory>();
		inven.Init();
		inven.Reset();
	}


}
