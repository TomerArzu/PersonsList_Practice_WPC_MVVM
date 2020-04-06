using PersonsListPractice.Infra;
using System;
using System.Windows.Input;

namespace PersonsListPractice.ViewModel
{
    public class AddPersonVM : ViewModelBase
    {
        //Commands
        public ICommand AddPersonCommand { get; private set; }

        public ICommand CloseWin { get; private set; }

        //Members
        private string personName;

        public string PersonName
        {
            get { return personName; }
            set
            {
                personName = value;
                OnPropertyChanged("PersonName");
            }
        }

        private int? personId;

        public int? PersonId
        {
            get { return personId; }
            set
            {
                personId = value;
                OnPropertyChanged("PersonId");
            }
        }

        // Events
        //public event EventHandler SomeEvent; // passing data using events
        public Action<PersonVM> PersonAddedCallback; // passing data using delegate

        public event EventHandler CloseWindowEvent;

        // Ctor
        public AddPersonVM()
        {
            AddPersonCommand = new RelayCommand(AddPerson);
            CloseWin = new RelayCommand(CloseWindow);
        }

        // methods
        public void CloseWindow()
        {
            if (CloseWindowEvent != null)
            {
                CloseWindowEvent(this, EventArgs.Empty);
            }
        }

        public void AddPerson()
        {
            PersonVM NewP = new PersonVM(PersonName, PersonId.Value);
            this.PersonId = null;
            PersonName = string.Empty;
            if (PersonAddedCallback != null) // we are checking if there is some registerd function in the the event, if there is- we call this function by the delegate and pass it parameter
            {
                PersonAddedCallback(NewP); // calling the function using the event
            }
            //solotion to pass data using events
            //if(SomeEvent != null)
            //{
            //    SomeEvent(this, EventArgs.Empty);
            //}
        }
    }
}