using Volo.Abp;

namespace ContosoStore.Merchants;

public class MerchantAlreadyExistsException : BusinessException
{
    public MerchantAlreadyExistsException(string email) : base(ContosoStoreDomainErrorCodes.MerchantAlreadyExists)
    {
        WithData("email", email);
    }
}

