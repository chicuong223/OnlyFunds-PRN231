﻿using OnlyFundsAPI.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface INotificationRepository
    {
        IQueryable<Notification> GetList();
        Task<Notification> GetByID(int id);
        Task<Notification> Create(PostTag tag);
        Task<Notification> Update(PostTag tag);
        Task Delete(int id);
    }
}
