using BeautySalon.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BeautySalon.Pages
{
    /// <summary>
    /// Логика взаимодействия для serviceview.xaml
    /// </summary>
    public partial class serviceview : Window
    {
        Client client;
        public serviceview(Client cl)
        {
            InitializeComponent();
            client = cl;
            try
            {
                Load();
                if (client.PhotoPath != null)
                {
              
                    string imagePath = "C:\\Users\\artem\\source\\repos\\BeautySalon\\BeautySalon\\Resources\\" + client.PhotoPath.Trim();
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.UriSource = new Uri(imagePath);
                    bitmapImage.DecodePixelWidth = 100; // Установите желаемую ширину миниатюры (в пикселях)
                    bitmapImage.EndInit();

                    // Установите BitmapImage в элемент Image на вашей форме (назовем его MyImage)
                    AgentImage.Source = bitmapImage;
                }
                FirstName.Text = "Имя " + client.FirstName;
                LastName.Text = "Фамилия " + client.LastName;
                if (client.Patronymic != "" || (client.Patronymic != null))
                {
                    MiddleName.Text = "Отчество " + client.Patronymic;
                }
                else
                {
                    MiddleName.Text = "Отчество отсутствует";
                }


            }
            catch
            {

            }


        }
        public void Load()
        {
            DataView.ItemsSource = barhatnie_brovkiEntities.GetContext().ClientService.Where(Client => Client.ClientID == client.ID).ToList();
            CountVisit.Text = "Кол-во посещений " + barhatnie_brovkiEntities.GetContext().ClientService.Where(Client => Client.ClientID == client.ID).Count().ToString();
        }

        private void DeleteService_Click(object sender, RoutedEventArgs e)
        {
            if (DataView.SelectedItem != null)
            {
                try
                {
                    if (MessageBox.Show("Вы точно хотите удалить эту услугу!", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                    {
                        var SelectedService = DataView.SelectedItem as ClientService;
                        barhatnie_brovkiEntities.GetContext().ClientService.Remove(SelectedService);
                        barhatnie_brovkiEntities.GetContext().SaveChanges();
                        MessageBox.Show("Успешно!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                        Load();
                    }
                }
                catch
                {
                    MessageBox.Show("Неизсветная ошибка!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                }


            }

            else
            {
                MessageBox.Show("Выделите услугу и нажмите на кнопку!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void EditService_Click(object sender, RoutedEventArgs e)
        {
            Load();
        }
    }
}