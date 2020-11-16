using NZTravelMate.Models;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace NZTravelMate.ViewModels
{
    public class CurrencyViewModel : ViewModelBase
    {
        //The api string requires a "base currency code" to work off

        private ObservableCollection<Currency> _currencies;
        private ICurrencyStore _currencyStore;
        private AppState _appState;
        private IAppStateStore _appStateStore;

        private bool _isDataLoaded;

        private double _firstAmount = 1;
        private double _secondAmount = 1;
        private int _firstCurrency = 32; // NZD
        private int _secondCurrency = 2; // AUD
        private string _firstOutput = "";
        private string _secondOutput = "";
        private double _taxInput = 0;
        private string _taxOutput = "";
        private bool _taxVisible = false;

        private bool isCalculating = false;

        #region Exposed Bindables

        //Swap is assigned in the constructor
        public ICommand SwapCommand { private set; get; }

        public ICommand LoadDataCommand { get; private set; }
        public ICommand SaveAppStateDataCommand { get; private set; }

        public AppState CurrentState
        {
            get { return _appState; }
            set
            {
                _appState = value;
                _firstCurrency = _appState.FirstIndex;
                _secondCurrency = _appState.SecondIndex;
                //Update the appState table
                Debug.WriteLine($"{_appState.Id}, {_appState.FirstIndex}, {_appState.SecondIndex}");
                SaveAppStateDataCommand.Execute(null);

                OnPropertyChanged();
            }
        }

        public ObservableCollection<Currency> Currencies
        {
            get { return _currencies; }
            set
            {
                _currencies = value;
                OnPropertyChanged();
            }
        }

        public double FirstAmount
        {
            get { return _firstAmount; }
            set
            {
                _firstAmount = value;
                OnPropertyChanged();
                MakeCalculation(true);
            }
        }

        public double SecondAmount
        {
            get { return _secondAmount; }
            set
            {
                _secondAmount = value;
                OnPropertyChanged();
                MakeCalculation(false);
            }
        }

        public int FirstCurrency
        {
            get { return _firstCurrency; }
            set
            {
                _firstCurrency = value;
                OnPropertyChanged();
                MakeCalculation(true);
            }
        }

        public int SecondCurrency
        {
            get { return _secondCurrency; }
            set
            {
                _secondCurrency = value;
                OnPropertyChanged();
                MakeCalculation(true);
            }
        }

        public string FirstOutput
        {
            get { return _firstOutput; }
            set
            {
                _firstOutput = value;
                OnPropertyChanged();
            }
        }

        public string SecondOutput
        {
            get { return _secondOutput; }
            set
            {
                _secondOutput = value;
                OnPropertyChanged();
            }
        }

        //Tax calculation part
        public double TaxInput
        {
            get { return _taxInput; }
            set
            {
                _taxInput = value;
                OnPropertyChanged();
                MakeCalculation(true);
            }
        }

        public string TaxOutput
        {
            get { return _taxOutput; }
            set
            {
                _taxOutput = value;
                OnPropertyChanged();
            }
        }

        public bool TaxVisible
        {
            get { return _taxVisible; }
            set
            {
                _taxVisible = value;
                OnPropertyChanged();
            }
        }

        #endregion Exposed Bindables

        //Constructor
        public CurrencyViewModel(SQLiteCurrencyStore currencyStore, SQLiteAppStateStore appStateStore)
        {
            _currencyStore = currencyStore;
            _appStateStore = appStateStore;

            //Set the property to a new Command containing the method I want to call
            SwapCommand = new Command(execute: () => { SwapCurrencies(); });
            LoadDataCommand = new Command(async () => await LoadData());
            SaveAppStateDataCommand = new Command(async () => await SaveAppStateData());
        }

        //Load Database
        private async Task LoadData()
        {
            if (_isDataLoaded)
                return;
            try
            {
                //Get values from API connection
                var OERA = new OpenExchangeRateApi();
                var currencies = await OERA.GetCurrency();
                if (currencies != null)
                {
                    //Save newly constructed currency data to database
                    Currencies = currencies;
                    await SaveCurrencyData(_currencies);
                    Debug.WriteLine("UPDATED FROM API");
                }
                else
                {
                    Debug.WriteLine("LOADED FROM DATABASE");
                    //If API failure load data from Database
                    Currencies = await _currencyStore.GetCurrenciesAsync();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tData Failed to Load: {0}", ex.Message);
            }
            try
            {
                //Load previous state
                var oldState = await _appStateStore.GetAppStateAsync();
                if (oldState == null)
                {
                    Debug.WriteLine("OLD STATE IS NULL");
                    var newState = new AppState { Id = 1, FirstIndex = 32, SecondIndex = 2 };
                    await _appStateStore.AddAppState(newState);
                    CurrentState = newState;
                }
                else
                {
                    Debug.WriteLine("READING FROM OLD STATE");
                    CurrentState = oldState;
                }

                _isDataLoaded = true;
                //Hacky way to refresh pickers
                SwapCurrencies();
                SwapCurrencies();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tApp State Failed to Load: {0}", ex.Message);
            }
        }

        private async Task SaveAppStateData()
        {
            if (!_isDataLoaded)
                return;
            try
            {
                await _appStateStore.UpdateAppState(_appState);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tData Failed to Save: {0}", ex.Message);
            }
        }

        //Save or update the currency database
        private async Task SaveCurrencyData(ObservableCollection<Currency> currencies)
        {
            Debug.WriteLine($"THERE ARE {currencies.Count}");
            foreach (var currency in currencies)
            {
                var databaseObject = await _currencyStore.GetCurrency(currency.Code);
                //If it exists in the database, update it. Otherwise create it
                if (databaseObject == null)
                {
                    Debug.WriteLine($"TRYING TO CREATE {currency.Code}");
                    await _currencyStore.AddCurrency(currency);
                }
                else
                {
                    Debug.WriteLine($"TRYING TO UPDATE {currency.Code}");
                    await _currencyStore.UpdateCurrency(currency);
                }
            }
        }

        //Calculation
        public void MakeCalculation(bool isFirst)
        {
            if (isCalculating || !_isDataLoaded) return;
            try
            {
                isCalculating = true;

                double firstValue = FirstAmount != 0 ? FirstAmount : 1;
                double secondValue = SecondAmount != 0 ? SecondAmount : 1;

                int leftIndex = FirstCurrency;
                int rightIndex = SecondCurrency;

                //If they have the same currency, flip the last pair of currencies around
                if (leftIndex == rightIndex)
                {
                    leftIndex = CurrentState.SecondIndex;
                    rightIndex = CurrentState.FirstIndex;

                    FirstCurrency = leftIndex;
                    SecondCurrency = rightIndex;
                }

                //Order of calculations based on which block of elements was updated
                if (isFirst)
                {
                    SecondAmount = Math.Round(
                        CalculationStation.GetValueByRates(
                        firstValue,
                        _currencies[leftIndex].Rate,
                        _currencies[rightIndex].Rate
                        ), 2);
                    CurrentState.FirstIndex = leftIndex;
                    CurrentState.SecondIndex = rightIndex;
                }
                else
                {
                    FirstAmount = Math.Round(
                        CalculationStation.GetValueByRates(
                        secondValue,
                        _currencies[rightIndex].Rate,
                        _currencies[leftIndex].Rate
                        ), 2);
                    CurrentState.FirstIndex = rightIndex;
                    CurrentState.SecondIndex = leftIndex;
                }

                double taxValue = 0;
                //Show or hide Tax
                if (_taxInput > 0)
                {
                    taxValue = Math.Round(SecondAmount * (_taxInput * 0.01), 2);
                    TaxOutput = $"+ Sales Tax: {taxValue}";
                    TaxVisible = true;
                }
                else
                {
                    _taxInput = 0;
                    TaxVisible = false;
                }

                //Final display
                FirstOutput = $"{_firstAmount} {_currencies[CurrentState.FirstIndex].Name}";
                SecondOutput = $"{_secondAmount + taxValue} {_currencies[CurrentState.SecondIndex].Name}";
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tView Model Calculation Failed: {0}", ex.Message);
            }
            finally
            {
                CurrentState = CurrentState;
                isCalculating = false;
            }
        }

        public void SwapCurrencies()
        {
            //Bit hacky relying on make calculation to flip it
            isCalculating = true;
            SecondCurrency = CurrentState.FirstIndex;
            isCalculating = false;
            FirstCurrency = CurrentState.SecondIndex;
        }
    }
}