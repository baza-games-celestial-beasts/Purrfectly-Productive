using Managers.Game_States;
using Managers.Scene_Manager;
using Player;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Installers
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private PlayerInventory playerInventory;
        
        public override void InstallBindings()
        {
            Container.Bind<PlayerInventory>().FromInstance(playerInventory).AsSingle();
        }
    }
}