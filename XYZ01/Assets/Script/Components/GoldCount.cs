using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components
{
    public class GoldCount : MonoBehaviour
    {
        int CountCoin;
        
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("SilverCoin"))
            {
                CountCoin++;
            }
            else
                if (collision.gameObject.CompareTag("GoldCoin"))
            {
                CountCoin += 10;
            }
            Debug.Log("Монет: " + CountCoin);
        }

    }
}
