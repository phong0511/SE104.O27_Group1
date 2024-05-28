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
using BUS;
using DTO;
using DAL;
using System.Globalization;

namespace GUI
{
    /// <summary>
    /// Interaction logic for AddAndUpdateProject.xaml
    /// </summary>
    public partial class AddAndUpdateProject : Window
    {
        BUS_DuAn projectManager = new BUS_DuAn();
        public AddAndUpdateProject(DTO_DuAn initializeDA = null)
        {
            InitializeComponent();
            if (initializeDA != null)
            {
                wTitle.Text = "SỬA DU AN";
                ButtonAddNew.Visibility = Visibility.Hidden;
                ButtonUpdate.Visibility = Visibility.Visible;
                madaText.Text = initializeDA.MADA;
                tendaText.Text = initializeDA.TENDA;
                statText.SelectedValue = initializeDA.STAT;
                TStartText.Text = initializeDA.TSTART;
                TEndText.Text = initializeDA.TEND;
                ngansachText.Text = initializeDA.NGANSACH.ToString();
                malskText.Text = initializeDA.MALSK;
                manqlText.Text = initializeDA.MAOWNER;

            }
        }

        private void ButtonAddNew_Click(object sender, RoutedEventArgs e)
        {
            DTO_DuAn newDA = new DTO_DuAn();
            newDA.TENDA = tendaText.Text;
            newDA.STAT = statText.SelectedValue != null ? statText.SelectedValue.ToString() : "";
            newDA.MADA = madaText.Text;
            newDA.TSTART = TStartText.Text;
            newDA.TEND = TEndText.Text;
            newDA.NGANSACH = long.TryParse(ngansachText.Text, out long tempResult) ? tempResult : -1;
            newDA.MALSK = malskText.Text;
            newDA.MAOWNER = manqlText.Text;
            (bool, string) res = projectManager.AddData(newDA);

            if (res.Item1 == true)
            {
                MessageBox.Show("Thêm du an thành công!");
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show(res.Item2);
            }
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            DTO_DuAn da = new DTO_DuAn();
            da.MADA = madaText.Text;
            da.MALSK = malskText.Text;
            da.MAOWNER = manqlText.Text;
            da.TENDA = tendaText.Text;
            da.STAT = statText.SelectedValue.ToString();
            da.NGANSACH = long.TryParse(ngansachText.Text, out long tempResult) ? tempResult : -1;
            da.TSTART = TStartText.Text;
            da.TEND = TEndText.Text;
            (bool, string) res = projectManager.EditProject(da);

            if (res.Item1 == true)
            {
                MessageBox.Show("Sửa du an thành công!");
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show(res.Item2);
            }
        }
    }
}
