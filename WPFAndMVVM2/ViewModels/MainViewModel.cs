using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using WPFAndMVVM2.Models;

namespace WPFAndMVVM2.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private PersonRepository personRepo = new PersonRepository();

        public event PropertyChangedEventHandler PropertyChanged;

        // Implement the rest of this MainViewModel class below to 
        // establish the foundation for data binding !

        public ObservableCollection<PersonViewModel> PersonsVM { get; set; }

        private PersonViewModel _currentPerson;
        public PersonViewModel CurrentPerson {
            get { return _currentPerson; }
            set { _currentPerson = value; OnPropertyChanged("CurrentPerson"); }
            }
       
        

        public void OnPropertyChanged(string propertyName) {
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
      
            if(propertyChanged != null) {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public MainViewModel() {
            PersonsVM = new ObservableCollection<PersonViewModel>();
        
            foreach(Person person in personRepo.GetAll()) {
                PersonViewModel personView = new PersonViewModel(person);
                PersonsVM.Add(personView);
            }
        }
       public void AddDefaultPerson() {
           
            Person person = personRepo.Add("Specify FirstName", "Specify LastName", 0 , "Specify Phone");
            
          // PersonsVM.Add(new PersonViewModel(person)); orhans eksempel
           
            PersonViewModel personViewModel = new PersonViewModel(person);
            PersonsVM.Add(personViewModel);
            CurrentPerson = personViewModel;
            

        }
        public void DeleteSelectedPerson() {
            CurrentPerson.DeletePerson(personRepo);
            PersonsVM.Remove(CurrentPerson);
          
        }
    }
}
