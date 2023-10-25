using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDONG : MonoBehaviour
{
    private Spawner spawner;

    public GameObject Hit_Range_indicator;


    private void Update()
    {
        spawner = FindObjectOfType<Spawner>();
        StartCoroutine(move_Down());
    }

    private void Start()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Debug.Log("¹Ù´Ú¿¡ Ãæµ¹");
            spawner.TakeIn_Pool(gameObject);


        }
    }


    private IEnumerator move_Down()
    {
        WaitForSeconds wfs = new WaitForSeconds(1f);
        while (true)
        {
            transform.position += new Vector3(0, -0.01f, 0);
            yield return wfs;
        }
    }
}
