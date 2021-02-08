﻿using System.Collections.Generic;
using System.Threading.Tasks;
using atheneum_app.Models.View;
using Refit;

namespace atheneum_app.DataAccess.Interfaces
{
    public interface IPublisherService
    {
        [Get("/publishers")]
        Task<IEnumerable<PublisherViewModel>> GetAll();
    }
}