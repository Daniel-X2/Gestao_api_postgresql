


public class Crypto
{
    public static bool verificar(string text,string hash_banco)
    {
       
        if (BCrypt.Net.BCrypt.Verify(text, hash_banco))
        {
            return true;
        }

        throw new InvalidPassword();
    }

    public static  string retornHash(string text)
    {
        string hashPassword=  BCrypt.Net.BCrypt.HashPassword(text);
        
        return hashPassword;
    }
}