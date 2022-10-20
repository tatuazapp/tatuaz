using NodaTime;

namespace Tatuaz.Testing.Fakes.Common;

public class PrimitiveValuesGenerator : IPrimitiveValuesGenerator
{
    private readonly decimal[] _decimals;
    private readonly double[] _doubles;
    private readonly float[] _floats;
    private readonly Guid[] _guids;
    private readonly Instant[] _instants;
    private readonly int[] _ints;
    private readonly string[] _strings;

    public PrimitiveValuesGenerator()
    {
        _guids = new Guid[10];
        _instants = new Instant[10];
        _strings = new string[10];
        _ints = new int[10];
        _floats = new float[10];
        _doubles = new double[10];
        _decimals = new decimal[10];

        for (var i = 0; i < 10; i++)
        {
            _guids[i] = Guid.NewGuid();
            _instants[i] = Instant
                .FromUtc(2020, 1, 1, 0, 0)
                .Plus(Duration.FromMilliseconds(Random.Shared.Next()));
            _strings[i] = Guid.NewGuid().ToString();
            _ints[i] = Random.Shared.Next();
            _floats[i] = Random.Shared.NextSingle() * 2048f - 1024f;
            _doubles[i] = Random.Shared.NextDouble() * 2048.0 - 1024.0;
            _decimals[i] = (decimal)(Random.Shared.NextDouble() * 2048.0 - 1024.0);
        }
    }

    public Guid Guids(int index)
    {
        if (index < 0 || index >= 10)
        {
            throw new ArgumentException(null, nameof(index));
        }

        return _guids[index];
    }

    public Instant Instants(int index)
    {
        if (index < 0 || index >= 10)
        {
            throw new ArgumentException(null, nameof(index));
        }

        return _instants[index];
    }

    public string Strings(int index)
    {
        if (index < 0 || index >= 10)
        {
            throw new ArgumentException(null, nameof(index));
        }

        return _strings[index];
    }

    public int Ints(int index)
    {
        if (index < 0 || index >= 10)
        {
            throw new ArgumentException(null, nameof(index));
        }

        return _ints[index];
    }

    public float Floats(int index)
    {
        if (index < 0 || index >= 10)
        {
            throw new ArgumentException(null, nameof(index));
        }

        return _floats[index];
    }

    public double Doubles(int index)
    {
        if (index < 0 || index >= 10)
        {
            throw new ArgumentException(null, nameof(index));
        }

        return _doubles[index];
    }

    public decimal Decimals(int index)
    {
        if (index < 0 || index >= 10)
        {
            throw new ArgumentException(null, nameof(index));
        }

        return _decimals[index];
    }
}
