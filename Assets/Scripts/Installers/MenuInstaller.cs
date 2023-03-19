using UI.Windows_Manager;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class MenuInstaller: MonoInstaller
    {
        //[SerializeField] private PlayerInventory playerInventory;
        [SerializeField] private WindowsManager windowsManager;
        
        public override void InstallBindings()
        {
            //Container.Bind<PlayerInventory>().FromInstance(playerInventory).AsSingle();
            Container.Bind<WindowsManager>().FromInstance(windowsManager).AsSingle();
        }
    }
}