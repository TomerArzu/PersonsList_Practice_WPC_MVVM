using PersonsListPractice.Infra;
using PersonsListPractice.View;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;


namespace PersonsListPractice.ViewModel
{
    public class MainWindowVM : ViewModelBase
    {
        //Members

        private ObservableCollection<PersonVM> _Persons;

        public ObservableCollection<PersonVM> Persons
        {
            get
            {
                return _Persons;
            }
            set
            {
                _Persons = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<PersonVM> _FilteredPersonsList;

        public ObservableCollection<PersonVM> FilteredPersonsList
        {
            get
            {
                return _FilteredPersonsList;
            }
            set
            {
                _FilteredPersonsList = value;
                OnPropertyChanged();
            }
        }

        private string _SearchedString;

        public string SearchedString
        {
            get
            {
                return _SearchedString;
            }
            set
            {
                if (string.Equals(SearchedString, value)) return;
                _SearchedString = value;
                OnPropertyChanged();
                UpdatePersonlist(value);
            }
        }

        public NewPersonWin newPerson;

        //Events and Actions

        //commands
        public ICommand AddPersonCommand { get; private set; }

        public ICommand EditModeCommand { get; private set; } //can del
        public ICommand DeleteSelectedCommand { get; private set; }

        //ctor
        public MainWindowVM()
        {
            PersonVM p1 = new PersonVM("Tomer", 0);
            PersonVM p2 = new PersonVM("Ofer", 0);
            PersonVM p3 = new PersonVM("Naor", 0);
            PersonVM p4 = new PersonVM("Lior", 0);
            //if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject())) //If this is true it means that we are currently not running the application and we're currently only designing the application.
            //{                                                                               // If this is false then all of these won't be executed.
            //    Persons = new ObservableCollection<PersonVM>
            //    {
            //        p1
            //    };
            //}

            Persons = new ObservableCollection<PersonVM>
            {
                p1,
                p2,
                p3,
                p4
            };
            int i = 1;
            //filterdPersonsList = CollectionViewSource.GetDefaultView(Persons); // need to find put how to do this with "where" and NOT ICollectionView
            FilteredPersonsList = Persons;
            foreach (var p in Persons)
            {
                p.Index = i++;
                p.DeletePersonEvent += P_DeletePersonEvent;
            }
            this.AddPersonCommand = new RelayCommand(this.AddPerson);
            this.EditModeCommand = new RelayCommand(this.EnterOrExitEditMode);
            this.DeleteSelectedCommand = new RelayCommand(this.DeleteSelected);
        }

        //methods
        private void DeleteSelected()
        {
            for (int i = 0; i < Persons.Count; i++)
            {
                if (Persons[i].IsSelected)
                {
                    P_DeletePersonEvent(Persons[i--], EventArgs.Empty);
                }
            }
        }

        private void EnterOrExitEditMode()
        {
            foreach (var p in Persons)
            {
                p.IsSelected = false;
            }
        }

        private void P_DeletePersonEvent(object sender, EventArgs e)
        {
            PersonVM p = sender as PersonVM;
            int PersonToDelIndex = Persons.IndexOf(p);
            int indexToassign = p.Index;
            for (int i = PersonToDelIndex + 1; i < Persons.Count; i++)
            {
                Persons[i].Index = indexToassign++;
            }
            Persons.Remove(p);
            UpdatePersonlist(SearchedString);
        }

        public void AddPerson()
        {
            newPerson = new NewPersonWin();
            var addPersonVm = new AddPersonVM();
            newPerson.DataContext = addPersonVm;
            addPersonVm.CloseWindowEvent += CloseAddNewPersonWindow;
            addPersonVm.PersonAddedCallback += PersonAdded; // at "addPersonVm" (that is an instance of AddPersonVM) there is a delegate called PersonAddedCallback, we register the function PersonAdded to the delegate,
                                                            // now - each time that we will use the delegate as function we can pass to it a parameter and the registed function will be called
                                                            //using events
                                                            //addPersonVm.SomeEvent += AddPersonVm_SomeEvent; // in the case of event, we register to the event "SomeEvent" that in "addPersonVm" (that is an instance of AddPersonVM) the function "AddPersonVm_SomeEvent",
                                                            //now each time the event will happend the registerd function will be execute
            newPerson.Show();
        }

        private void CloseAddNewPersonWindow(object sender, EventArgs e)
        {
            if (newPerson != null)
            {
                newPerson.Close();
            }
        }

        //private void AddPersonVm_SomeEvent(object sender, EventArgs e) //the function of the delegate
        //{
        //    throw new NotImplementedException();
        //}
        private void PersonAdded(PersonVM p) //function to be registerd in PersonAddedCallback
        {
            p.DeletePersonEvent += P_DeletePersonEvent;
            p.Index = Persons.Count + 1;
            Persons.Add(p);
            UpdatePersonlist(SearchedString);
        }

        private void UpdatePersonlist(string SearchedValue)
        {
            if (SearchedValue != null)
                //filterdPersonsList.Filter = p => ((PersonVM)p).Id.ToString().Contains(SearchedValue) || ((PersonVM)p).Name.ToLower().Contains(SearchedValue.ToLower());
                FilteredPersonsList = new ObservableCollection<PersonVM>(this.Persons.MyWhere(p => p.Id.ToString().Contains(SearchedValue) || p.Name.ToLower().Contains(SearchedValue.ToLower())));
            //FilteredPersonsList = new ObservableCollection<PersonVM>(this.Persons.Where(p => p.Id.ToString().Contains(SearchedValue) || p.Name.ToLower().Contains(SearchedValue.ToLower())));
        }
    }
}