using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using Tiles;
using UnityEngine;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when a player collides with a coin.
    /// </summary>
    /// <typeparam name="PlayerCollision"></typeparam>
    public class PlayerCoinCollision : Simulation.Event<PlayerCoinCollision>
    {
        public PlayerController player;
        public Coin coin;
        PlatformerModel model = Simulation.GetModel<PlatformerModel>();
        
        public override void Execute()
        {
            player.CoinPlus();
            player.UpdateUI();
            AudioSource.PlayClipAtPoint(coin.tokenCollectAudio, coin.transform.position);
        }
    }
}