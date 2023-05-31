﻿using Election_System.DTO.Responses;
using Election_System.Models;
using Election_System.Repositories;
using Election_System.Enumerations;
using Election_System.DTO.Requests;

namespace Election_System.Services
{
    public class DocumentService : IDocumentService
    {
        private IDocumentRepository _documentRepository;
        private IFileRepository _fileRepository;
        public DocumentService(IDocumentRepository documentRepository, IFileRepository fileRepository) 
        {
            _documentRepository = documentRepository;
            _fileRepository = fileRepository;
        }

        public DocumentResponse GetById(int id)
        {
            return new DocumentResponse(_documentRepository.GetById(id));
        }

        public List<DocumentResponse> GetDepartmentCandidacyDocuments()
        {
            List<DocumentResponse> documentResponses = new List<DocumentResponse>();
            foreach (var document in _documentRepository.GetDepartmentCandidacyDocuments())
            {
                documentResponses.Add(new DocumentResponse(document));
            }

            return documentResponses;
        }

        public List<DocumentResponse> GetQualificationControlDocuments()
        {
            List<DocumentResponse> documentResponses = new List<DocumentResponse>();
            foreach (var document in _documentRepository.GetQualificationControlDocuments())
            {
                documentResponses.Add(new DocumentResponse(document));
            }

            return documentResponses;
        }

        public DocumentResponse GetDeparmentCandidacyDocumentByStudentId(int studentId)
        {
            return new DocumentResponse(_documentRepository.GetDeparmentCandidacyDocumentByStudentId(studentId));
        }

        public DocumentResponse GetQualificationControlDocumentByStudentId(int studentId)
        {
            return new DocumentResponse(_documentRepository.GetQualificationControlDocumentByStudentId(studentId));
        }

        public DocumentResponse Add(DocumentRequest documentRequest)
        {
            Models.File newFile = null;
            long fileSize = documentRequest.File.Length;

            if (fileSize > 0)
            {
                using (var stream = new MemoryStream())
                {
                    documentRequest.File.CopyTo(stream);
                    var bytes = stream.ToArray();

                    newFile = new Models.File()
                    {
                        Name = documentRequest.File.Name,
                        Type = documentRequest.File.ContentType,
                        Content = bytes
                    };

                }
            }

            Document newDocument = new Document()
            {
                File = newFile,
                StudentId = documentRequest.StudentId,
                ControlStatus = ControlStatus.WAITING,
                ProcessType = documentRequest.Process,
                DocumentType = documentRequest.Document
            };

            Document document = _documentRepository.Add(newDocument);
            Models.File file = _fileRepository.GetById((int)document.FileId);
            file.DocumentId = document.Id;
            _fileRepository.Update(file);

            return new DocumentResponse(_documentRepository.GetById(document.Id));
        }

        public bool Remove(int id)
        {
            try
            {
                _fileRepository.DeleteById(_documentRepository.GetById(id).Id);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }

}
