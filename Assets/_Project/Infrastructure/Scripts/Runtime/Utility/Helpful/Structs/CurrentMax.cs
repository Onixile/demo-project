namespace _Project.Infrastructure.Scripts.Runtime.Utility.Helpful.Structs
{
  public struct CurrentMaxInt
  {
    public          int Current;
    public readonly int Max;

    public CurrentMaxInt(int current, int max)
    {
      Current = current;
      Max = max;
    }
  }
}