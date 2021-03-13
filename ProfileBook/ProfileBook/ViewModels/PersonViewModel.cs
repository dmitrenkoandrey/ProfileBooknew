using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using ProfileBook.ViewModels;
using ProfileBook.Models;

namespace ProfileBook.TreeView
{
    public class PersonViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        PersonsListViewModel lvm;
        public Person Person { get; private set; }
        public PersonViewModel()
        {
            Person = new Person();
           //Person.RegDate = DateTime.Now.ToString();
        }

        public PersonsListViewModel ListViewModel
        {
            get { return lvm; }
            set
            {
                if (lvm != value)
                {
                    lvm = value;
                    OnPropertyChanged("ListViewModel");
                }
            }
        }
        public int Id
        {
            get { return Person.Id; }
            set
            {
                if (Person.Id != value)
                {
                    Person.Id = value;
                    OnPropertyChanged("Id");
                }
            }
        }
        public string NickName
        {
            get { return Person.NickName; }
            set
            {
                if (Person.NickName != value)
                {
                    Person.NickName = value;
                    OnPropertyChanged("NickName");
                }
            }
        }
        public string Name
        {
            get { return Person.Name; }
            set
            {
                if (Person.Name != value)
                {
                    Person.Name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        
        public string ProfileImage
        {
            get { return Person.ProfileImage; }
            set
            {
                if (Person.ProfileImage != value)
                {
                    Person.ProfileImage = value;
                    OnPropertyChanged("ProfileImage");
                }
            }
        }
        public string Description
        {
            get { return Person.Description; }
            set
            {
                if (Person.Description != value)
                {
                    Person.Description = value;
                    OnPropertyChanged("Description");
                }
            }
        }
    
        public string RegDate
        {
            get { return Person.RegDate; }
            set
            {
                if (Person.RegDate != value)
                {
                    Person.RegDate = value;
                    OnPropertyChanged("RegDate");
                }
            }
        }


        public bool IsValid
        {
            get
            {
                return ((!string.IsNullOrEmpty(NickName.Trim())) ||
                    (!string.IsNullOrEmpty(Name.Trim())) ||
                    (!string.IsNullOrEmpty(Description.Trim())));
            }
        }
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

    }
}

