using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreation : MonoBehaviour
{
    //����װ��ʼ����ͼ�������������
    //0.home 1.wall 2.barrier 3.born effect 4.river 5.grass 6.air barrier
    //wall���Ա��ӵ����ƣ���barrier����
    public GameObject[] item;
    private List<Vector3> itemPositionList = new List<Vector3>();//�Ѿ��������λ���б�
    private void Awake()
    {
        InitMap();
    }
    private void InitMap()
    {

        //ʵ�����ϼ��ڵ�ͼ�м��·�
        CreateItem(item[0], new Vector3(0, -8, 0), Quaternion.identity);
        //��ǽ���ϼ�Χ����
        CreateItem(item[1], new Vector3(-1, -8, 0), Quaternion.identity);
        CreateItem(item[1], new Vector3(1, -8, 0), Quaternion.identity);
        CreateItem(item[1], new Vector3(-1, -7, 0), Quaternion.identity);
        CreateItem(item[1], new Vector3(0, -7, 0), Quaternion.identity);
        CreateItem(item[1], new Vector3(1, -7, 0), Quaternion.identity);

        //ʵ������Χǽ
        for (int i = -20; i <= 20; i++)
        {
            CreateItem(item[6], new Vector3(i, 9, 0), Quaternion.identity);
            CreateItem(item[6], new Vector3(i, -9, 0), Quaternion.identity);
        }
        for (int i = -8; i <= 8; i++)
        {
            CreateItem(item[6], new Vector3(-20, i, 0), Quaternion.identity);
            CreateItem(item[6], new Vector3(20, i, 0), Quaternion.identity);

        }
        //ʾ�������ϰ�������25��ǽ80��
        for (int i = 0; i <= 80; i++)
        {
            CreateItem(item[1], CreateRandomPosition(), Quaternion.identity);

        }
        for (int i = 0; i <= 25; i++)
        {
            CreateItem(item[2], CreateRandomPosition(), Quaternion.identity);

        }
        for (int i = 0; i <= 25; i++)
        {
            CreateItem(item[4], CreateRandomPosition(), Quaternion.identity);

        }
        for (int i = 0; i <= 25; i++)
        {
            CreateItem(item[5], CreateRandomPosition(), Quaternion.identity);

        }
        //��ʼ�����
        GameObject go = Instantiate(item[3], new Vector3(-2, -8, 0), Quaternion.identity);
        go.GetComponent<born>().createPlayer = true;
        //��������
        Instantiate(item[3], new Vector3(-19, 8, 0), Quaternion.identity);
        Instantiate(item[3], new Vector3(0, 8, 0), Quaternion.identity);
        Instantiate(item[3], new Vector3(19, 8, 0), Quaternion.identity);

        InvokeRepeating("createEnemy", 4, 5);

    }
    //���÷�װ�ķ���
    private void CreateItem(GameObject createGameObject, Vector3 CreatePosition, Quaternion createRotation)
    {//�÷�����ʵ��������������������mapcreation����ɢ����game��
        GameObject itemgo = Instantiate(createGameObject, CreatePosition, createRotation);
        itemgo.transform.SetParent(gameObject.transform);
        itemPositionList.Add(CreatePosition);
    }
    //�������λ�õķ���
    private Vector3 CreateRandomPosition()
    {
        //��ͼ��Ե���������壬������ܲ����޷�ͨ�صĵ�ͼ
        while (true)
        {
            //����һ�����λ��
            Vector3 createPosition = new Vector3(Random.Range(-18, 19), Random.Range(-7, 8), 0);
            //������λ�ò����б���,�ͷ���
            if (!HasThePosition(createPosition))
                return createPosition;
        }

    }
    private bool HasThePosition(Vector3 createpos)
    {
        for (int i = 0; i < itemPositionList.Count; i++)
        {
            if (createpos == itemPositionList[i])
                return true;
        }
        return false;
    }
    private void createEnemy()
    {
        int num = Random.Range(0, 3);
        if (num == 0)
        {
            Instantiate(item[3], new Vector3(-19, 8, 0), Quaternion.identity);
        }
        else if (num == 1)
        {
            Instantiate(item[3], new Vector3(0, 8, 0), Quaternion.identity);
        }
        else
        {
            Instantiate(item[3], new Vector3(19, 8, 0), Quaternion.identity);
        }
    }
}
