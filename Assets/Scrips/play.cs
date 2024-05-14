using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class play : MonoBehaviour
{
    //����ֵ
    private float timeVal;
    private Vector3 bulletEulerAngles;
    public float movespeed = 3;
    private float defendTimeVal = 3; //����ʱ��
    private bool isDefended = true;


    //����
    private SpriteRenderer sr;
    public Sprite[] tankSprite;//�� �� �� �� ̹����ͼ
    public GameObject bulletPrefab;//�ӵ���ͼ
    public GameObject explosionPrefab;//��ը��Ч
    public GameObject defendEffectPrefab;//������Ч

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
        if (isDefended)
        {
            defendEffectPrefab.SetActive(true);//����Ч
            defendTimeVal -= Time.deltaTime;
            if (defendTimeVal <= 0)
            {
                isDefended = false;
                defendEffectPrefab.SetActive(false);//�ر���Ч
            }
        }
        if (timeVal > 0.4f)//ʱ������
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

    //̹�˵Ĺ�����ʽ                                            
    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //�ӵ������ĽǶ�=̹�˵ĽǶ�=�ӵ�Ӧ��ת�ĽǶ�
            Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEulerAngles));
            //��Ϸ���壬λ�ã�����
            timeVal = 0;
        }
    }


    private void Move()//tank�ƶ�����
    {
        float v = Input.GetAxisRaw("Vertical");//��ô�ֱ�������
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

        float h = Input.GetAxisRaw("Horizontal");//���ˮƽ�������
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

    //̹������
    private void Die()
    {
        if (isDefended)//�޵�ʱ�䣬�ӵ��޷��˺�
        {
            return;
        }

        PlayerManager.Instance.isDead = true;
        //������ը��Ч
        Instantiate(explosionPrefab, transform.position, transform.rotation);

        //����
        Destroy(gameObject);

    }
}