using System;
using WebSnipper.UI.Business.Commands;
using WebSnipper.UI.Core;

namespace WebSnipper.UI.Presentation.ViewModels
{
    public class NewSiteInfoViewModel : BusyViewModel
    {
        public string Url { get; set; }
        public string Description { get; set; }
        public IReactiveCommand ApplyCmd { get; }


        public NewSiteInfoViewModel(ICreateSiteCommand createSiteCmd)
        {
            ApplyCmd = Command.Create(
                async () =>
                {
                    try
                    {
                        using (StartBusy())
                        {
                            await createSiteCmd.Execute(new CreateSiteModel(Url, Description));
                        }
                    }
                    catch (Exception e)
                    {
                    }
                });
        }
    }
}
