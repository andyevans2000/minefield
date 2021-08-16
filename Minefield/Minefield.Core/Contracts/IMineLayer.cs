namespace Minefield.Core.Contracts
{
    public interface IMineLayer
    {
        /// <summary>
        /// try to lay a mine in a aqaure
        /// </summary>
        /// <returns></returns>
        bool TryToLayMine();
    }
}
