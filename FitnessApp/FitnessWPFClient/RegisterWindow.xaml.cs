using System.Windows;
using System.Windows.Controls;
using FitnessApp.Manager;
using FitnessApp.Model;
using FitnessDL.Enums;
using FitnessApp.Data;

namespace FitnessWPFClient
{
    public partial class RegisterWindow : Window
    {
        private readonly MemberRepository _memberRepository;

            public RegisterWindow()
            {
                InitializeComponent();
                _memberRepository = new MemberRepository(new FitnessContext());
            }

            private void RegisterButton_Click(object sender, RoutedEventArgs e)
            {
                try
                {
                    var voornaam = VoornaamTextBox.Text;
                    var achternaam = AchternaamTextBox.Text;
                    var email = EmailTextBox.Text;
                    var verblijfsplaats = VerblijfsplaatsTextBox.Text;
                    var geboortedatum = GeboortedatumPicker.SelectedDate ?? throw new Exception("Geboortedatum is verplicht.");
                    var interesses = string.IsNullOrWhiteSpace(InteressesTextBox.Text) ? null : new List<string>(InteressesTextBox.Text.Split(','));
                    var klantType = (KlantType)Enum.Parse(typeof(KlantType), (KlantTypeComboBox.SelectedItem as ComboBoxItem).Content.ToString());

                var newMember = new Members
                    {
                        Voornaam = voornaam,
                        Achternaam = achternaam,
                        Emailadres = email,
                        Verblijfsplaats = verblijfsplaats,
                        Geboortedatum = geboortedatum,
                        TypeKlant = klantType,
                        Interesses = interesses
                    };

                    _memberRepository.AddMember(newMember);

                    MessageBox.Show("Registratie succesvol!");
                    var loginWindow = new LoginWindow();
                    loginWindow.Show();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Fout bij registratie: {ex.Message}");
                }
            }
    }
 }

