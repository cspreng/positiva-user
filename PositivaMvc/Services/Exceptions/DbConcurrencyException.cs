﻿using System;


namespace PositivaMvc.Services.Exceptions
{
    public class DbConcurrencyException:ApplicationException
    {
        public DbConcurrencyException(string message):base(message)
        {

        }
    }
}
