using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Services
{
    public class StackChecker 
    {
        public bool CanDeleteStack(LinkedList<GameTile.GameTile> tiles, out List<GameTile.GameTile> items)
        {
            List<GameTile.GameTile> tilesList = tiles.GroupBy(tile => tile.TileType)
                .Where(m => m.Count() >= 3).SelectMany(m => m).ToList();

            if (tilesList.Any())
            {
                items = tilesList;
                return true;
            }
        
            items = null;
            return false;
        }
    }
}
