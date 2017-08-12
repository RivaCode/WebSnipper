﻿using WebSnipper.UI.Business;
using WebSnipper.UI.Business.Queries;
using WebSnipper.UI.Core;

namespace WebSnipper.UI.Presentation.ViewModels
{
    public class UrlViewModel : NotifyObject
    {
        private string _description;
        private string _url;

        public string Description
        {
            get => _description;
            set => this.SetAndRaise(ref _description, value, NotifyChanged());
        }

        public string Url
        {
            get => _url;
            set => this.SetAndRaise(ref _url, value, NotifyChanged());
        }

        public UrlViewModel(SiteModel model)
        {
            _description = model.Description;
            _url = model.Url;
        }
    }
}