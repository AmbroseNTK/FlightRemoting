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
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

using SharedLibrary;

using WpfApp1.FlightSOAP;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        public MainWindow()
        {
            InitializeComponent();
        }
        private IFlightBUS flightBUS;
        FlightSOAP.FlightServiceSoapClient flightService;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            flightService = new FlightServiceSoapClient();
            flightService.Open();
        }
        private void TbTime_LostFocus(object sender, RoutedEventArgs e)
        {
            //Validate input time
            string[] tokens = tbTime.Text.Split(':');
            if (tokens.Length == 2)
            {
                int hours = 0;
                int minutes = 0;
                if (int.TryParse(tokens[0], out hours) && int.TryParse(tokens[1], out minutes))
                {
                    if (hours >= 0 && hours < 24 && minutes >= 0 && minutes < 60)
                    {
                        tbTime.Text = ((hours < 10) ? ("0" + hours) : hours.ToString()) + ":"
                            + ((minutes < 10) ? ("0" + minutes) : minutes.ToString());
                        return;
                    }
                }
            }
            tbTime.Text = "00:00";
        }

        private void BtSelect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (flightService.State == System.ServiceModel.CommunicationState.Opened)
                {
                    gridData.ItemsSource = flightService.SelectAll();
                    gridData.Columns.RemoveAt(0);   
                }
                else
                {
                    MessageBox.Show("SOAP is opening");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private FlightSOAP.Flight GetFlightFromFields(bool hasID = false)
        {
            string date = "";
            if (pickerDate.SelectedDate != null)
            {
                string month = pickerDate.SelectedDate.Value.Month.ToString();
                string day = pickerDate.SelectedDate.Value.Day.ToString();
                month = month.Length == 1 ? "0" + month : month;
                day = day.Length == 1 ? "0" + day : day;
                date = month + "/" + day + "/" + pickerDate.SelectedDate.Value.Year;

            }

            FlightSOAP.Flight flight = new FlightSOAP.Flight()
            {
                Code = tbCode.Text,
                ArrivalAirport = tbAA.Text,
                ArrivalGate = tbAG.Text,
                DepatureAirport = tbDA.Text,
                DepatureGate = tbDG.Text,
                Date = pickerDate.SelectedDate == null ? System.DateTime.Now.ToShortDateString() : date,
                CheckInTime = tbTime.Text
            };
            if (hasID)
            {
                flight.ID = int.Parse(tbID.Text);
            }
            return flight;
        }

        private void GridData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                FlightSOAP.Flight flight = this.gridData.SelectedItem as FlightSOAP.Flight;
                if (flight != null)
                {
                    tbID.Text = flight.ID.ToString();
                    tbCode.Text = flight.Code;
                    tbAA.Text = flight.ArrivalAirport;
                    tbAG.Text = flight.ArrivalGate;
                    tbDA.Text = flight.DepatureAirport;
                    tbDG.Text = flight.DepatureGate;
                    pickerDate.SelectedDate = DateTime.Parse(flight.Date);
                    tbTime.Text = flight.CheckInTime;
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source,MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private void BtInsert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                flightService.Insert(GetFlightFromFields());
                gridData.ItemsSource = flightService.SelectAll();
                gridData.Columns.RemoveAt(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                flightService.Update(GetFlightFromFields(true));
                gridData.ItemsSource = flightService.SelectAll();
                gridData.Columns.RemoveAt(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtDelete_Click(object sender, RoutedEventArgs e)
        {
            
            if (MessageBox.Show("Delete this record...","Warning",MessageBoxButton.OKCancel,MessageBoxImage.Question)==MessageBoxResult.OK)
            {
                try
                {
                    flightService.Delete(int.Parse(tbID.Text));
                    gridData.ItemsSource = flightService.SelectAll();
                    gridData.Columns.RemoveAt(0);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.Source, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtKeywords_Click(object sender, RoutedEventArgs e)
        {
            string keyword = (sender as Button).Tag.ToString();
            if (tbSearch.Text!="" && tbSearch.Text.Substring(tbSearch.Text.Length - 1) != ";")
            {
                tbSearch.Text += ";";
            }
            tbSearch.Text += keyword;
        }

        private void BtSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                gridData.ItemsSource = flightService.Search(tbSearch.Text);
                gridData.Columns.RemoveAt(0);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
