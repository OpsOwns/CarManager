﻿namespace CarManager.Domain.ValueObjects;

public class Password : ValueObject
{
    private const string PasswordComplexityExpression =
        @"^(?=.*[a-zA-Z])(?=.*\d.*\d)(?=.*[!@#$%^&*~/""()_=+\[\]\\|,.?-]).*$";

    private Password(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<Password> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) return Result.Failure<Password>(DomainErrors.General.ValueIsRequired());

        if (!Regex.IsMatch(value, PasswordComplexityExpression))
            return Result.Failure<Password>(DomainErrors.UserAuth.PasswordBreakComplexityRules());

        return value.Length switch
        {
            > 200 => Result.Failure<Password>(DomainErrors.General.ValueIsTooLong(200)),
            < 6 => Result.Failure<Password>(DomainErrors.General.ValueIsTooShort(6)),
            _ => Result.Success(new Password(value))
        };
    }

    public static implicit operator string(Password value)
    {
        return value.Value;
    }

    public override string ToString()
    {
        return Value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public HashedPassword Hash()
    {
        var salt = HashCalculator.GenerateSalt();
        var hash = HashCalculator.Calculate(Value, salt);

        return new HashedPassword(hash, salt);
    }


    public bool IsMatch(HashedPassword hashedPassword)
    {
        var hash = HashCalculator.Calculate(Value, hashedPassword.Salt);

        return hashedPassword.Hash == hash;
    }
}

internal static class HashCalculator
{
    public static string Calculate(string password, string salt)
    {
        var passwordBytes = Encoding.ASCII.GetBytes(password);
        var saltBytes = Convert.FromBase64String(salt);

        var buffer = new byte[passwordBytes.Length + saltBytes.Length];
        saltBytes.CopyTo(buffer, 0);
        passwordBytes.CopyTo(buffer, saltBytes.Length);

        var hashBuffer = SHA256.HashData(buffer);

        return Convert.ToBase64String(hashBuffer);
    }

    public static string GenerateSalt()
    {
        Random rnd = new();
        var salt = new byte[32];
        rnd.NextBytes(salt);

        return Convert.ToBase64String(salt);
    }
}