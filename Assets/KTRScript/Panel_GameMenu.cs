using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel_GameMenu : MonoBehaviour
{
    Animator _animator;

    public GameObject btLeft;
    public GameObject btRight;
    public GameObject btHeart;

    bool bOpened = false;

    // Use this for initialization
    void Start () {
        _animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClickMainMenu()
    {
        bOpened = !bOpened;
        if (bOpened)
        {
            btLeft.SetActive(true);
            btRight.SetActive(true);
            btHeart.SetActive(true);
        }
        else
        {
            //btLeft.SetActive(false);
            //btRight.SetActive(false);
            //btHeart.SetActive(false);
        }

        _animator.SetBool("isOpen", bOpened);
    }

    public void OnAnimEndBtClose()
    {
        btLeft.SetActive(false);
        btRight.SetActive(false);
        btHeart.SetActive(false);
    }

    public void OnClickBtLeft()
    {

    }

    public void OnClickBtRight()
    {

    }

    public void OnClickBtHeart()
    {

    }
}
