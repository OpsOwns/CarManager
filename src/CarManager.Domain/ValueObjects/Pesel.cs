namespace CarManager.Domain.ValueObjects;

public sealed class Pesel : ValueObject
{
    public string Value { get; }
    
    private Pesel(string value)
    {
        Value = value;
    }

    public static Result<Pesel> Create(string value)
    {
        if (string.IsNullOrEmpty(value))
            return Result.Failure<Pesel>(CustomErrors.General.ValueIsRequired());

        if (!IsValidPesel(value))
        {
            return Result.Failure<Pesel>(CustomErrors.Customer.InvalidPesel());
        }

        return new Pesel(value);
    }


    private static bool IsValidPesel(string input)
    {
        int[] weights = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };
        bool result = false;
        if (input.Length == 11)
        {
            int controlSum = CalculateControlSum(input, weights);
            int controlNum = controlSum % 10;
            controlNum = 10 - controlNum;
            if (controlNum == 10)
            {
                controlNum = 0;
            }

            int lastDigit = int.Parse(input[^1].ToString());
            result = controlNum == lastDigit;
        }

        return result;
    }

    private static int CalculateControlSum(string input, int[] weights, int offset = 0)
    {
        int controlSum = 0;
        for (int i = 0; i < input.Length - 1; i++)
        {
            controlSum += weights[i + offset] * int.Parse(input[i].ToString());
        }

        return controlSum;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}