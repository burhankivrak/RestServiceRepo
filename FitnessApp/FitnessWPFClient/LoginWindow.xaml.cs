using FitnessApp.Data;
using FitnessApp.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FitnessWPFClient
{
    public partial class LoginWindow : Window
    {
        private readonly MemberRepository _memberRepository;

        public LoginWindow()
        {
            InitializeComponent();
            _memberRepository = new MemberRepository(new FitnessContext());
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;
            DateTime? geboortedatum = GeboortedatumPicker.SelectedDate;

            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Vul een e-mailadres in.");
                return;
            }

            if (!geboortedatum.HasValue)
            {
                MessageBox.Show("Selecteer een geboortedatum.");
                return;
            }

            try
            {
                var member = _memberRepository.GetMemberByEmailAndBirthday(email, geboortedatum.Value);
                if (member != null)
                {
                    var reserveerWindow = new ReserveerWindow(member.Id);
                    reserveerWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Geen lid gevonden met deze combinatie van e-mail en geboortedatum.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Er is een fout opgetreden: {ex.Message}");
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var registerWindow = new RegisterWindow();
            registerWindow.Show();
            this.Close();
        }
    }
}
