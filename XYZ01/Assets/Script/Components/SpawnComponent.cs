using UnityEngine;
using System;
namespace PixelCrew.Components
{
    public class SpawnComponent : MonoBehaviour
    {
        [SerializeField] Transform _target; //место спавна
        [SerializeField] GameObject _prefabObject; //еденичный респавн обьекта

        [ContextMenu("Spawn")]
        public void Spawn()
        {
            //Создание обьекта(ссылка на обьект, позиция, ротация)
            var instantiate = Instantiate(_prefabObject, _target.position, Quaternion.identity);
            instantiate.transform.localScale = _target.lossyScale;
        }

        [ContextMenu("RespawnObjects")]
        public void RespawnObjects()
        {
            GameObjectDrop gameObjectClass = new GameObjectDrop();

            var _objectDrop = gameObjectClass.PrefabObject;
            var _count = gameObjectClass.CountObjectPrefabs;
            var _dropRarity = gameObjectClass.DropRarity;
            var _mode = gameObjectClass.DropMode;
            
            if (_mode == true)
            {

            }
            else
            {
                if (_count >= 1)
                {
                    PrefabCount(_count, _objectDrop);
                }
                else
                {
                    Debug.Log("Количество обьектов не указано");
                    return;
                }
            }
        }



        void CreatePrefabs(Vector2 _target, GameObject[] _countObject)
        {

            for (int i = 0; i < _countObject.Length; i++)
            {
                var instantiate = Instantiate(_countObject[i], _target, Quaternion.identity);
                instantiate.transform.localScale = this._target.lossyScale;
      
            }
        } 

        void PrefabCount(int _count, GameObject[] _objectDrop)
        {
            for (int i = 0; i < _count; i++) //сколько раз
            {
     

            }
        }


    }

    [Serializable]
      public class GameObjectDrop
    {
        [SerializeField] GameObject[] _prefabObjects;
        [SerializeField] int _countObjectPrefabs;
        [SerializeField] bool _dropMode;
        [SerializeField] int _dropRarity;

        public GameObject[] PrefabObject => _prefabObjects;
        public int CountObjectPrefabs => _countObjectPrefabs;
        public int DropRarity => _dropRarity;
        public bool DropMode => _dropMode;
    }
}
