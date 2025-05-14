using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Platformer.Mechanics;
using Platformer.Core; // Dùng cho Simulation.Schedule

public class QuizManager : MonoBehaviour
{
    public GameObject quizPanel;
    public Text questionText;
    public Button[] optionButtons;

    private List<Question> questions = new List<Question>();
    private Question currentQuestion;
    private PlayerController player;
    private EnemyController currentEnemy;

    public bool isAnsweringQuestion = false;

    void Start()
    {
        quizPanel.SetActive(false);
        player = FindObjectOfType<PlayerController>();
        LoadQuestions();
    }

    void LoadQuestions()
    {
        questions = new List<Question>()
        {
            new Question("Thủ đô của Việt Nam là gì?", new string[] {"Hà Nội", "Hải Phòng", "Huế", "Sài Gòn"}, 0),
            new Question("5 + 7 = ?", new string[] {"11", "12", "13", "14"}, 1),
            new Question("Con vật nào biết gáy?", new string[] {"Chó", "Gà trống", "Mèo", "Vịt"}, 1),
            new Question("Mặt trời mọc ở hướng nào?", new string[] {"Tây", "Đông", "Nam", "Bắc"}, 1),
            new Question("Quả nào có nhiều vitamin C?", new string[] {"Cam", "Chuối", "Dưa hấu", "Táo"}, 0),
            new Question("Cái gì càng lấy đi nhiều thì nó càng lớn?", new string[] {"Lỗ hổng", "Tình yêu", "Thời gian", "Ánh sáng"}, 0),
            new Question("Ai là người viết truyện 'Dế Mèn Phiêu Lưu Ký'?", new string[] {"Nguyễn Du", "Tô Hoài", "Nam Cao", "Xuân Diệu"}, 1),
            new Question("1 tuần có bao nhiêu ngày?", new string[] {"5", "6", "7", "8"}, 2),
            new Question("Nước biển có vị gì?", new string[] {"Ngọt", "Chua", "Mặn", "Nhạt"}, 2),
            new Question("Động vật nào sau đây biết bay?", new string[] {"Chó", "Mèo", "Chim", "Heo"}, 2),
            new Question("Trái đất quay quanh gì?", new string[] {"Mặt trăng", "Mặt trời", "Sao Hỏa", "Sao Kim"}, 1),
            new Question("Số lớn hơn 99 nhưng nhỏ hơn 101 là?", new string[] {"98", "99", "100", "101"}, 2),
            new Question("Con gì đi bằng 4 chân và kêu meo meo?", new string[] {"Chó", "Gà", "Mèo", "Heo"}, 2)
        };
    }

    public void ShowRandomQuestion(EnemyController enemy)
    {
        currentEnemy = enemy;
        isAnsweringQuestion = true;

        int randomIndex = Random.Range(0, questions.Count);
        currentQuestion = questions[randomIndex];

        questionText.text = currentQuestion.question;

        for (int i = 0; i < optionButtons.Length; i++)
        {
            if (i < currentQuestion.options.Length)
            {
                optionButtons[i].GetComponentInChildren<Text>().text = currentQuestion.options[i];
                int index = i;
                optionButtons[i].onClick.RemoveAllListeners();
                optionButtons[i].onClick.AddListener(() => OnAnswerSelected(index));
                optionButtons[i].gameObject.SetActive(true);
            }
            else
            {
                optionButtons[i].gameObject.SetActive(false);
            }
        }

        quizPanel.SetActive(true);

        if (player != null)
            player.controlEnabled = false;
    }

    void OnAnswerSelected(int selectedIndex)
    {
        quizPanel.SetActive(false);
        isAnsweringQuestion = false;

        if (selectedIndex == currentQuestion.correctAnswerIndex)
        {
            Debug.Log("✅ Trả lời đúng!");

            if (player != null)
                player.controlEnabled = true;

            if (currentEnemy != null)
            {
                var health = currentEnemy.GetComponent<Health>();
                if (health != null)
                {
                    health.Decrement();
                    if (!health.IsAlive)
                    {
                        Destroy(currentEnemy.gameObject);
                    }
                }
                else
                {
                    // Nếu không có Health component thì vẫn xóa quái
                    Destroy(currentEnemy.gameObject);
                }
            }
        }
        else
        {
            Debug.Log("❌ Trả lời sai! Người chơi chết.");
            Simulation.Schedule<Platformer.Gameplay.PlayerDeath>();
        }
    }

    [System.Serializable]
    public class Question
    {
        public string question;
        public string[] options;
        public int correctAnswerIndex;

        public Question(string question, string[] options, int correctAnswerIndex)
        {
            this.question = question;
            this.options = options;
            this.correctAnswerIndex = correctAnswerIndex;
        }
    }
}