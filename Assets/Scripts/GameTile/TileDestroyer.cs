using System.Collections.Generic;
using Sirenix.Utilities;

namespace GameTile
{
    public class TileDestroyer
    {
        public void DestroyTiles(IEnumerable<GameTile> tiles) => 
            tiles.ForEach(tile => tile.Disable());
    
        public void DestroyTile(GameTile tile) => 
            tile.Disable();
    }
}
