using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    [SerializeField] private InputField nameInputField;

    public void OnbuttonName()
    {
        Debug.Log("입력된 이름: " + nameInputField.text);
        GameManager.instance.Neckname = nameInputField.text;
        Debug.Log(GameManager.instance.Neckname);

        // 입력 필드를 읽기 전용으로 만들어, 사용자가 닉네임을 변경할 수 없도록 합니다.
        nameInputField.readOnly = true;
    }
}
