using Platformer.Core;
using Platformer.Model;
using UnityEngine;

namespace Platformer.Gameplay
{
    public class PlayerDeath : Simulation.Event<PlayerDeath>
    {
        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public override void Execute()
        {
            var player = model.player;
            if (player.health.IsAlive)
            {
                player.health.Die();
                model.virtualCamera.m_Follow = null;
                model.virtualCamera.m_LookAt = null;
                player.controlEnabled = false;

                // Hiện Game Over UI thay vì animation
                GameUIManager.Instance?.ShowGameOver();

                if (player.audioSource && player.ouchAudio)
                    player.audioSource.PlayOneShot(player.ouchAudio);

                // Không cần animation nữa
                // player.animator.SetTrigger("hurt");
                // player.animator.SetBool("dead", true);

                // Không cần spawn lại tự động nữa
                // Simulation.Schedule<PlayerSpawn>(2);
            }
        }
    }
}
