﻿using Choice.Domain.Models;
using Choice.Services.ApiServices;
using Choice.Services.AuthenticationServices;
using Choice.Services.CompanyApiService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Choice.Validators
{
    public class RegisterCompanyInputValidator : IValidator
    {
        private readonly ICompanyApiService _companyService;
        private readonly RegisterCompanyInput _input;

        public RegisterCompanyInputValidator(RegisterCompanyInput input, ICompanyApiService companyService)
        {
            _input = input;
            _companyService = companyService;

            Fails = new Dictionary<string, string>();
        }

        public Dictionary<string, string> Fails { get; }

        public async Task<bool> Validate()
        {
            Fails.Clear();

            if (!Equals(_input.Password, _input.PasswordConfirmtion))
                Fails.Add("Ошибка", "Ваши пароли не совпадают");

            IList<Company> companies = await _companyService.GetAll();

            Company companyGotByEmail = companies.FirstOrDefault(c => c.Email == _input.Email);

            if (companyGotByEmail != null)
                Fails.Add("Ошибка", "Комания с таким e-mail уже зарегестрирована");

            return Fails.Count == 0;
        }
    }
}