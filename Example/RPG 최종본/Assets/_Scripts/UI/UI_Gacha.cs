using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Gacha : MonoBehaviour
{
	UILabel Label = null;
	UITexture Texture = null;

	ItemInstance itemInstance;
	public ItemInstance ITEM_INSTANCE
	{
		get { return itemInstance; }
		set { itemInstance = value; }
	}

	public void Init(ItemInstance instance)
	{
		itemInstance = instance;

		Label = this.transform.Find("Contents").GetComponent<UILabel>();
		Texture = this.transform.Find("Texture").GetComponent<UITexture>();

		Label.text = instance.ITEM_INFO.GetSlotString()
			+ " : " + itemInstance.ITEM_INFO.NAME +
			"\n" + instance.ITEM_INFO.STATUS.StatusString();
		Texture.mainTexture = Resources.Load<Texture>("Textures/"
								 + itemInstance.ITEM_INFO.ITEM_IMAGE);

		transform.Find("Effect").
			GetComponent<TweenAlpha>().ResetToBeginning();
		transform.Find("Effect").
			GetComponent<TweenAlpha>().enabled = true;
	}
	public void YesClick()
	{
		UI_Tools.Instance.HideUI(eUIType.PF_UI_GACHA);
	}

}
