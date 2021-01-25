using SlothEnterprise.External;

namespace SlothEnterprise.ProductApplication
{
    public static class ApplicationResultExceptions
    {
        public static int ToProductId(this IApplicationResult result) 
            => result.Success
                ? result.ApplicationId ?? -1 
                : -1;
    }
}
