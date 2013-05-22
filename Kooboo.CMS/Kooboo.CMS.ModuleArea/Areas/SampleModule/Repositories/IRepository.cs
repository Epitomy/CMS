﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kooboo.CMS.ModuleArea.Repositories
{
    public interface IRepository<T>
        where T : class
    {
        T ById(int id);

        IQueryable<T> All();

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
