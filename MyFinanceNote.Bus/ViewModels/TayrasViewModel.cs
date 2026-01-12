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

        public TayrasViewModel()
        {
            Tayras = new ObservableCollection<Tayra>();
        }

        [RelayCommand]
        public async Task LoadAsync()
        {
            Tayras.Clear();
            // The items of the table.
            await foreach (var t in _context.Tayras.AsAsyncEnumerable())
            {
                Tayras.Add(t);
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
