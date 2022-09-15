namespace CarManager.Domain.Errors;

public static class CustomErrors
{
    public static class General
    {
        public static Error NotFound(Id? id = null)
        {
            var forId = id == null ? "" : $" for Id '{id.Value}'";
            return new Error("record.not.found", $"Record not found{forId}");
        }

        public static Error ValueIsInvalid()
        {
            return new("value.is.invalid", "Value is invalid");
        }

        public static Error ValueIsTooLong(int maxValue)
        {
            return new("value.is.toLong", $"Value is too long, max length is {maxValue}");
        }

        public static Error ValueIsTooShort(int minValue)
        {
            return new("value.is.toShort", $"Value is too short, minValue length is {minValue}");
        }

        public static Error ValueIsRequired()
        {
            return new("value.is.required", "Value is required");
        }

        public static Error InvalidLength(string? name)
        {
            var label = name == null ? " " : " " + name + " ";
            return new Error("invalid.string.length", $"Invalid{label}length");
        }

        public static Error CollectionIsTooSmall(int min, int current)
        {
            return new Error(
                "collection.is.too.small",
                $"The collection must contain {min} items or more. It contains {current} items.");
        }

        public static Error CollectionIsTooLarge(int max, int current)
        {
            return new Error(
                "collection.is.too.large",
                $"The collection must contain {max} items or more. It contains {current} items.");
        }

        public static Error InternalServerError(string message)
        {
            return new Error("internal.server.error", message);
        }

        public static Error InvalidLink(string value)
        {
            return new Error("link.invalid", $"Invalid link {value}");
        }
    }

    public static class UserAuth
    {
        public static Error RoleIsOutOfRange(string roles)
        {
            return new Error("auth.role.outOfRange", $"invalid role only available roles are {roles}");
        }

        public static Error PasswordBreakComplexityRules()
        {
            return new Error("auth.password.complexity", "Password complexity rules");
        }

        public static Error EmailAlreadyExists(string email)
        {
            return new Error("auth.email.exists", $"The email {email} already exists in the system");
        }

        public static Error UserNotFoundByEmail(string email)
        {
            return new Error("auth.email.notfound", $"The email {email} was not found in the system");
        }

        public static Error InvalidPassword()
        {
            return new Error("auth.password.invalid", "The password is invalid");
        }

        public static Error UserNotFound()
        {
            return new Error("auth.user.notfound", "The user was not found");
        }

        public static Error RefreshTokenNotFound()
        {
            return new Error("auth.refreshToken.notfound", "The refresh token was not found");
        }

        public static Error RefreshTokenExpired()
        {
            return new Error("auth.refreshToken.expired", "The refresh token expired");
        }

        public static Error RefreshTokenUsed()
        {
            return new Error("auth.refreshToken.used", "The refresh token was used");
        }
    }

    public static class Customer
    {
        public static Error InvalidPesel()
        {
            return new Error("customer.pesel.invalid", "The pesel is invalid");
        }

        public static Error PeselAlreadyExists()
        {
            return new Error("customer.pesel.exists", "The pesel is already exists");
        }
    }

    public static class Car
    {
        public static Error ProductionYearIsTooLow()
        {
            return new Error("car.year.low", "Production year can't be less than 1910");
        }

        public static Error CarAlreadyRegistered(Guid id)
        {
            return new Error("car.registered", $"Car with id {id} is already registered");
        }
    }
}