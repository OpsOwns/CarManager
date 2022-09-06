﻿namespace CarManager.Domain.Errors;

public static class DomainErrors
{
    public static class General
    {
        public static Error NotFound(long? id = null)
        {
            var forId = id == null ? "" : $" for Id '{id}'";
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
    }

    public static class UserAuth
    {
        public static Error PasswordBreakComplexityRules()
        {
            return new Error("auth.password.complexity", "Password complexity rules");
        }
    }
}