﻿using Election_System.Configurations;
using Election_System.DTO.Responses;
using Election_System.Enumerations;
using Election_System.Generics;
using Election_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Election_System.Repositories
{
    public class ElectionRepository : GenericRepository<ElectionResult>, IElectionRepository
    {
        public ElectionRepository(DataContext dataContext) : base(dataContext) 
        {

        }

        public List<ElectionResultResponse> GetElectionResultsForDepartmentRepresentative(int departmentId)
        {
            var query = GetDataContext().electionResults.
                Include(es => es.CandidateStudent).
                Where(es => es.CandidateStudent.DepartmentId == departmentId).
                GroupBy(es => es.CandidateStudentId).
                Select(g => new { candidateStudentId = g.Key, NumberOfVotes = g.Count() });

            List<ElectionResultResponse> electionResults = new List<ElectionResultResponse>();
            foreach (var q in query)
            {
                electionResults.Add(new ElectionResultResponse()
                {
                    CandidateStudentId = (int)q.candidateStudentId,
                    NumberOfVotes = q.NumberOfVotes
                });
            }

            return electionResults;
        }

        public ElectionResult GetElectionResultForDepartmentRepresentative(int voterId)
        {
            try
            {
                return GetDataContext().electionResults.
                Where(es => es.VoterStudentId == voterId && es.ProcessType == ProcessType.DEPARTMENT_REPRESENTATIVE).
                Include(es => es.CandidateStudent).
                FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int RemoveAll()
        {
            return GetDataContext().Database.ExecuteSql($"DELETE FROM electionResults");
        }

    }

}
