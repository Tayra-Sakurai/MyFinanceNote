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
        private readonly DispatcherQueue? _dispatcher;

        [ObservableProperty]
        private ObservableCollection<Tayra> tayras;

        public TayrasViewModel()
        {
            // MainWindow のコンストラクタ内で生成される想定のため、
            // ここで UI スレッドの DispatcherQueue を取得して保持します。
            _dispatcher = DispatcherQueue.GetForCurrentThread();
            Tayras = new ObservableCollection<Tayra>();
        }

        [RelayCommand]
        public async Task LoadAsync()
        {
            // 1) DB から非同期に全件をローカルリストに取得（非 UI スレッドでも安全）
            var items = await _context.Tayras.AsNoTracking().ToListAsync().ConfigureAwait(false);

            // 2) UI スレッドでコレクションを更新
            if (_dispatcher != null)
            {
                // TryEnqueue で UI スレッドに処理を投げる
                _dispatcher.TryEnqueue(() =>
                {
                    Tayras.Clear();
                    foreach (var t in items)
                    {
                        Tayras.Add(t);
                    }
                });
            }
            else
            {
                // Dispatcher が取れなかったフォールバック（呼び出し元が UI スレッドである場合のみ安全）
                Tayras.Clear();
                foreach (var t in items)
                {
                    Tayras.Add(t);
                }
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
            await _context.SaveChangesAsync().ConfigureAwait(false);

            if (_dispatcher != null)
            {
                _dispatcher.TryEnqueue(() => Tayras.Add(element));
            }
            else
            {
                Tayras.Add(element);
            }
        }
    }
}
