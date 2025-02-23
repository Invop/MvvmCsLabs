namespace Lab4.Exceptions;

public class FutureBirthDateException : ArgumentException
{
    public FutureBirthDateException()
        : base("Дата народження не може бути в майбутньому")
    {
    }

    public FutureBirthDateException(string message)
        : base(message)
    {
    }

    public FutureBirthDateException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}

public class PastBirthDateException : ArgumentException
{
    public PastBirthDateException()
        : base("Дата народження занадто далеко в минулому (>135 років)")
    {
    }

    public PastBirthDateException(string message)
        : base(message)
    {
    }

    public PastBirthDateException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}

public class InvalidEmailException : FormatException
{
    public InvalidEmailException()
        : base("Неправильний формат email адреси")
    {
    }

    public InvalidEmailException(string message)
        : base(message)
    {
    }

    public InvalidEmailException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}