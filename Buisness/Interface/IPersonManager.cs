﻿using DTO;

namespace BLL
{
    public interface IPersonManager
    {
        Person GetPersonByLogin(string login, string password);
    }
}