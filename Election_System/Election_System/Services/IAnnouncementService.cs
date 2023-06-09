﻿using Election_System.DTO.Requests;
using Election_System.DTO.Responses;

namespace Election_System.Services
{
    public interface IAnnouncementService
    {
        public List<AnnouncementResponse> GetAll();
        public List<AnnouncementResponse> GetActives();
        public AnnouncementResponse GetById(int id);
        public AnnouncementResponse Add(AnnouncementRequest announcementRequest);
        public AnnouncementResponse Update(int id, AnnouncementRequest announcementRequest);
        public bool Remove(int id);

    }

}
