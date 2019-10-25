using MaTrack.Core.Dtos;
using MaTrack.Core.Entities;
using MaTrack.Shared.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MaTrack.Shared.Dialogs
{
    public sealed partial class SetStageDialog : ContentDialog
    {
        private IHttpClientService _httpClientService;
        private List<StageEntity> _stages;
        private AdminEntity _adminEntity;
        public SetStageDialog()
        {
            this.InitializeComponent();
        }
        public SetStageDialog(UserAuthDto userAuthDto,AdminEntity adminEntity)
        {
            this.InitializeComponent();
            _httpClientService = new HttpClientService();
            _httpClientService.SetAuthorizationHeaderToken(_httpClientService.HttpClient, userAuthDto.Token);
            _adminEntity = adminEntity;
        }
        public async Task InitializeAsync()
        {
            var stages = JsonConvert.DeserializeObject<List<StageEntity>>(await _httpClientService.GetAsync("Stages/all"));
            ComboStages.ItemsSource = stages;
        }
        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var selectedStage = ComboStages.SelectedItem as StageEntity;
            _adminEntity.StageId = selectedStage.Id;
            await _httpClientService.PutAsync(_adminEntity, "Admin/update");
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
