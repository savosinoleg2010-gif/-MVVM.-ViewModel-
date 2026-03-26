using System;
using System.Windows.Input;
using ConsoleApp1.Core;

namespace ConsoleApp1.ViewModels;

public class TipCalculatorViewModel : ViewModelBase
{
    private decimal _billAmount;
    private decimal _tipPercent = 10;
    private int _numberOfPeople = 1;
    private bool _roundUp;

    public TipCalculatorViewModel()
    {
        ResetCommand = new RelayCommand(_ => Reset(), _ => true);
        CalculateCommand = new RelayCommand(_ => Calculate(), _ => true);
        SetTipPercentCommand = new RelayCommand(parameter => SetTipPercent(parameter), _ => true);
    }

    public decimal BillAmount
    {
        get => _billAmount;
        set
        {
            if (SetProperty(ref _billAmount, value))
            {
                Calculate();
            }
        }
    }

    public decimal TipPercent
    {
        get => _tipPercent;
        set
        {
            if (SetProperty(ref _tipPercent, value))
            {
                Calculate();
            }
        }
    }

    public int NumberOfPeople
    {
        get => _numberOfPeople;
        set
        {
            if (value < 1) value = 1;
            if (SetProperty(ref _numberOfPeople, value))
            {
                Calculate();
            }
        }
    }

    public bool RoundUp
    {
        get => _roundUp;
        set
        {
            if (SetProperty(ref _roundUp, value))
            {
                Calculate();
            }
        }
    }

    public decimal TipAmount { get; private set; }
    public decimal TotalAmount { get; private set; }
    public decimal AmountPerPerson { get; private set; }

    public ICommand ResetCommand { get; }
    public ICommand CalculateCommand { get; }
    public ICommand SetTipPercentCommand { get; }

    private void Calculate()
    {
        TipAmount = BillAmount * (TipPercent / 100);
        TotalAmount = BillAmount + TipAmount;

        if (RoundUp)
        {
            TotalAmount = Math.Ceiling(TotalAmount);
        }

        AmountPerPerson = TotalAmount / NumberOfPeople;

        if (RoundUp)
        {
            AmountPerPerson = Math.Ceiling(AmountPerPerson);
        }

        OnPropertyChanged(nameof(TipAmount));
        OnPropertyChanged(nameof(TotalAmount));
        OnPropertyChanged(nameof(AmountPerPerson));
    }

    private void Reset()
    {
        BillAmount = 0;
        TipPercent = 10;
        NumberOfPeople = 1;
        RoundUp = false;
        TipAmount = 0;
        TotalAmount = 0;
        AmountPerPerson = 0;

        OnPropertyChanged(nameof(TipAmount));
        OnPropertyChanged(nameof(TotalAmount));
        OnPropertyChanged(nameof(AmountPerPerson));
    }

    private void SetTipPercent(object? parameter)
    {
        if (parameter is int percent)
        {
            TipPercent = percent;
        }
    }
}
