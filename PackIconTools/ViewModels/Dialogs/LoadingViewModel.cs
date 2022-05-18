using Caliburn.Micro;
using PackIconTools.Core;
using Action = System.Action;

namespace PackIconTools.ViewModels.Dialogs
{
    class LoadingViewModel : Screen
    {
        public LoadingViewModel(bool showProgress = false, bool canCancel = false)
        {
            CanCancelExcuted = canCancel;
            IsShowingProgress = showProgress;
        }

        private string _message;
        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                NotifyOfPropertyChange(() => Message);
            }
        }

        private bool _isShowingProgress;
        public bool IsShowingProgress
        {
            get => _isShowingProgress;
            set
            {
                _isShowingProgress = value;
                NotifyOfPropertyChange(() => IsShowingProgress);
            }
        }

        private int _progress;
        public int Progress
        {
            get => _progress;
            set
            {
                _progress = value;
                NotifyOfPropertyChange(() => Progress);
            }
        }

        public event Action CancelEvent;


        public bool CanCancelExcuted { get; set; }

        public RelayCommand CancelCommand => new RelayCommand(x => Cancel(), y => CanCancelExcuted);

        private void Cancel()
        {
            CancelEvent?.Invoke();
        }
    }
}
