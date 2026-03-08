namespace LinkVaultApi.Exceptions
{
    public class Exceptions:Exception
    {
        public Exceptions(string msg):base(msg) 
        {
            
        }
    }
    public class DulipcateException:Exception
    {
    public DulipcateException(string msg) : base(msg)
    {
        
    }
    }
    public class BadRequestException : Exception
    {
    public BadRequestException(string msg):base(msg) 
    {
        
    }
    }
    public class NotFoundException:Exception
    {
        public NotFoundException(string source,int id):base($"can't find from {source} with id:{id}")
        {
            
        }
    }
}
