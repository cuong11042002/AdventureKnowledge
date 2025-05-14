using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine;

namespace Platformer.Gameplay
{
    public class PlayerEnemyCollision : Simulation.Event<PlayerEnemyCollision>
    {
        public EnemyController enemy;
        public PlayerController player;

        private static QuizManager quizManager;

        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public override void Execute()
        {
            if (player == null || enemy == null)
            {
                Debug.LogWarning("Player hoặc Enemy null trong PlayerEnemyCollision.");
                return;
            }

            if (quizManager == null)
            {
                quizManager = GameObject.FindObjectOfType<QuizManager>();
                if (quizManager == null)
                {
                    Debug.LogError("QuizManager không tồn tại trong scene!");
                    return;
                }
            }

            // Nếu đang trả lời thì không cho va chạm tiếp
            if (quizManager.isAnsweringQuestion)
                return;

            // Dừng điều khiển Player và hiện câu hỏi
            quizManager.ShowRandomQuestion(enemy);
        }
    }
}
