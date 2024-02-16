using BeautySalon.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
namespace BeautySalon.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddNewClient.xaml
    /// </summary>
    public partial class AddNewClient : Page
    {
        public string PathImage = "";
        public AddNewClient()
        {
            InitializeComponent();
            Gender.ItemsSource = GenderMass;
        }

        private void AddImage_click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.Filter = "Файлы изображений: (*.png, *.jpg,*.jpeg)|*png; *jpg; *jpeg";
            OFD.InitialDirectory = "C:\\Users\\artem\\source\\repos\\BeautySalon\\BeautySalon\\Resources\\"; //Сюда будут сохраняться все фотографии
            if (OFD.ShowDialog() == true)
            {
                string imagePath = OFD.FileName; //АБСОЛЮТНЫЙ ПУТЬ К ФАЙЛУ

                // Создайте BitmapImage из выбранной фотографии
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(imagePath);
                bitmapImage.DecodePixelWidth = 100; // Установите желаемую ширину миниатюры (в пикселях)
                bitmapImage.EndInit();

                // Установите BitmapImage в элемент Image на вашей форме (назовем его MyImage)
                AgentImage.Source = bitmapImage;

                // Сохраните путь к выбранной фотографии, если это необходимо
                PathImage = OFD.SafeFileName; //ОТНОСИТЕЛЬНЫЙ ПУТЬ К ФАЙЛУ
            }
        }
        public string[] GenderMass { get; set; } =
        {
            "Мужчина",
            "Женщина"

        };
        private void AddClient_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (FirstName.Text.Length > 50)
                {
                    MessageBox.Show("Имя не может быть длиннее 50 символов!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                if (LastName.Text.Length > 50)
                {
                    MessageBox.Show("Фамилия не может быть длиннее 50 символов!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                if (Patronymic.Text.Length > 50)
                {
                    MessageBox.Show("Отчество не может быть длиннее 50 символов!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                if (FirstName.Text == "")
                {
                    MessageBox.Show("Введите имя!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                if (LastName.Text == "")
                {
                    MessageBox.Show("Введите фамилию!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                if (Patronymic.Text == "")
                {
                    MessageBox.Show("Введите отчество!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                if (new Regex(@"^[a-zA-Z\s\-]+$").IsMatch(FirstName.Text))
                {
                    MessageBox.Show("Введите имя!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                if (new Regex(@"^[a-zA-Z\s\-]+$").IsMatch(LastName.Text))
                {
                    MessageBox.Show("Введите фамилию!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                if (new Regex(@"^[a-zA-Z\s\-]+$").IsMatch(Patronymic.Text)) //знак ! это тип не должно быть
                {
                    MessageBox.Show("Введите отчество!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                if ((Email.Text != "") && (!(new Regex(@"(\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)")).IsMatch(Email.Text)))
                {
                    MessageBox.Show("Введите электронную почту!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                };

                if (!new Regex(@"^[0-9+\-\(\)\s]+$").IsMatch(Phone.Text)) //\s это любой пробел.Здесь валидация на + и - и на скобки Короче валидация огонь!!! и знак ! это тип не должно быть, короче так надо)))
                {
                    MessageBox.Show("Введите телефон!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                int gen = Gender.SelectedIndex + 1;
                Client newClient = new Client()
                {
                    LastName = LastName.Text.ToString(),
                    FirstName = FirstName.Text.ToString(),
                    Patronymic = Patronymic.Text.ToString(),
                    Birthday = (DateTime)Birthday.SelectedDate,
                    RegistrationDate = (DateTime)RegistrationDate.SelectedDate,
                    Email = Email.Text.ToString(),
                    Phone = Phone.Text.ToString(),
                    GenderCode = gen.ToString(),
                    PhotoPath = PathImage

                };
                barhatnie_brovkiEntities.GetContext().Client.Add(newClient);
                barhatnie_brovkiEntities.GetContext().SaveChanges();
                MessageBox.Show("Данные успешно сохранены!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.GoBack();
            }
            catch
            {
                MessageBox.Show("Возникла ошибка ", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
