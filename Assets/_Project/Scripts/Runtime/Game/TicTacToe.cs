using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Runtime.Game
{
  public class TicTacToe : MonoBehaviour
  {
    private const int FieldSize = 3;

    private Sprite _crossSprite;
    private Sprite _circleSprite;

    private FillType _playerFillType;
    private FillType _botFillType;

    private bool _isPlayerTurn;

    private Dictionary<Vector2Int, Cell> _inputCells;

    private Action _onPlayerWin;
    private Action _onBotWin;
    private Action _onNoWinners;

    public void Initialization(TicTacToeConfig config,
      Func<Transform, GameObject> onCreateCellPanel, Action onPlayerWin, Action onBotWin, Action onNoWinners)
    {
      transform.position += config.FiledOffset;
      
      _onNoWinners = onNoWinners;
      _onBotWin = onBotWin;
      _onPlayerWin = onPlayerWin;

      _crossSprite = config.CrossSprite;
      _circleSprite = config.CircleSprite;
      
      _playerFillType = FillType.Cross;
      _botFillType = FillType.Circle;

      _isPlayerTurn = true;

      _inputCells = new Dictionary<Vector2Int, Cell>();

      CreateField();

      StartCoroutine(Game());

      void CreateField()
      {
        for (int x = 0; x < FieldSize; x++)
        {
          for (int y = 0; y < FieldSize; y++)
          {
            Vector2 spawnPosition = new Vector2(config.StartPosition.x + config.CellOffset * x, config.StartPosition.y + config.CellOffset * y);
            GameObject cellPanel = onCreateCellPanel.Invoke(transform);
            cellPanel.transform.localPosition = spawnPosition;
            cellPanel.gameObject.name = $"Input Panel [{x}, {y}]";

            Vector2Int key = new Vector2Int(x, y);

            Cell cell = cellPanel.GetComponentInChildren<Cell>();
            cell.Initialization(delegate { PlayerTurn(key); });

            _inputCells.Add(key, cell);
          }
        }
      }
    }

    private IEnumerator Game()
    {
      while (true)
      {
        if (EndGameCheck())
          break;

        if (!_isPlayerTurn)
        {
          yield return new WaitForSeconds(0.5f);

          BotTurn();
          _isPlayerTurn = true;
        }

        yield return null;
      }
    }

    private bool EndGameCheck()
    {
      if (CheckForWin(out FillType winType))
      {
        if (_playerFillType == winType)
          _onPlayerWin?.Invoke();
        else
          _onBotWin?.Invoke();

        StopCoroutine(Game());
        return true;
      }

      if (CheckForFilledField())
      {
        _onNoWinners?.Invoke();

        StopCoroutine(Game());
        return true;
      }

      return false;
    }

    private void PlayerTurn(Vector2Int key)
    {
      if (!_isPlayerTurn)
        return;

      if (!IsValidTurn(key))
        return;

      if (_inputCells.ContainsKey(key))
        FillCell(key, _playerFillType);

      _isPlayerTurn = false;
    }

    private void BotTurn()
    {
      if (GetWinningTurnForBot(out Vector2Int wKey))
      {
        Turn(wKey);
        return;
      }

      if (GetBlockingTurnForBot(out Vector2Int bKey))
      {
        Turn(bKey);
        return;
      }

      RandomTurn();

      void Turn(Vector2Int key)
      {
        if (IsValidTurn(key))
          FillCell(key, _botFillType);
        else
          RandomTurn();
      }

      void RandomTurn()
      {
        Vector2Int key = new Vector2Int();
        do
        {
          key.x = Random.Range(0, FieldSize);
          key.y = Random.Range(0, FieldSize);
        } while (!IsValidTurn(key));

        FillCell(key, _botFillType);
      }
    }

    private bool CheckForWin(out FillType cellFillType)
    {
      cellFillType = FillType.Empty;

      if (_inputCells == null)
        return false;

      for (int i = 0; i < FieldSize; i++)
      {
        if (Compare(_inputCells[new Vector2Int(i, 0)], _inputCells[new Vector2Int(i, 1)], _inputCells[new Vector2Int(i, 2)]))
        {
          cellFillType = _inputCells[new Vector2Int(i, 0)].FillType;
          return true;
        }

        if (Compare(_inputCells[new Vector2Int(0, i)], _inputCells[new Vector2Int(1, i)], _inputCells[new Vector2Int(2, i)]))
        {
          cellFillType = _inputCells[new Vector2Int(0, i)].FillType;
          return true;
        }
      }

      if (Compare(_inputCells[new Vector2Int(0, 0)], _inputCells[new Vector2Int(1, 1)], _inputCells[new Vector2Int(2, 2)]))
      {
        cellFillType = _inputCells[new Vector2Int(0, 0)].FillType;
        return true;
      }

      if (Compare(_inputCells[new Vector2Int(0, 2)], _inputCells[new Vector2Int(1, 1)], _inputCells[new Vector2Int(2, 0)]))
      {
        cellFillType = _inputCells[new Vector2Int(0, 2)].FillType;
        return true;
      }

      return false;

      bool Compare(Cell cellA, Cell cellB, Cell cellC) =>
        cellA.FillType != FillType.Empty && cellA.FillType == cellB.FillType && cellA.FillType == cellC.FillType;
    }

    private bool CheckForFilledField() =>
      _inputCells.Values.All(cell => cell.FillType != FillType.Empty);

    private bool IsValidTurn(Vector2Int key) =>
      _inputCells[key].FillType == FillType.Empty;

    private bool GetWinningTurnForBot(out Vector2Int key) =>
      GetTurnForBot(_botFillType, out key);

    private bool GetBlockingTurnForBot(out Vector2Int key) =>
      GetTurnForBot(_playerFillType, out key);

    private bool GetTurnForBot(FillType fillType, out Vector2Int key)
    {
      for (int i = 0; i < FieldSize; i++)
      {
        if (CheckLine(new Vector2Int(i, 0), new Vector2Int(i, 1), new Vector2Int(i, 2), fillType, out key))
          return true;

        if (CheckLine(new Vector2Int(0, i), new Vector2Int(1, i), new Vector2Int(2, i), fillType, out key))
          return true;
      }

      if (CheckLine(new Vector2Int(0, 0), new Vector2Int(1, 1), new Vector2Int(2, 2), fillType, out key))
        return true;

      if (CheckLine(new Vector2Int(0, 2), new Vector2Int(1, 1), new Vector2Int(2, 0), fillType, out key))
        return true;

      key = new Vector2Int(-1, -1);
      return false;

      bool CheckLine(Vector2Int cell1, Vector2Int cell2, Vector2Int cell3, FillType cfillType, out Vector2Int ckey)
      {
        if (_inputCells[cell1].FillType == cfillType && _inputCells[cell2].FillType == cfillType && _inputCells[cell3].FillType != FillType.Circle)
        {
          ckey = new Vector2Int(cell3.x, cell3.y);
          return true;
        }

        if (_inputCells[cell1].FillType == cfillType && _inputCells[cell3].FillType == cfillType && _inputCells[cell2].FillType != FillType.Circle)
        {
          ckey = new Vector2Int(cell2.x, cell2.y);
          return true;
        }

        if (_inputCells[cell2].FillType == cfillType && _inputCells[cell3].FillType == cfillType && _inputCells[cell1].FillType != FillType.Circle)
        {
          ckey = new Vector2Int(cell1.x, cell1.y);
          return true;
        }

        ckey = new Vector2Int(-1, -1);
        return false;
      }
    }

    private void FillCell(Vector2Int key, FillType fillType) =>
      _inputCells[key].Fill(fillType, GetIconSprite(fillType));

    private Sprite GetIconSprite(FillType fillType)
    {
      return fillType switch
      {
        FillType.Circle => _circleSprite,
        FillType.Cross => _crossSprite,
        _ => null
      };
    }
  }
}