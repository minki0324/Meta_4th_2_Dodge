using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SaveData
{
    public string ID;
    public int Score;
}

[System.Serializable]
public class HighScore
{
    public SaveData[] highscore;
}

public class Ranking : MonoBehaviour
{
    public SaveData innerdata;
    public HighScore highScore = new HighScore();

    [SerializeField] private Text[] RankingTxt;

    List<SaveData> objList = new List<SaveData>();

    private string SAVE_DATA_DIRECTORY;
    private string SAVE_FILENAME = "/SaveFile.txt";

    private void Awake()
    {
        SAVE_DATA_DIRECTORY = Application.dataPath + "/Saves/";

        if(!Directory.Exists(SAVE_DATA_DIRECTORY))
        {
            Directory.CreateDirectory(SAVE_DATA_DIRECTORY);
        }
        LoadData();
    }

    public void SaveData_m()
    {
        SaveData newData = new SaveData
        {
            ID = GameManager.instance.Neckname,
            Score = (int)GameManager.instance.GameTime
        };

        SaveData existingData = objList.Find(data => data.ID == newData.ID);
        if (existingData != null)
        {
            // 기존 데이터가 있고, 새 점수가 더 높으면 업데이트
            if (newData.Score > existingData.Score)
            {
                existingData.Score = newData.Score;
            }
        }
        else
        {
            // 기존 데이터가 없으면 추가
            objList.Add(newData);
        }

        // 리스트를 HighScore 객체에 할당
        highScore.highscore = objList.ToArray();

        string json = JsonUtility.ToJson(highScore);
        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME, json);

        Debug.Log("저장 완료");
        Debug.Log(json);
    }

    public void LoadData()
    {
        if (File.Exists(SAVE_DATA_DIRECTORY + SAVE_FILENAME))
        {
            string json = File.ReadAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME);
            HighScore highScore = JsonUtility.FromJson<HighScore>(json);
            objList = new List<SaveData>(highScore.highscore);
        }
    }

    public void DisplayRanking()
    {
        // objList를 점수가 높은 순으로 정렬
        objList.Sort((data1, data2) => data2.Score.CompareTo(data1.Score));

        // 상위 10개(또는 전체 데이터)를 표시
        for (int i = 0; i < Mathf.Min(10, objList.Count); i++)
        {
            RankingTxt[i].gameObject.SetActive(true);
            RankingTxt[i].text = $"No.{i+1} Player {objList[i].ID} [ {objList[i].Score} ]";
            Debug.Log($"Rank {i + 1}: {objList[i].ID} - {objList[i].Score}");
        }
    }
}
