using PersonsListPractice.Infra;
using System;
using System.Threading;
using System.Windows.Input;

namespace PersonsListPractice.ViewModel
{
    public class PersonVM : ViewModelBase
    {
        // class members
        private int index;

        public int Index
        {
            get { return index; }
            set
            {
                index = value;
                OnPropertyChanged();
            }
        }

        private int id;

        public int Id
        {
            get { return id; }
            private set
            {
                id = value;
                OnPropertyChanged();
            }
        }

        public string Name { get; set; }

        // Tools-class members
        private Random rand;

        private Boolean isSelected;

        public Boolean IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                OnPropertyChanged();
            }
        }

        //commands
        public ICommand RemovePersonCommand { get; private set; }

        //Events and Actions
        public event EventHandler DeletePersonEvent;

        //ctor
        public PersonVM(string name, int id)
        {
            if (id == 0)
            {
                rand = new Random();
                Thread.Sleep(100);
                Id = rand.Next(1000, 10000);
            }
            else
            {
                Id = id;
            }
            Name = name;
            this.RemovePersonCommand = new RelayCommand(this.PersonToRemove);
        }

        //methods

        public void PersonToRemove()
        {
            DeletePersonEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}