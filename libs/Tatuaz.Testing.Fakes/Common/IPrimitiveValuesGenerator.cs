namespace Tatuaz.Testing.Fakes.Common;

public interface IPrimitiveValuesGenerator
{
    Guid Guids(int index);
    DateTime DateTimes(int index);
    string Strings(int index);
    int Ints(int index);
    float Floats(int index);
    double Doubles(int index);
    decimal Decimals(int index);
}
