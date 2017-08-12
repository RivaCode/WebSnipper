using System;
using System.Collections;
using System.ComponentModel;
using WebSnipper.UI.Business.Commands;

namespace WebSnipper.UI.Presentation.ViewModels
{
    public class NewSiteInfoViewModel : BusyViewModel, INotifyDataErrorInfo
    {
        private Result _createResult;
        public string Url { get; set; }
        public string Description { get; set; }
        public IReactiveCommand ApplyCmd { get; }


        public NewSiteInfoViewModel(ICreateSiteCommand createSiteCmd)
        {
            _createResult = Result.Ok();
            ApplyCmd = Command.Create(
                async () =>
                {
                    _createResult = await this.SafeInvokeAsync(
                        () => createSiteCmd.Execute(new CreateSiteModel(Url, Description)));
                    ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(null));
                });
        }

        #region INotifyDataErrorInfo members

        public IEnumerable GetErrors(string propertyName)
        {
            yield return _createResult.Error;
        }

        public bool HasErrors => _createResult.IsFailure;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        
        #endregion
    }
}
