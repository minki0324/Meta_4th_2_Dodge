using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    [SerializeField] private InputField nameInputField;

    public void OnbuttonName()
    {
        Debug.Log("�Էµ� �̸�: " + nameInputField.text);
        GameManager.instance.Neckname = nameInputField.text;
        Debug.Log(GameManager.instance.Neckname);

        // �Է� �ʵ带 �б� �������� �����, ����ڰ� �г����� ������ �� ������ �մϴ�.
        nameInputField.readOnly = true;
    }
}
