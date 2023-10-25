using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    private Text time_txt;

    private void Awake()
    {
        TryGetComponent(out time_txt);
    }

    private void Update()
    {
        int sec = Mathf.FloorToInt(GameManager.instance.GameTime % 60f);
        int min = Mathf.FloorToInt(GameManager.instance.GameTime / 60f);
        time_txt.text = $"{min:D2} : {sec:D2}";
    }
}
