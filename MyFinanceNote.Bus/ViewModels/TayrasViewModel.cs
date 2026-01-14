using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Dispatching;
using MyFinanceNote.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;

namespace MyFinanceNote.ViewModels
{
    public partial class TayrasViewModel : ObservableObject
    {
        private readonly ChimpanzeeContext _context = new();

        [ObservableProperty]
        private ObservableCollection<Tayra> tayras;

        [ObservableProperty]
        private double cashTotal = 0;

        [ObservableProperty]
        private double icocaTotal = 0;

        [ObservableProperty]
        private double coopTotal = 0;

        public TayrasViewModel()
        {
            Tayras = new ObservableCollection<Tayra>();
        }

        [RelayCommand]
        public async Task LoadAsync()
        {
            Tayras.Clear();
            CashTotal = 0;
            IcocaTotal = 0;
            CoopTotal = 0;
            // The items of the table.
            await foreach (var t in _context.Tayras.AsAsyncEnumerable())
            {
                Tayras.Add(t);
                CashTotal += (double)t.Cash;
                IcocaTotal += (double)t.Icoca;
                CoopTotal += (double)t.Coop;
            }
        }

        [RelayCommand]
        public async Task AddAsync()
        {
            var element = new Tayra { Date = DateTime.Now, Event = string.Empty };
            _context.Add(element);
            await _context.SaveChangesAsync();

            Tayras.Add(element); // UI スレッドで呼ばれる想定
        }

        [RelayCommand]
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
