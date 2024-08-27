namespace _Project.Infrastructure.Scripts.Runtime.Utility.Helpful.Structs
{
  public readonly struct MinMax
  {
    public readonly int Min;
    public readonly int Max;

    public MinMax(int min, int max)
    {
      Min = min;
      Max = max;
    }
  }
}