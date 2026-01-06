using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyFinanceNote.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinanceNote.ViewModels
{
    public partial class TayraViewModel : ObservableObject
    {
        private readonly ChimpanzeeContext _context;

        public Tayra tayra;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        [NotifyCanExecuteChangedFor(nameof(DeleteCommand))]
        private DateTimeOffset date = new(DateTime.Now.Date);

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        [NotifyCanExecuteChangedFor(nameof(DeleteCommand))]
        private TimeSpan time = DateTime.Now.TimeOfDay;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        [NotifyCanExecuteChangedFor(nameof(DeleteCommand))]
        private string? event1 = string.Empty;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        [NotifyCanExecuteChangedFor(nameof(DeleteCommand))]
        private decimal cash = 0;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        [NotifyCanExecuteChangedFor(nameof(DeleteCommand))]
        private decimal icoca = 0;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        [NotifyCanExecuteChangedFor(nameof(DeleteCommand))]
        private decimal coop = 0;

        public TayraViewModel (ChimpanzeeContext chimpanzeeContext)
        {
            _context = chimpanzeeContext;
            tayra = new Tayra();
        }

        public void InitializeForExistingValue(Tayra tayra)
        {
            this.tayra = tayra;
            Date = new DateTimeOffset(tayra.Date.Date);
            Time = tayra.Date.TimeOfDay;
            Event1 = tayra.Event;
            Cash = tayra.Cash;
            Icoca = tayra.Icoca;
            Coop = tayra.Coop;
        }

        [RelayCommand(CanExecute = nameof(CanSave))]
        public async Task SaveAsync()
        {
            tayra.Event = Event1;
            tayra.Icoca = Icoca;
            tayra.Coop = Coop;
            tayra.Cash = Cash;
            DateTime datetime = Date.Date;
            tayra.Date = datetime.Add(Time);

            await _context.SaveChangesAsync();
        }

        private bool CanSave ()
        {
            Debug.WriteLine(Event1);
            return tayra != null;
        }

        [RelayCommand(CanExecute = nameof(CanDelete))]
        public async Task Delete()
        {
            _context.Remove(tayra);
            await _context.SaveChangesAsync();
        }

        private bool CanDelete()
        {
            return tayra is not null;
        }
    }
}
