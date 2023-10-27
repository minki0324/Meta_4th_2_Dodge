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
            // ���� �����Ͱ� �ְ�, �� ������ �� ������ ������Ʈ
            if (newData.Score > existingData.Score)
            {
                existingData.Score = newData.Score;
            }
        }
        else
        {
            // ���� �����Ͱ� ������ �߰�
            objList.Add(newData);
        }

        // ����Ʈ�� HighScore ��ü�� �Ҵ�
        highScore.highscore = objList.ToArray();

        string json = JsonUtility.ToJson(highScore);
        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME, json);

        Debug.Log("���� �Ϸ�");
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
        // objList�� ������ ���� ������ ����
        objList.Sort((data1, data2) => data2.Score.CompareTo(data1.Score));

        // ���� 10��(�Ǵ� ��ü ������)�� ǥ��
        for (int i = 0; i < Mathf.Min(10, objList.Count); i++)
        {
            RankingTxt[i].gameObject.SetActive(true);
            RankingTxt[i].text = $"No.{i+1} Player {objList[i].ID} [ {objList[i].Score} ]";
            Debug.Log($"Rank {i + 1}: {objList[i].ID} - {objList[i].Score}");
        }
    }
}
