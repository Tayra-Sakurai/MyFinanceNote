using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
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
            await foreach (var tayra in _context.Tayras.AsAsyncEnumerable())
            {
                Tayras.Add(tayra);
            }
        }

        [RelayCommand]
        public async Task AddAsync()
        {
            Tayra element = new()
            {
                Date = DateTime.Now,
                Event = string.Empty,
                Cash = 0,
                Icoca = 0,
                Coop = 0,
            };
            _context.Add(element);
            await _context.SaveChangesAsync();
            Tayras.Add(element);
        }
    }
}
