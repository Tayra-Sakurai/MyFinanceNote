using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Dispatching;
using MyFinanceNote.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;

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
            _context.SavedChanges += _context_SavedChanges;
        }

        /// <summary>
        /// Update the list as the changes are saved.
        /// </summary>
        /// <param name="sender">
        /// The sender of the event. Must be <see cref="_context"/>.
        /// </param>
        /// <param name="e">
        /// The event arguments.
        /// </param>
        private async void _context_SavedChanges(object sender, SavedChangesEventArgs e)
        {
            // Update the property.
            Tayras.Clear();
            IcocaTotal = 0;
            CashTotal = 0;
            CoopTotal = 0;
            List<Tayra> tayrasList = await _context.Tayras.ToListAsync();
            tayrasList.Sort();
            foreach (var tayra in tayrasList)
            {
                Tayras.Add(tayra);
                CashTotal += (double)tayra.Cash;
                IcocaTotal += (double)tayra.Icoca;
                CoopTotal += (double)tayra.Coop;
            }
        }

        [RelayCommand]
        public async Task LoadAsync()
        {
            Tayras.Clear();
            CashTotal = 0;
            IcocaTotal = 0;
            CoopTotal = 0;
            // The items of the table.
            List<Tayra> tayrasList = await _context.Tayras.ToListAsync();
            tayrasList.Sort();
            foreach (Tayra tayra in tayrasList)
            {
                Tayras.Add(tayra);
                CashTotal += (double)tayra.Cash;
                CoopTotal += (double)tayra.Coop;
                IcocaTotal += (double)tayra.Icoca;
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
