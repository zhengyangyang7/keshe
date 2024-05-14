using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerManager : MonoBehaviour
{
    //����ֵ
    public int lifeValue = 3;
    public int playerScore = 0;
    public bool isDead;
    public bool isDefeat;
    //����
    public GameObject born;
    public Text playerLifeValueText;
    public Text playerScoreText;
    public GameObject isDefeatUI;
    //����
    private static PlayerManager instance;

    public static PlayerManager Instance { get => instance; set => instance = value; }

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isDefeat)
        {
            isDefeatUI.SetActive(true);
            Invoke("returnToMainMenu", 3);
            return;
        }
        if (isDead)
        {
            Recover();
        }
        playerScoreText.text = playerScore.ToString();
        playerLifeValueText.text = lifeValue.ToString();
    }
    public void Recover()
    {
        if (lifeValue < 1)
        {
            //��Ϸʧ�ܣ�����������
            isDefeat = true;
            Invoke("returnToMainMenu", 3);
        }
        else
        {
            lifeValue--;
            GameObject go = Instantiate(born, new Vector3(-2, -8, 0), Quaternion.identity);
            go.GetComponent<born>().createPlayer = true;
            isDead = false;
        }
    }
    private void returnToMainMenu()
    {

        SceneManager.LoadScene(0);
    }
}
