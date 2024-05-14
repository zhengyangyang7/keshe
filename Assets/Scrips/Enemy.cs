using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    //����ֵ

    private Vector3 bulletEulerAngles;
    public float movespeed = 3;
    private float v;
    private float h;

    //����
    private SpriteRenderer sr;
    public Sprite[] tankSprite;//�� �� �� �� ̹����ͼ
    public GameObject bulletPrefab;//�ӵ���ͼ
    public GameObject explosionPrefab;//��ը��Ч
    public GameObject defendEffectPrefab;//������Ч

    //��ʱ��
    private float timeVal;
    private float TurnTimeVal;


    private void Awake()//ȡ���� ����start
    {
        sr = GetComponent<SpriteRenderer>();//�����Ⱦ���
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timeVal > 3f)//ʱ������
        {
            Attack();
        }
        else
        {
            timeVal += Time.deltaTime;
        }
    }
    private void FixedUpdate()//�������ں��� ��update��ִ�� ʱ��ƽ�� ÿһ֡�̶�
    {

        Move();

    }
    private void Move()//tank�ƶ�����
    {
        if (TurnTimeVal > 4)
        {
            int num = Random.Range(0, 8);
            if (num >= 5)
            {
                v = -1;
                h = 0;
            }
            else if (num == 0)
            {
                v = 1;
                h = 0;
            }
            else if (num > 0 && num <= 2)
            {
                h = -1;
                v = 0;
            }
            else if (num > 2 && num <= 4)
            {
                h = 1;
                v = 0;
            }
            TurnTimeVal = 0;
        }
        else
        {
            TurnTimeVal += Time.deltaTime;
        }

        transform.Translate(Vector3.up * v * movespeed * Time.fixedDeltaTime, Space.World);
        if (v < 0)
        {
            sr.sprite = tankSprite[2];//��ת����
            bulletEulerAngles = new Vector3(0, 0, -180);
        }
        else if (v > 0)
        {
            sr.sprite = tankSprite[0];//��ת����
            bulletEulerAngles = new Vector3(0, 0, 360);
        }
        if (v != 0)//�������ȼ��������ֱ�����в�����ֱ�ӷ��أ�����ˮƽ����
        {
            return;
        }


        transform.Translate(Vector3.right * h * movespeed * Time.fixedDeltaTime, Space.World);//x��ķ���
        if (h < 0)
        {
            sr.sprite = tankSprite[3];//��ת����
            bulletEulerAngles = new Vector3(0, 0, 90);
        }
        else if (h > 0)
        {
            sr.sprite = tankSprite[1];//��ת�� ��
            bulletEulerAngles = new Vector3(0, 0, -90);
        }


    }
    //̹�˵Ĺ�����ʽʱ����                                       
    private void Attack()
    {

        //�ӵ������ĽǶ�=̹�˵ĽǶ�=�ӵ�Ӧ��ת�ĽǶ�
        Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEulerAngles));
        //��Ϸ���壬λ�ã�����
        timeVal = 0;

    }




    //̹������
    private void Die()
    {
        PlayerManager.Instance.playerScore++;
        //������ը��Ч
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        //����
        Destroy(gameObject);
    }
}
