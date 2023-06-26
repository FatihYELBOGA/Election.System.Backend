﻿using Election_System.Configurations;
using Election_System.Generics;
using Election_System.Models;

namespace Election_System.Repositories
{
    public class AnnouncementRepository : GenericRepository<Announcement>, IAnnouncementRepository
    {   
        public AnnouncementRepository(DataContext dataContext) : base(dataContext)
        {

        }

        public List<Announcement> GetActives()
        {
            return GetDataContext().announcements.
                Where(a => DateTime.Compare(a.StartDate,DateTime.Now)<0 && DateTime.Compare(a.EndDate,DateTime.Now)>0).
                ToList();
        }

    }
}
