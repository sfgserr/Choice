using Avalonia.Controls;
using Avalonia.Controls.Templates;
using ClientApp.Services.Mappers;
using ClientApp.ViewModels;
using ClientApp.Views;
using System;

namespace ClientApp
{
    public class ViewMapperDataTemplate : IDataTemplate
    {
        private readonly ViewModelMapper _mapper;

        public ViewMapperDataTemplate(ViewModelMapper mapper)
        {
            _mapper = mapper;
        }

        public Control? Build(object? param) => 
            _mapper.Map((ViewModelBase)param);

        public bool Match(object? data)
        {
            return data is ViewModelBase;
        }
    }
}
