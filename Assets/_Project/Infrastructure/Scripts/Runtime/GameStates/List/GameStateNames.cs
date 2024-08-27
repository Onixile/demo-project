namespace _Project.Infrastructure.Scripts.Runtime.GameStates.List
{
  public readonly struct GameStateNames
  {
    public readonly string Scene;
    public readonly string UIGroupLabel;
    public readonly string ConfigsGroupLabel;
    public readonly string ObjectsGroupLabel;

    public GameStateNames(string stateMarker)
    {
      Scene = $"{stateMarker}";
      UIGroupLabel = $"ui_{stateMarker}";
      ConfigsGroupLabel = $"config_{stateMarker}";
      ObjectsGroupLabel = $"object_{stateMarker}";
    }
  }
}