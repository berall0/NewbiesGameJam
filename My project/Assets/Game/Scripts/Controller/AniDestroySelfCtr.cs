using UnityEngine;

namespace Game.Scripts.Controller
{
    public class AniDestroySelfCtr: MonoBehaviour
    {
        void DestroySelf()
        {
            Destroy(gameObject);
        }
    }
}