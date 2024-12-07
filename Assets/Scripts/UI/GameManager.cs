using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; //Singleton
    public Image gameoverBoard;
    public TMP_Text resultScore;
    public ImgsFillDynamic imgsFillDynamic;
    public bool isGameOver;
    private int skillGauge = 0;
    private int skillGaugeMax = 100;

    private void Awake()
    {
        if (Instance == null) //Singleton
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        // Q 키를 눌렀을 때 점프
        if (Input.GetKeyDown(KeyCode.Q) && skillGauge == skillGaugeMax)
        {
            Debug.Log("skillGauge : 0");
            skillGauge = 0;
            var allObjects = FindObjectsOfType<VeggiesCombine>();
            var filteredObjects = allObjects.Where(obj => obj.elapsedTime == -1.0f).ToList();
            foreach (var obj in filteredObjects)
            {
                obj.Combine();
            }
        }
    }

    public void OnGameOver()
    {
        if (isGameOver == false) 
        {
            gameoverBoard.gameObject.SetActive(true);
            imgsFillDynamic.gameObject.SetActive(false);
            resultScore.text = "Score:\n" + ScoreManager.Instance.score;
            isGameOver = true;
            Debug.Log("Game Over");
        }
    }

    public void shinyCombine(int point)
    {
        skillGauge = Mathf.Min(skillGaugeMax, skillGauge + point);
        Debug.Log($"skillGauge : {skillGauge}");
    }


}
