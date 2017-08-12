using System;
using System.Collections;
using System.ComponentModel;
using WebSnipper.UI.Business.Commands;
using WebSnipper.UI.Core;

namespace WebSnipper.UI.Presentation.ViewModels
{
    public class NewSiteInfoViewModel : BusyViewModel, INotifyDataErrorInfo
    {
        private Result _createResult = Result.Ok();
        private string _url;

        public string Url
        {
            get => _url;
            set => this.SetAndRaise(ref _url, value, NotifyChanged());
        }

        public string Description { get; set; }
        public IReactiveCommand ApplyCmd { get; }
        public bool? CanClose => _createResult?.IsSuccess;


        public NewSiteInfoViewModel(ICreateSiteCommand createSiteCmd)
        {
            ApplyCmd = Command.Create(
                this.ObserveProperty(self => self.Url)
                    .If(url => !string.IsNullOrWhiteSpace(url)),
                async () =>
                {
                    _createResult = await this.SafeInvokeAsync(
                        () => createSiteCmd.Execute(new CreateSiteModel(Url, Description)));
                    ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(Url)));
                    NotifyChanged()(new PropertyChangedEventArgs(nameof(CanClose)));
                });
        }

        #region INotifyDataErrorInfo members

        public IEnumerable GetErrors(string propertyName)
        {
            yield return propertyName.Equals(nameof(Url), StringComparison.OrdinalIgnoreCase)
                ? _createResult.Error
                : "";
        }

        public bool HasErrors => _createResult.IsFailure;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        
        #endregion
    }
}
