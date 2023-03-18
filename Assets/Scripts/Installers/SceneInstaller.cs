using Generator;
using Player;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private PlayerInventory playerInventory;
        [SerializeField] private CatsCreator catsCreator;
        
        public override void InstallBindings()
        {
            Container.Bind<PlayerInventory>().FromInstance(playerInventory).AsSingle();
            Container.Bind<CatsCreator>().FromInstance(catsCreator).AsSingle();
        }
    }
}