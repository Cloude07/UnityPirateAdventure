using UnityEngine;

namespace PixelCrew.Utils
{
    public static class GameObjectExtensions 
    {
       public static bool IsInLayer(this GameObject go, LayerMask layer)
        {
            /*смешивает слои для определения
             * 0001 go.layer
             * 0110 mask
             * 0111
             *|-побитовая или
             */
            
            return layer == (layer | 1 << go.layer);
        }
    }
}
