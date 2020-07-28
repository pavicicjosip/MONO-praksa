﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMDb.Model;


namespace TMDb.Repository.Common
{
    public interface IGenreRepository
    {
        Task<List<Genre>> ReturnAllGenresAsync();
    }
}