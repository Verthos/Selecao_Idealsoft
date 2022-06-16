using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows;
using System.Linq;
using Selecao.Application.Services.PersonValidator;
using FrontEndWPF.ViewModels;

namespace FrontEndWPF
{

    public partial class MainWindow : Window
    {
        HttpClient client = new HttpClient();
        public ObservableCollection<PersonViewModel> plist = new ObservableCollection<PersonViewModel>();
        IPersonPropertiesValidator validator = new PersonPropertiesValidator();

        public MainWindow()
        {
            client.BaseAddress = new Uri("https://localhost:7184/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            InitializeComponent();
            PeopleList.ItemsSource = plist;
        }

        private void Consultar_Click(object sender, RoutedEventArgs e)
        {
            GetPeople();
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            PersonViewModel newPerson = new PersonViewModel(FirstNameText.Text, LastNameText.Text, PhoneNameText.Text);
            CreatePerson(newPerson);
            
        }

        private async void GetPeople()
        {
            plist.Clear();
            var response = await client.GetStringAsync("people");
            var people = JsonConvert.DeserializeObject<List<PersonViewModel>>(response);
            
            foreach (var person in people)
            {
                plist.Add(person);
            }
        }
        private void Deletar_Click(object sender, RoutedEventArgs e)
        {
            DeletePerson();
        }
        private void Editar_Click(object sender, RoutedEventArgs e)
        {
            var selectedList = plist.Where(x => x.IsSelected == true).ToList();
            PersonViewModel person = new PersonViewModel(FirstNameText.Text, LastNameText.Text, PhoneNameText.Text);
            UpdatePerson(person);
        }

        private async void CreatePerson(PersonViewModel person)
        {
            if (!validator.PersonIsValid(person.Name, person.LastName, person.PhoneNumber))
            {
                MessageBox.Show($"Dados inválidos: Use nomes e sobrenomes com mais de três letras e telefone no formato 99999-9999");
                return;
            }
            await client.PostAsJsonAsync("person", person);
            MessageBox.Show($"Adicionado nome {person.Name} à lista");
        }

        private async void UpdatePerson(PersonViewModel person)
        {
            var selectedList = plist.Where(x => x.IsSelected == true).ToList();
            if (selectedList.Count != 1)
            {
                MessageBox.Show("Selecione apenas o item que deseja editar.");
                return;
            }
            if (!validator.PersonIsValid(person.Name, person.LastName, person.PhoneNumber))
            {
                MessageBox.Show($"Dados inválidos: Use nomes e sobrenomes com mais de três letras e telefone no formato 99999-9999");
            }
            person.Id = selectedList[0].Id;
            await client.PutAsJsonAsync($"person", person);
        }
        private async void DeletePerson()
        {
            
            var selectedList = plist.Where(x => x.IsSelected == true).ToList();
            if (selectedList.Count < 1)
            {
                MessageBox.Show("Selecione ao menos um item.");
                return;
            }
            foreach (PersonViewModel person in selectedList)
            {
                await client.DeleteAsync($"person/{person.Id}");
            }
            plist.Clear();
            GetPeople();
        }

        
    }
}
