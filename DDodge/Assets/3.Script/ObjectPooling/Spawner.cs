using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject DDONG_Prefab;

    [SerializeField]
    private Queue<GameObject> Pool_Q;

    public float SpawnTime { get; private set; } = 0.1f;

    //[SerializeField]
    //private Sprite Hit_Range_indicator;




    private float x;
    private float y;
    private float z;

    private Vector3 poolPos;

    [SerializeField]
    private int Prefabs_Count = 20;


    private void Awake()
    {

        Pool_Q = new Queue<GameObject>(Prefabs_Count);
        

        //합치고 똥떨어질 범위 정해서 x,y,z 바꾸기!_!

        Init();

       
        StartCoroutine(SpawnDDong_Coroutine());

    }

    private void Init()
    {
        for (int i = 0; i < Prefabs_Count; i++)
        {
            //GameObject newDDONG = Instantiate(DDONG_Prefab, poolPos, Quaternion.identity);
            Pool_Q.Enqueue(ObjectPooling());



        }
    }


    private GameObject ObjectPooling()
    {
        x = Random.Range(-15, 8);
        y = Random.Range(10, 20);
        z = Random.Range(-28, 1);
        poolPos = new Vector3(x, y, z);
        GameObject newDDONG = Instantiate(DDONG_Prefab, poolPos, Quaternion.identity);
        newDDONG.gameObject.SetActive(false);
        newDDONG.transform.SetParent(transform);

        //Vector3 indiPos = new Vector3(newDDONG.transform.position.x, 0f, newDDONG.transform.position.z);
        //GameObject indicator = Instantiate(newDDONG.GetComponent<DDONG>().Hit_Range_indicator, indiPos, Quaternion.identity);
        //indicator.transform.SetParent(newDDONG.transform);


        return newDDONG;
    }

    private IEnumerator SpawnDDong_Coroutine()
    {
        WaitForSeconds seconds = new WaitForSeconds(SpawnTime);
        while (true)
        {
            // Vector3 Position = poolPos;

            TakeOut_Pool();

            yield return seconds;

        }
    }

    public void TakeOut_Pool()
    {
        if (Pool_Q.Count <= 0)
        {
            return;
        }

        GameObject DDONG = Pool_Q.Dequeue();
        if (DDONG.activeSelf == false)
        {
            DDONG.SetActive(true);


        }
    }

    public void TakeIn_Pool(GameObject ddong)
    {
        x = Random.Range(-15, 8);
        y = Random.Range(10, 20);
        z = Random.Range(-28, 1);
        poolPos = new Vector3(x, y, z);
        ddong.transform.position = poolPos;

        if (ddong.activeSelf)
        {
            ddong.SetActive(false);


        }
        Pool_Q.Enqueue(ddong);
    }
}
