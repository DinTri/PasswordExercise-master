namespace PasswordExercise.Contracts
{
    public interface IPasswordGenerator
    {
        string GeneratePassword(PasswordRequirements requirements);
       
    }
}
