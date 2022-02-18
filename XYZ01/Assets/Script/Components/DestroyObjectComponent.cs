using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PixelCrew.Components
{
    public class DestroyObjectComponent : MonoBehaviour
    {
        [SerializeField] GameObject _objectToDestroy;
    public void DestroyObject()
        {
            //уничтожение обьекта
            Destroy(_objectToDestroy);
        }


        public void DestroyObject(Image _image)
        {
            if (_image.fillAmount < 1)
            {
                Destroy(_objectToDestroy);
            }
            else if(_image.fillAmount == 1)
            {
                Debug.Log("Full hp");
            }
        }

        public void DestroyObject(bool _status)
        {
            if (_status == true)
            {
                Destroy(_objectToDestroy);
            }
        }
    }


}
