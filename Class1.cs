using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    public class HistoryExpViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<HistoryModel> his;
        private DatabaseService db;
        private string _sample;

        public HistoryExpViewModel()
        {
            his = new ObservableCollection<HistoryModel>();
            db = new DatabaseService();
            _ = initData();
        }

        public ObservableCollection<HistoryModel> historyOut
        {
            get => his;
        }

        public async void saveToHistory(String item)
        {
            HistoryModel historyModel = new HistoryModel();
            historyModel.expression = item;
            await db.SaveItemAsync(historyModel);
            await initData();
            OnPropertyChanged("historyOut");
            OnPropertyChanged();
        }
        public async Task initData()
        {
            var res = await db.GetItemsAsync();
            his = new ObservableCollection<HistoryModel>();
            res.ForEach(his.Add);
        }

        public async Task clearHistory()
        {
            await db.DeleteAllAsync();
            await initData();
            OnPropertyChanged("historyOut");
            OnPropertyChanged();
        }

        public void OnPropertyChanged([CallerMemberName] string name = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    }
}
