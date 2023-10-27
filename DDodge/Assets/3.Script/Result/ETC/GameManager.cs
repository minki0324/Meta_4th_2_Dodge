using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private Ranking ranking;
    [SerializeField] private GameObject GameOverUI;

    public float GameTime;
    public bool isLive = false;

    public string Neckname = null;
    public int Score;

    private void Awake()
    {
        instance = this;
        Stop();
    }

    private void Update()
    {
        GameTime += Time.deltaTime;
    }

    public void GameOver()
    {
        StartCoroutine(GameOver_co());
    }

    private IEnumerator GameOver_co()
    {
        GameOverUI.gameObject.SetActive(true);
        isLive = false;
        ranking.SaveData_m();
        ranking.DisplayRanking();

        yield return new WaitForSeconds(0.5f);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Stop();
    }

    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
        
}
