using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using GUI.ViewModel;
using MaterialDesignThemes.Wpf;
using System.Collections;
using System.Windows.Media;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : UserControl
    {
        ObservableCollection<Flight> flights = new ObservableCollection<Flight>();
        private List<SortPropertyPair> SortProperties = new List<SortPropertyPair>
        {
            new SortPropertyPair { Key = "Seq", Value = "STT"},
            new SortPropertyPair { Key= "Departure airport", Value = "SanBayDi"},
            new SortPropertyPair { Key = "Destination airport", Value = "SanBayDen"},
            new SortPropertyPair { Key = "Departure time", Value = "KhoiHanh"},
            new SortPropertyPair { Key = "Duration", Value = "ThoiGian"},
            new SortPropertyPair { Key = "Empty seat", Value = "SoGheDat"},
            new SortPropertyPair { Key = "Booked seat", Value = "SoGheTrong"}
        };
        private Dictionary<string, string> airportDictionary = new Dictionary<string, string>();

        public List<AirportDTO> airports { get; set; }
        public Window2()
        {
            // InitializeComponent();
            var converter = new BrushConverter();

            FlightsDataGrid.ItemsSource = flights;

            Airport_BLL airport_bll = new Airport_BLL();

            // Dùng cho item source
            airports = airport_bll.L_airport();
            // Dùng cho xử lý nếu cần
            airportDictionary = airports.ToDictionary(airport => airport.AirportID, airport => airport.AirportName);

            SourceAirport.ItemsSource = airports;
            DestinationAirport.ItemsSource = airports;

            LoadFlight();
        }

        private void LoadFlight()
        {
            DateTime startDate = StartDay.SelectedDate.HasValue ? StartDay.SelectedDate.Value.Date : new DateTime(1753, 1, 1, 0, 0, 0);
            DateTime endDate = EndDay.SelectedDate.HasValue ? EndDay.SelectedDate.Value.Date : new DateTime(9999, 12, 31, 23, 59, 59);
            List<FlightInforDTO> flightInformationSearches = new List<FlightInforDTO>();
            flightInformationSearches = new BLL.SearchProcessor().GetFlightInfoDTO(string.Empty, string.Empty, startDate, endDate);
            flights = Flight.ConvertListToObservableCollection(flightInformationSearches, airportDictionary);
            FlightsDataGrid.ItemsSource = flights;
        }

        private bool IsEmpty(object value)
        {
            if (value == null)
            {
                return true;
            }

            if (value is string str)
            {
                return string.IsNullOrEmpty(str);
            }

            if (value is ICollection collection)
            {
                return collection.Count == 0;
            }

            if (value is Array array)
            {
                return array.Length == 0;
            }

            // Add more type checks as needed

            return false;
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            string a = string.Empty;
            string b = string.Empty;
            if (SourceAirport.SelectedIndex != -1)
            {
                a = SourceAirport.SelectedValue as string;
            }
            if (DestinationAirport.SelectedIndex != -1)
            {
                b = DestinationAirport.SelectedValue as string;
            }

            // Check if SourceAirport or DestinationAirport are empty
            if (IsEmpty(a) || IsEmpty(b))
            {
                MessageBox.Show("Source or Destination airport cannot be empty.");
                return;
            }

            // 2 Giá trị dưới là min và max cho phép của SQL
            DateTime startDate = StartDay.SelectedDate.HasValue ? StartDay.SelectedDate.Value.Date : new DateTime(1753, 1, 1, 0, 0, 0);
            DateTime endDate = EndDay.SelectedDate.HasValue ? EndDay.SelectedDate.Value.Date : new DateTime(9999, 12, 31, 23, 59, 59);

            List<FlightInforDTO> flightInformationSearches = new List<FlightInforDTO>();
            flightInformationSearches = new BLL.SearchProcessor().GetFlightInfoDTO(a, b, startDate, endDate);
            flights = Flight.ConvertListToObservableCollection(flightInformationSearches, airportDictionary);

            FlightsDataGrid.ItemsSource = flights;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Edit f = new Edit();
            f.Show();
        }

        private void textBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        //<<<<<<<<<<<====================FUNCTION TO TEST=============================>>>>>>>>>>>>>>



        public string EmptyAirportCheck(string sourceAirport, string destinationAirport)
        {
            if (string.IsNullOrEmpty(sourceAirport) || string.IsNullOrEmpty(destinationAirport))
            {
                return "Source or Destination airport cannot be empty.";
            }
            return "";
        }

        private List<Flight> flightscheck = new List<Flight>();
        private ComboBox comboBoxSource = new ComboBox();
        private ComboBox comboBoxDestination = new ComboBox();
        private ListBox listBoxResults = new ListBox();
        public void CreateData()
        {
            flightscheck = new List<Flight>
            {
                new Flight("Hanoi", "HoChiMinh", new DateTime(2024, 11, 30), new DateTime(2024, 11, 30, 12, 0, 0)),
                new Flight("Hanoi", "Danang", new DateTime(2024, 12, 1), new DateTime(2024, 12, 1, 14, 0, 0)),
                new Flight("HoChiMinh", "Hanoi", new DateTime(2024, 12, 2), new DateTime(2024, 12, 2, 16, 0, 0))
            };
            
            // Cập nhật dữ liệu vào ComboBox
            var airports = flightscheck.SelectMany(f => new[] { f.SourceAirport, f.DestinationAirport }).Distinct().ToList();
            comboBoxSource.ItemsSource = airports;
            comboBoxDestination.ItemsSource = airports.ToList();
        }

        public void ComboBoxSelectionChanged(object sender, EventArgs e)
        {
            var source = comboBoxSource.SelectedItem?.ToString();
            var destination = comboBoxDestination.SelectedItem?.ToString();

            if (!string.IsNullOrEmpty(source) && !string.IsNullOrEmpty(destination))
            {
                var matchingFlights = flights
                    .Where(f => f.SourceAirport == source && f.DestinationAirport == destination)
                    .ToList();

                if (matchingFlights.Any())
                {
                    listBoxResults.ItemsSource = matchingFlights;
                    
                }
                else
                {
                    listBoxResults.ItemsSource = null;

                }
            }
        }
    }
    public class Flight
    {
        public string SourceAirport { get; set; }
        public string DestinationAirport { get; set; }
        public DateTime StartDay { get; set; }
        public DateTime EndDay { get; set; }

        public Flight(string source, string destination, DateTime start, DateTime end)
        {
            SourceAirport = source;
            DestinationAirport = destination;
            StartDay = start;
            EndDay = end;
        }

        public string FlightDetails => $"{SourceAirport} -> {DestinationAirport} | {StartDay:dd/MM/yyyy HH:mm} - {EndDay:dd/MM/yyyy HH:mm}";

        internal static ObservableCollection<Flight> ConvertListToObservableCollection(List<FlightInforDTO> flightInformationSearches, Dictionary<string, string> airportDictionary)
        {
            throw new NotImplementedException();
        }
    }
}
