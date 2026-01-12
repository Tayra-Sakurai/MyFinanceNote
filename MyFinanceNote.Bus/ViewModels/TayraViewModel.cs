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
        private readonly ChimpanzeeContext _context = new();

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(DeleteCommand))]
        private Tayra tayra;

        [ObservableProperty]
        private DateTimeOffset date = new(DateTime.Now.Date);

        [ObservableProperty]
        private TimeSpan time = DateTime.Now.TimeOfDay;

        [ObservableProperty]
        private string? event1 = string.Empty;

        [ObservableProperty]
        private decimal cash = 0;

        [ObservableProperty]
        private decimal icoca = 0;

        [ObservableProperty]
        private decimal coop = 0;

        public TayraViewModel ()
        {
            DateTime dateTime = Date.Date;
            dateTime = dateTime.Add(Time);
            this.Tayra = new Tayra()
            {
                Cash = Cash,
                Coop = Coop,
                Date = dateTime,
                Event = Event1,
                Icoca = Icoca
            };
        }

        public void InitializeForExistingValue(Tayra tayra)
        {
            this.Tayra = tayra;
            Date = Tayra.Date.Date;
            Time = Tayra.Date.TimeOfDay;
            Event1 = Tayra.Event;
            Cash = Tayra.Cash;
            Icoca = Tayra.Icoca;
            Coop = Tayra.Coop;
        }

        [RelayCommand]
        public void Save()
        {
            this.Tayra.Event = Event1;
            this.Tayra.Icoca = Icoca;
            this.Tayra.Coop = Coop;
            this.Tayra.Cash = Cash;
            DateTime datetime = Date.Date;
            this.Tayra.Date = datetime.Add(Time);
        }

        [RelayCommand(CanExecute = nameof(CanDelete))]
        public async Task Delete()
        {
            _context.Remove(this.Tayra);
        }

        private bool CanDelete()
        {
            return this.Tayra is not null;
        }
    }
}
