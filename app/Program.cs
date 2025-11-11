using System;
using System.Collections.Generic;
using System.Globalization;

// Базовый класс устройства: общие поля и поведение
public class Device
{
    // Поля: производитель, модель, цвет, цена
    private string _brand = string.Empty;
    private string _model = string.Empty;
    private string _color = string.Empty;
    private decimal _price;

    // Свойство: бренд (не пустая строка)
    public string Brand
    {
        get => _brand;
        set => _brand = ValidateNonEmpty(value, nameof(Brand));
    }

    // Свойство: модель (не пустая строка)
    public string Model
    {
        get => _model;
        set => _model = ValidateNonEmpty(value, nameof(Model));
    }

    // Свойство: цвет (не пустая строка)
    public string Color
    {
        get => _color;
        set => _color = ValidateNonEmpty(value, nameof(Color));
    }

    // Свойство: цена (0..1_000_000)
    public decimal Price
    {
        get => _price;
        set
        {
            if (value < 0m || value > 1_000_000m)
            {
                throw new ArgumentOutOfRangeException(nameof(Price), "Цена должна быть в диапазоне [0; 1_000_000].");
            }
            _price = value;
        }
    }

    // Конструктор с параметрами по умолчанию
    public Device(string brand = "Generic", string model = "Model", string color = "Black", decimal price = 0m)
    {
        Brand = brand;
        Model = model;
        Color = color;
        Price = price;
    }

    // Функции-доступоры (дублируют свойства — по требованию задания)
    public string GetBrand() => Brand;
    public string GetModel() => Model;
    public string GetColor() => Color;
    public decimal GetPrice() => Price;

    // Функции-мутаторы (изменение характеристик)
    public void UpdatePrice(decimal newPrice) => Price = newPrice;
    public void UpdateColor(string newColor) => Color = newColor;

    // Проверка на непустую строку
    protected static string ValidateNonEmpty(string value, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException($"{fieldName} не может быть пустым.");
        }
        return value.Trim();
    }

    // Виртуальная печать сведений (для полиморфизма)
    public virtual void PrintInfo()
    {
        Console.WriteLine($"Устройство: {Brand} {Model}, цвет: {Color}, цена: {Price.ToString(CultureInfo.InvariantCulture)}");
    }
}

// Производный класс: Телевизор
public class Television : Device
{
    // Доп. поля: диагональ, разрешение, smart-функции, тип панели
    private int _screenSizeInches;
    private string _resolution = string.Empty;
    private bool _isSmart;
    private string _panelType = string.Empty;

    // Диагональ (10..120 дюймов)
    public int ScreenSizeInches
    {
        get => _screenSizeInches;
        set
        {
            if (value < 10 || value > 120)
            {
                throw new ArgumentOutOfRangeException(nameof(ScreenSizeInches), "Диагональ должна быть в диапазоне [10; 120] дюймов.");
            }
            _screenSizeInches = value;
        }
    }

    // Разрешение (строка, не пустая)
    public string Resolution
    {
        get => _resolution;
        set => _resolution = ValidateNonEmpty(value, nameof(Resolution));
    }

    // Признак Smart TV
    public bool IsSmart
    {
        get => _isSmart;
        set => _isSmart = value;
    }

    // Тип панели (LED, OLED, QLED и т.п.)
    public string PanelType
    {
        get => _panelType;
        set => _panelType = ValidateNonEmpty(value, nameof(PanelType));
    }

    // Конструктор с параметрами по умолчанию
    public Television(
        string brand = "Generic",
        string model = "TV",
        string color = "Black",
        decimal price = 0m,
        int screenSizeInches = 32,
        string resolution = "1080p",
        bool isSmart = true,
        string panelType = "LED") : base(brand, model, color, price)
    {
        ScreenSizeInches = screenSizeInches;
        Resolution = resolution;
        IsSmart = isSmart;
        PanelType = panelType;
    }

    // Доступоры 
    public int GetScreenSizeInches() => ScreenSizeInches;
    public string GetResolution() => Resolution;
    public bool GetIsSmart() => IsSmart;
    public string GetPanelType() => PanelType;

    // Мутаторы
    public void UpdateResolution(string newResolution) => Resolution = newResolution;
    public void UpdateScreenSize(int newSize) => ScreenSizeInches = newSize;

    // Переопределённая печать сведений
    public override void PrintInfo()
    {
        Console.WriteLine(
            $"Телевизор: {Brand} {Model}, {ScreenSizeInches}\" {Resolution}, панель: {PanelType}, smart: {(IsSmart ? "да" : "нет")}, цвет: {Color}, цена: {Price.ToString(CultureInfo.InvariantCulture)}");
    }
}

// Производный класс: Радиоприёмник
public class RadioReceiver : Device
{
    // Доп. поля: диапазон, мин/макс частота, RDS
    private string _band = string.Empty; // AM, FM, AM/FM, DAB
    private double _minFrequencyMHz;
    private double _maxFrequencyMHz;
    private bool _hasRds;

    // Диапазон (ограниченный набор значений)
    public string Band
    {
        get => _band;
        set
        {
            string v = ValidateNonEmpty(value, nameof(Band)).ToUpperInvariant();
            if (v != "AM" && v != "FM" && v != "AM/FM" && v != "DAB")
            {
                throw new ArgumentException("Диапазон должен быть одним из: AM, FM, AM/FM, DAB.");
            }
            _band = v;
        }
    }

    // Минимальная частота (0.05..300 МГц)
    public double MinFrequencyMHz
    {
        get => _minFrequencyMHz;
        set
        {
            if (value < 0.05 || value > 300.0)
            {
                throw new ArgumentOutOfRangeException(nameof(MinFrequencyMHz), "Мин. частота должна быть в диапазоне [0.05; 300] МГц.");
            }
            _minFrequencyMHz = value;
        }
    }

    // Максимальная частота (0.05..300 МГц)
    public double MaxFrequencyMHz
    {
        get => _maxFrequencyMHz;
        set
        {
            if (value < 0.05 || value > 300.0)
            {
                throw new ArgumentOutOfRangeException(nameof(MaxFrequencyMHz), "Макс. частота должна быть в диапазоне [0.05; 300] МГц.");
            }
            _maxFrequencyMHz = value;
        }
    }

    // Наличие RDS
    public bool HasRds
    {
        get => _hasRds;
        set => _hasRds = value;
    }

    // Конструктор с параметрами по умолчанию
    public RadioReceiver(
        string brand = "Generic",
        string model = "Radio",
        string color = "Black",
        decimal price = 0m,
        string band = "FM",
        double minFrequencyMHz = 87.5,
        double maxFrequencyMHz = 108.0,
        bool hasRds = true) : base(brand, model, color, price)
    {
        Band = band;
        MinFrequencyMHz = minFrequencyMHz;
        MaxFrequencyMHz = maxFrequencyMHz;
        if (MaxFrequencyMHz <= MinFrequencyMHz)
        {
            throw new ArgumentException("Макс. частота должна быть больше мин. частоты.");
        }
        HasRds = hasRds;
    }

    // Доступоры
    public string GetBand() => Band;
    public double GetMinFrequencyMHz() => MinFrequencyMHz;
    public double GetMaxFrequencyMHz() => MaxFrequencyMHz;
    public bool GetHasRds() => HasRds;

    // Мутаторы
    public void UpdateBand(string newBand) => Band = newBand;
    public void UpdateFrequencyRange(double minMHz, double maxMHz)
    {
        MinFrequencyMHz = minMHz;
        MaxFrequencyMHz = maxMHz;
        if (MaxFrequencyMHz <= MinFrequencyMHz)
        {
            throw new ArgumentException("Макс. частота должна быть больше мин. частоты.");
        }
    }

    // Переопределённая печать сведений
    public override void PrintInfo()
    {
        Console.WriteLine(
            $"Радиоприемник: {Brand} {Model}, диапазон: {Band}, {MinFrequencyMHz.ToString(CultureInfo.InvariantCulture)}–{MaxFrequencyMHz.ToString(CultureInfo.InvariantCulture)} МГц, RDS: {(HasRds ? "да" : "нет")}, цвет: {Color}, цена: {Price.ToString(CultureInfo.InvariantCulture)}");
    }
}

public static class Program
{
    // Глобальный список устройств для работы с ними
    private static List<Device> devices = new List<Device>();

    public static void Main()
    {
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

        // Создание устройств по умолчанию
        InitializeDefaultDevices();

        // Интерактивное меню
        RunInteractiveMenu();
    }

    private static void InitializeDefaultDevices()
    {
        // Создание телевизоров
        devices.Add(new Television(brand: "Samsung", model: "Q80", color: "Black", price: 79999m, screenSizeInches: 55, resolution: "4K", isSmart: true, panelType: "QLED"));
        devices.Add(new Television(brand: "LG", model: "C2", color: "Gray", price: 119999m, screenSizeInches: 65, resolution: "4K", isSmart: true, panelType: "OLED"));
        devices.Add(new Television(brand: "Philips", model: "PUS8507", color: "Silver", price: 64999m, screenSizeInches: 50, resolution: "4K", isSmart: true, panelType: "LED"));

        // Создание радиоприёмников
        devices.Add(new RadioReceiver(brand: "Sony", model: "ICF-P36", color: "Black", price: 1999m, band: "AM/FM", minFrequencyMHz: 0.52, maxFrequencyMHz: 108.0, hasRds: false));
        devices.Add(new RadioReceiver(brand: "Panasonic", model: "RF-2400D", color: "Black", price: 3499m, band: "AM/FM", minFrequencyMHz: 0.52, maxFrequencyMHz: 108.0, hasRds: false));
        devices.Add(new RadioReceiver(brand: "Sony", model: "XDR-S61D", color: "White", price: 8999m, band: "DAB", minFrequencyMHz: 174.928, maxFrequencyMHz: 239.200, hasRds: true));

    }

    private static void PrintAllDevices()
    {
        for (int i = 0; i < devices.Count; i++)
        {
            Console.Write($"{i + 1}. ");
            devices[i].PrintInfo();
        }
    }

    private static void RunInteractiveMenu()
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("=== Меню ===");
            Console.WriteLine("1) Добавить телевизор (Television)");
            Console.WriteLine("2) Добавить радиоприемник (RadioReceiver)");
            Console.WriteLine("3) Показать список устройств");
            Console.WriteLine("4) Изменить характеристики устройства");
            Console.WriteLine("5) Выход");
            Console.Write("Ваш выбор: ");
            string? choice = Console.ReadLine();
            Console.WriteLine();

            if (choice == "1")
            {
                // Ввод параметров и добавление телевизора
                var t = CreateTelevisionFromInput();
                devices.Add(t);
                Console.WriteLine("Телевизор добавлен.");
            }
            else if (choice == "2")
            {
                // Ввод параметров и добавление радиоприёмника
                var r = CreateRadioFromInput();
                devices.Add(r);
                Console.WriteLine("Радиоприемник добавлен.");
            }
            else if (choice == "3")
            {
                if (devices.Count == 0)
                {
                    Console.WriteLine("Список пуст.");
                }
                else
                {
                    Console.WriteLine("=== Список устройств ===");
                    PrintAllDevices();
                }
            }
            else if (choice == "4")
            {
                // Изменение характеристик устройства
                ModifyDevice();
            }
            else if (choice == "5")
            {
                // В C# память очищает GC, освободим список явно для наглядности
                devices.Clear();
                Console.WriteLine("Завершение работы.");
                break;
            }
            else
            {
                Console.WriteLine("Неизвестный пункт меню.");
            }
        }
    }

    private static void ModifyDevice()
    {
        if (devices.Count == 0)
        {
            Console.WriteLine("Список устройств пуст.");
            return;
        }

        Console.WriteLine("=== Выберите устройство для изменения ===");
        PrintAllDevices();
        
        int deviceIndex = ReadInt("Введите номер устройства: ", 1, devices.Count) - 1;
        var device = devices[deviceIndex];

        Console.WriteLine($"\nВыбрано устройство: {device.Brand} {device.Model}");
        Console.WriteLine("Текущие характеристики:");
        device.PrintInfo();

        // Меню изменения в зависимости от типа устройства
        if (device is Television tv)
        {
            ModifyTelevision(tv);
        }
        else if (device is RadioReceiver radio)
        {
            ModifyRadioReceiver(radio);
        }
        else
        {
            ModifyBaseDevice(device);
        }

        Console.WriteLine("\nОбновлённые характеристики:");
        device.PrintInfo();
    }

    private static void ModifyTelevision(Television tv)
    {
        Console.WriteLine("\n=== Изменение характеристик телевизора ===");
        Console.WriteLine("1) Изменить цену");
        Console.WriteLine("2) Изменить цвет");
        Console.WriteLine("3) Изменить диагональ");
        Console.WriteLine("4) Изменить разрешение");
        Console.WriteLine("5) Изменить тип панели");
        Console.WriteLine("6) Изменить Smart TV");
        
        int choice = ReadInt("Выберите параметр для изменения: ", 1, 6);
        
        switch (choice)
        {
            case 1:
                decimal newPrice = ReadDecimal("Новая цена: ", 0m, 1_000_000m);
                tv.UpdatePrice(newPrice);
                break;
            case 2:
                string newColor = ReadNonEmpty("Новый цвет: ");
                tv.UpdateColor(newColor);
                break;
            case 3:
                int newSize = ReadInt("Новая диагональ (дюймы 10-120): ", 10, 120);
                tv.UpdateScreenSize(newSize);
                break;
            case 4:
                string newResolution = ReadNonEmpty("Новое разрешение: ");
                tv.UpdateResolution(newResolution);
                break;
            case 5:
                string newPanel = ReadNonEmpty("Новый тип панели: ");
                tv.PanelType = newPanel;
                break;
            case 6:
                bool newSmart = ReadBool("Smart TV? (y/n): ");
                tv.IsSmart = newSmart;
                break;
        }
    }

    private static void ModifyRadioReceiver(RadioReceiver radio)
    {
        Console.WriteLine("\n=== Изменение характеристик радиоприёмника ===");
        Console.WriteLine("1) Изменить цену");
        Console.WriteLine("2) Изменить цвет");
        Console.WriteLine("3) Изменить диапазон");
        Console.WriteLine("4) Изменить частотный диапазон");
        Console.WriteLine("5) Изменить RDS");
        
        int choice = ReadInt("Выберите параметр для изменения: ", 1, 5);
        
        switch (choice)
        {
            case 1:
                decimal newPrice = ReadDecimal("Новая цена: ", 0m, 1_000_000m);
                radio.UpdatePrice(newPrice);
                break;
            case 2:
                string newColor = ReadNonEmpty("Новый цвет: ");
                radio.UpdateColor(newColor);
                break;
            case 3:
                string newBand = ReadNonEmpty("Новый диапазон (AM, FM, AM/FM, DAB): ");
                radio.UpdateBand(newBand);
                break;
            case 4:
                double minFreq = ReadDouble("Мин. частота (МГц 0.05-300): ", 0.05, 300.0);
                double maxFreq = ReadDouble("Макс. частота (МГц 0.05-300): ", 0.05, 300.0);
                radio.UpdateFrequencyRange(minFreq, maxFreq);
                break;
            case 5:
                bool newRds = ReadBool("RDS? (y/n): ");
                radio.HasRds = newRds;
                break;
        }
    }

    private static void ModifyBaseDevice(Device device)
    {
        Console.WriteLine("\n=== Изменение характеристик устройства ===");
        Console.WriteLine("1) Изменить цену");
        Console.WriteLine("2) Изменить цвет");
        
        int choice = ReadInt("Выберите параметр для изменения: ", 1, 2);
        
        switch (choice)
        {
            case 1:
                decimal newPrice = ReadDecimal("Новая цена: ", 0m, 1_000_000m);
                device.UpdatePrice(newPrice);
                break;
            case 2:
                string newColor = ReadNonEmpty("Новый цвет: ");
                device.UpdateColor(newColor);
                break;
        }
    }

    // Чтение и валидация пользовательского ввода
    private static Television CreateTelevisionFromInput()
    {
        string brand = ReadNonEmpty("Бренд: ");
        string model = ReadNonEmpty("Модель: ");
        string color = ReadNonEmpty("Цвет: ");
        decimal price = ReadDecimal("Цена: ", 0m, 1_000_000m);
        int size = ReadInt("Диагональ (дюймы 10-120): ", 10, 120);
        string resolution = ReadNonEmpty("Разрешение (например, 1080p, 4K): ");
        bool isSmart = ReadBool("Smart TV? (y/n): ");
        string panel = ReadNonEmpty("Тип панели (LED, OLED, QLED...): ");
        return new Television(brand, model, color, price, size, resolution, isSmart, panel);
    }

    private static RadioReceiver CreateRadioFromInput()
    {
        string brand = ReadNonEmpty("Бренд: ");
        string model = ReadNonEmpty("Модель: ");
        string color = ReadNonEmpty("Цвет: ");
        decimal price = ReadDecimal("Цена: ", 0m, 1_000_000m);
        string band = ReadNonEmpty("Диапазон (AM, FM, AM/FM, DAB): ");
        double minMHz = ReadDouble("Мин. частота (МГц 0.05-300): ", 0.05, 300.0);
        double maxMHz = ReadDouble("Макс. частота (МГц 0.05-300): ", 0.05, 300.0);
        bool hasRds = ReadBool("RDS? (y/n): ");
        return new RadioReceiver(brand, model, color, price, band, minMHz, maxMHz, hasRds);
    }

    private static string ReadNonEmpty(string prompt)
    {
        // Читает непустую строку
        while (true)
        {
            Console.Write(prompt);
            string? s = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(s))
            {
                return s.Trim();
            }
            Console.WriteLine("Значение не может быть пустым.");
        }
    }

    private static decimal ReadDecimal(string prompt, decimal min, decimal max)
    {
        // Читает десятичное число в заданных границах
        while (true)
        {
            Console.Write(prompt);
            string? s = Console.ReadLine();
            if (decimal.TryParse(s, NumberStyles.Number, CultureInfo.InvariantCulture, out var value) && value >= min && value <= max)
            {
                return value;
            }
            Console.WriteLine($"Введите число в диапазоне [{min}; {max}] (используйте точку как разделитель).");
        }
    }

    private static int ReadInt(string prompt, int min, int max)
    {
        // Читает целое число в заданных границах
        while (true)
        {
            Console.Write(prompt);
            string? s = Console.ReadLine();
            if (int.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out var value) && value >= min && value <= max)
            {
                return value;
            }
            Console.WriteLine($"Введите целое число в диапазоне [{min}; {max}].");
        }
    }

    private static double ReadDouble(string prompt, double min, double max)
    {
        // Читает число с плавающей точкой в заданных границах
        while (true)
        {
            Console.Write(prompt);
            string? s = Console.ReadLine();
            if (double.TryParse(s, NumberStyles.Number, CultureInfo.InvariantCulture, out var value) && value >= min && value <= max)
            {
                return value;
            }
            Console.WriteLine($"Введите число в диапазоне [{min}; {max}] (используйте точку как разделитель).");
        }
    }

    private static bool ReadBool(string prompt)
    {
        // Читает логическое значение (y/n, да/нет)
        while (true)
        {
            Console.Write(prompt);
            string? s = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(s))
            {
                continue;
            }
            s = s.Trim().ToLowerInvariant();
            if (s == "y" || s == "yes" || s == "д" || s == "да") return true;
            if (s == "n" || s == "no" || s == "н" || s == "нет") return false;
            Console.WriteLine("Введите y/n.");
        }
    }
}


