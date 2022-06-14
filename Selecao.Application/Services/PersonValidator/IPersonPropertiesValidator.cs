namespace Selecao.Application.Services.PersonValidator
{
    public interface IPersonPropertiesValidator
    {
        bool PersonIsValid(string name, string lastName, string phoneNumber);
    }
}
