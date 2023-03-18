using Managers.Game_States;
using Managers.Scene_Manager;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class ServicesInstaller : MonoInstaller
    {
        [SerializeField] private GameStateManager gameStateManager;
        [SerializeField] private MySceneManager mySceneManager;
        
        public override void InstallBindings()
        {
            Container.Bind<GameStateManager>().FromInstance(gameStateManager).AsSingle();
            Container.Bind<MySceneManager>().FromInstance(mySceneManager).AsSingle();
        }
    }
}