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
        [SerializeField] private Inventory playerInventory;
        
        public override void InstallBindings()
        {
            Container.Bind<Inventory>().FromInstance(playerInventory).AsSingle();
        }
    }
}