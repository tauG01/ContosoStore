using Volo.Abp;

namespace ContosoStore.Customers;

public class CustomerAlreadyExistsException : BusinessException
{
    public CustomerAlreadyExistsException(string email)
        : base(ContosoStoreDomainErrorCodes.CustomerAlreadyExists)
    {
        WithData("email", email);
    }
}

