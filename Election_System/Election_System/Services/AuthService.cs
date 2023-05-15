﻿using Election_System.DTO.Requests;
using Election_System.DTO.Responses;
using Election_System.Repositories;

namespace Election_System.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAdministrationRepository _administrationRepository;
        private readonly IStudentRepository _studentRepository;
        public AuthService(IAdministrationRepository administrationRepository, IStudentRepository studentRepository)
        {
            _administrationRepository = administrationRepository;
            _studentRepository = studentRepository;
        }
        public LoginResponse Login(LoginRequest loginRequest)
        {

            foreach (var admin in _administrationRepository.GetAll())
            {
                if (admin.Username.Equals(loginRequest.UserName) && admin.Password.Equals(loginRequest.Password))
                {
                    return new LoginResponse(admin.Id);
                }
            }

            foreach (var student in _studentRepository.GetAll())
            {
                if (student.Username.Equals(loginRequest.UserName) && student.Password.Equals(loginRequest.Password))
                {
                    return new LoginResponse(student.Id);
                }
            }

            return new LoginResponse(0);

        }

    }

}
