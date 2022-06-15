using FrontEndWPF.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FrontEndWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HttpClient client = new HttpClient();

        public MainWindow()
        {
            
            client.BaseAddress = new Uri("https://localhost:7184/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            PersonViewModel newPerson = new PersonViewModel(FirstNameText.Text, LastNameText.Text, PhoneNameText.Text);
            CreatePerson(newPerson);
            MessageBox.Show($"Adicionado nome: {FirstNameText.Text} à lista");
        }

        private async void GetPeople()
        {
            var response = await client.GetStringAsync("people");
            var people = JsonConvert.DeserializeObject<List<PersonViewModel>>(response);

        }

        private async void CreatePerson(PersonViewModel person)
        {
            await client.PostAsJsonAsync("person", person);
        }

        private async void UpdatePerson(PersonViewModel person)
        {
            await client.PutAsJsonAsync($"person/{person.Id}", person);
        }
        private async void DeletePerson(int id)
        {
            await client.DeleteAsync($"person/{id}");
        }
    }
}
