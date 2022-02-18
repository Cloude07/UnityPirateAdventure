using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PixelCrew.Model;

namespace PixelCrew.Components
{
    public class ReloadLevelComponent : MonoBehaviour
    {
      public void Reload()
        {
            var session = FindObjectOfType<GameSession>();
            //Уничтожит gameObject
            Destroy(session.gameObject); 

            //Получение загруженой сцены
            var scene = SceneManager.GetActiveScene();
            //загрузка сцены
            SceneManager.LoadScene(scene.name);
        }
    }
}
