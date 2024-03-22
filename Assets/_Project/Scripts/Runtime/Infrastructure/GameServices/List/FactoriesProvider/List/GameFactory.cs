using _Project.Scripts.Runtime.Game;
using _Project.Scripts.Runtime.Infrastructure.GameServices.List.AssetsProvider;
using UnityEngine;

namespace _Project.Scripts.Runtime.Infrastructure.GameServices.List.FactoriesProvider.List
{
  public class GameFactory : Factory
  {
    private const string TicTacToeConfigPath = "Tic Tac Toe Config";
    private const string TicTacToePanelPath  = "Tic Tac Toe";
    private const string CellPanelPath = "Cell Panel";

    public GameFactory(IAssetsProvider assetsProvider) : base(assetsProvider)
    {
    }

    public TicTacToeConfig GetTicTacToeConfig() => 
      LoadFromResources<TicTacToeConfig>(TicTacToeConfigPath);

    public TicTacToe CreateTicTacToe(Transform parent) => 
      InstantiateFromAssetsGroup(TicTacToePanelPath, parent).GetComponent<TicTacToe>();

    public GameObject CreateCellPanel(Transform parent) => 
      InstantiateFromAssetsGroup(CellPanelPath, parent);
  }
}
