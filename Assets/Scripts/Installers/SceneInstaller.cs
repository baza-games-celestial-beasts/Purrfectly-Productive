using Generator_Logic;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private CatsCreator catsCreator;
        [SerializeField] private Generator generator;
        
        public override void InstallBindings()
        {
            Container.Bind<CatsCreator>().FromInstance(catsCreator).AsSingle();
            Container.Bind<Generator>().FromInstance(generator).AsSingle();
        }
    }
}