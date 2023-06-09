﻿using Election_System.Configurations;
using Election_System.Generics;
using Election_System.Models;
using Election_System.Enumerations;

namespace Election_System.Repositories
{
    public class ProcessRepository : GenericRepository<Process>, IProcessRepository
    {
        public ProcessRepository(DataContext dataContext) : base(dataContext)
        {

        }

        public List<Process> GetActives()
        {
            return GetDataContext().processes.
                Where(p => DateTime.Compare(p.StartDate, DateTime.Now.Date) < 0 && DateTime.Compare(p.EndDate, DateTime.Now.Date) > 0).
                ToList();
        }

        public Process GetStartedDepartmentCandidacy(ProcessType process)
        {
            return GetDataContext().processes.
                Where(p => DateTime.Compare(p.EndDate, DateTime.Now) < 0 && p.ProcessType == process).
                FirstOrDefault();
        }

        public Process GetStartingDepartmentCandidacy(ProcessType process)
        {
            return GetDataContext().processes.
                Where(p => DateTime.Compare(p.StartDate, DateTime.Now) < 0 && DateTime.Compare(p.EndDate, DateTime.Now.Date) > 0 && p.ProcessType == process).
                FirstOrDefault();
        }

        public Process GetWillStartDepartmentCandidacy(ProcessType process)
        {
            foreach (var p in GetDataContext().processes)
            {
                int a = DateTime.Compare(p.StartDate, DateTime.Now);
            }
            return GetDataContext().processes.
                Where(p => (DateTime.Compare(p.StartDate, DateTime.Now) > 0) && (p.ProcessType == process)).
                FirstOrDefault();
        }

    }

}
