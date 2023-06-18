using GameTile;
using Infrastructure.Services;
using Storage;
using Types;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class ServicesInstaller : MonoInstaller
    {
        [SerializeField] private TileMover _tileMover;
        [SerializeField] private TilesSpawner _tilesSpawner;

        public override void InstallBindings()
        {
            Container.Bind<PickedStorage>().AsSingle();
            Container.Bind<StackChecker>().AsSingle();
            Container.Bind<TileMover>().FromInstance(_tileMover).AsSingle();
            Container.Bind<TileDestroyer>().AsSingle();
            Container.Bind<LooseWinCheacker>().AsSingle();
            Container.Bind<TilesSpawner>().FromInstance(_tilesSpawner).AsSingle();
            Container.Bind<GameFactory>().AsSingle();
            Container.BindFactory<TileType, GameTile.GameTile, GameTile.GameTile.Factory>().AsSingle();
            Container.Bind<TilesInitializer>().AsSingle();
            Container.Bind<TileClickEventer>().AsSingle();
            Container.Bind<LevelDataLoader>().AsSingle();
            Container.Bind<SaveLoadService>().AsSingle();
        }
    }
}
