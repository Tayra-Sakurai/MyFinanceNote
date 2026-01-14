using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
        private ChimpanzeeContext _chimpanzeeContext = new();

        private Tayra tayra;

        [ObservableProperty]
        private DateTimeOffset date = new(DateTime.Now.Date);

        [ObservableProperty]
        private TimeSpan time = DateTime.Now.TimeOfDay;

        [ObservableProperty]
        private string event1 = string.Empty;

        [ObservableProperty]
        private double cash = 0;

        [ObservableProperty]
        private double icoca = 0;

        [ObservableProperty]
        private double coop = 0;

        public TayraViewModel ()
        {
            DateTime dateTime = Date.Date;
            dateTime = dateTime.Add(Time);
            tayra = new Tayra()
            {
                Cash = (decimal)Cash,
                Coop = (decimal)Coop,
                Date = dateTime,
                Event = Event1,
                Icoca = (decimal)Icoca
            };
        }

        public void InitializeForExistingValue(Tayra tayra)
        {
            this.tayra = tayra;
            Date = this.tayra.Date.Date;
            Time = this.tayra.Date.TimeOfDay;
            Event1 = this.tayra.Event;
            Cash = (double)this.tayra.Cash;
            Icoca = (double)this.tayra.Icoca;
            Coop = (double)this.tayra.Coop;
        }

        [RelayCommand]
        public void Save()
        {
            tayra.Event = Event1;
            tayra.Icoca = (decimal)Icoca;
            tayra.Coop = (decimal)Coop;
            tayra.Cash = (decimal)Cash;
            DateTime datetime = Date.Date;
            tayra.Date = datetime.Add(Time);
        }

        [RelayCommand]
        public void Delete()
        {
            _chimpanzeeContext.Entry(tayra).State = EntityState.Deleted;
            _chimpanzeeContext.SaveChanges();
        }
    }
}
