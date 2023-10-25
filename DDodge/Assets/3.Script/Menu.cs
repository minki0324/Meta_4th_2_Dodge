using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject HUD;

    public void GameStart()
    {
        HUD.gameObject.SetActive(true);
        gameObject.SetActive(false);
        GameManager.instance.Resume();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

}
