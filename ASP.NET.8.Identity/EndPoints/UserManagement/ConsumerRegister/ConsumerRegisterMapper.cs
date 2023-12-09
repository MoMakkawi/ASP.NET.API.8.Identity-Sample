using ASP.NET8.Identity.Identity;

using Riok.Mapperly.Abstractions;


namespace ASP.NET8.Identity.EndPoints.UserManagement.ConsumerRegister;


[Mapper]
public partial class ConsumerRegisterMapper
{
    public partial Consumer Map(ConsumerRegisterModel model);
}
