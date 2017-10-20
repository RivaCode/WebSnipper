﻿using Domain.Business;

namespace WebSnipper.UI.Presentation.SiteCards
{
    public class CardViewModel
    {
        private readonly SlimSiteModel _model;
        public string Name => _model.Name;

        public CardViewModel(SlimSiteModel model) => _model = model;
    }
}