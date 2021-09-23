using System;


namespace PositivaMvc.Services.Exceptions
{
    public class NotFoundException: ApplicationException
    {
        public NotFoundException(string message):base(message)
        {

        }
    }
}
