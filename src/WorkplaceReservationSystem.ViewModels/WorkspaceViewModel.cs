using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WorkplaceReservationSystem.Application.Services;

namespace WorkplaceReservationSystem.ViewModels
{
    public class WorkspaceViewModel
    {
        public WorkspaceViewModel(WorkspaceService workspaceService)
        {            
            InitializeCommands();
        }

        public WorkspaceViewModel(MainViewModel mainViewModel, WorkspaceService workspaceService)
        {
            MainViewModel = mainViewModel;
            WorkspaceService = workspaceService;

            InitializeCommands();
        }

        public WorkspaceService WorkspaceService {get; set; }

        public MainViewModel MainViewModel { get; }

        public ICommand ChangeSelectedWorkplaceCommand { get; set; }

        public void ExecuteChangeSelectedWorkplace(int workplaceId)
        {
            MainViewModel.SelectedWorkplaceId = workplaceId;
        }

        private void InitializeCommands()
        {
            ChangeSelectedWorkplaceCommand = new RelayCommand<int>(ExecuteChangeSelectedWorkplace);
        }
    }
}
