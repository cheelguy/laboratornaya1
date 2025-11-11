using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Reflection;

// Тесты для базового класса Device
[TestFixture]
public class DeviceTests
{
    // Тест конструктора по умолчанию - проверяет, что все поля инициализируются корректными значениями по умолчанию
    [Test]
    public void Device_Constructor_DefaultValues()
    {
        // Создаем устройство с параметрами по умолчанию
        var device = new Device();
        
        // Проверяем, что все поля установлены в значения по умолчанию
        Assert.That(device.Brand, Is.EqualTo("Generic"));
        Assert.That(device.Model, Is.EqualTo("Model"));
        Assert.That(device.Color, Is.EqualTo("Black"));
        Assert.That(device.Price, Is.EqualTo(0m));
    }

    // Тест конструктора с пользовательскими параметрами - проверяет корректную инициализацию всех полей
    [Test]
    public void Device_Constructor_CustomValues()
    {
        // Создаем устройство с пользовательскими параметрами
        var device = new Device("Samsung", "Galaxy", "White", 50000m);
        
        // Проверяем, что все поля установлены в переданные значения
        Assert.That(device.Brand, Is.EqualTo("Samsung"));
        Assert.That(device.Model, Is.EqualTo("Galaxy"));
        Assert.That(device.Color, Is.EqualTo("White"));
        Assert.That(device.Price, Is.EqualTo(50000m));
    }

    [Test]
    public void Device_Brand_EmptyValue_ThrowsException()
    {
        var device = new Device();
        // Попытка установить пустую строку должна вызвать исключение
        Assert.Throws<ArgumentException>(() => device.Brand = "");
    }

    [Test]
    public void Device_Price_NegativeValue_ThrowsException()
    {
        var device = new Device();
        // Попытка установить отрицательную цену должна вызвать исключение
        Assert.Throws<ArgumentOutOfRangeException>(() => device.Price = -100m);
    }

    [Test]
    public void Device_UpdatePrice_UpdatesCorrectly()
    {
        // Создаем устройство с начальной ценой
        var device = new Device("LG", "Monitor", "Black", 25000m);
        
        // Обновляем цену
        device.UpdatePrice(30000m);
        
        // Проверяем, что цена обновилась корректно
        Assert.That(device.Price, Is.EqualTo(30000m));
    }

    [Test]
    public void Device_PrintInfo_OutputsCorrectFormat()
    {
        // Создаем устройство для тестирования
        var device = new Device("Samsung", "Galaxy", "Blue", 50000m);
        
        // Перенаправляем вывод консоли в StringWriter для проверки
        var stringWriter = new System.IO.StringWriter();
        Console.SetOut(stringWriter);
        
        // Вызываем метод печати информации
        device.PrintInfo();
        
        // Получаем вывод и проверяем его содержимое
        string output = stringWriter.ToString();
        Assert.That(output, Does.Contain("Устройство: Samsung Galaxy"));
        
        // Восстанавливаем стандартный вывод консоли
        Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
    }
}

// Тесты для производного класса Television
[TestFixture]
public class TelevisionTests
{
    [Test]
    public void Television_Constructor_DefaultValues()
    {
        // Создаем телевизор с параметрами по умолчанию
        var tv = new Television();
        
        // Проверяем базовые поля (наследованные от Device)
        Assert.That(tv.Brand, Is.EqualTo("Generic"));
        Assert.That(tv.Model, Is.EqualTo("TV"));
        Assert.That(tv.Color, Is.EqualTo("Black"));
        Assert.That(tv.Price, Is.EqualTo(0m));
        
        // Проверяем специфичные для телевизора поля
        Assert.That(tv.ScreenSizeInches, Is.EqualTo(32));
        Assert.That(tv.Resolution, Is.EqualTo("1080p"));
        Assert.That(tv.IsSmart, Is.True);
        Assert.That(tv.PanelType, Is.EqualTo("LED"));
    }

    [Test]
    public void Television_Constructor_CustomValues()
    {
        // Создаем телевизор с пользовательскими параметрами
        var tv = new Television("Samsung", "Q80", "Black", 80000m, 55, "4K", true, "QLED");
        
        // Проверяем базовые поля
        Assert.That(tv.Brand, Is.EqualTo("Samsung"));
        Assert.That(tv.Model, Is.EqualTo("Q80"));
        Assert.That(tv.Color, Is.EqualTo("Black"));
        Assert.That(tv.Price, Is.EqualTo(80000m));
        
        // Проверяем специфичные для телевизора поля
        Assert.That(tv.ScreenSizeInches, Is.EqualTo(55));
        Assert.That(tv.Resolution, Is.EqualTo("4K"));
        Assert.That(tv.IsSmart, Is.True);
        Assert.That(tv.PanelType, Is.EqualTo("QLED"));
    }

    [Test]
    public void Television_ScreenSizeInches_TooSmall_ThrowsException()
    {
        var tv = new Television();
        // Попытка установить диагональ меньше 10 дюймов должна вызвать исключение
        Assert.Throws<ArgumentOutOfRangeException>(() => tv.ScreenSizeInches = 5);
    }

    [Test]
    public void Television_Resolution_EmptyValue_ThrowsException()
    {
        var tv = new Television();
        // Попытка установить пустое разрешение должна вызвать исключение
        Assert.Throws<ArgumentException>(() => tv.Resolution = "");
    }

    [Test]
    public void Television_UpdateScreenSize_UpdatesCorrectly()
    {
        // Создаем телевизор с начальной диагональю
        var tv = new Television("LG", "OLED", "Black", 100000m, 48, "4K", true, "OLED");
        
        // Обновляем диагональ экрана
        tv.UpdateScreenSize(65);
        
        // Проверяем, что диагональ обновилась корректно
        Assert.That(tv.ScreenSizeInches, Is.EqualTo(65));
    }

    [Test]
    public void Television_PrintInfo_OutputsCorrectFormat()
    {
        // Создаем телевизор для тестирования
        var tv = new Television("Sony", "Bravia", "Black", 120000m, 55, "4K", true, "OLED");
        
        // Перенаправляем вывод консоли в StringWriter для проверки
        var stringWriter = new System.IO.StringWriter();
        Console.SetOut(stringWriter);
        
        // Вызываем метод печати информации
        tv.PrintInfo();
        
        // Получаем вывод и проверяем его содержимое
        string output = stringWriter.ToString();
        Assert.That(output, Does.Contain("Телевизор: Sony Bravia"));
        Assert.That(output, Does.Contain("55"));
        Assert.That(output, Does.Contain("4K"));
        Assert.That(output, Does.Contain("smart: да"));
        
        // Восстанавливаем стандартный вывод консоли
        Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
    }
}

// Тесты для производного класса RadioReceiver
[TestFixture]
public class RadioReceiverTests
{
    [Test]
    public void RadioReceiver_Constructor_DefaultValues()
    {
        // Создаем радиоприемник с параметрами по умолчанию
        var radio = new RadioReceiver();
        
        // Проверяем базовые поля (наследованные от Device)
        Assert.That(radio.Brand, Is.EqualTo("Generic"));
        Assert.That(radio.Model, Is.EqualTo("Radio"));
        Assert.That(radio.Color, Is.EqualTo("Black"));
        Assert.That(radio.Price, Is.EqualTo(0m));
        
        // Проверяем специфичные для радиоприемника поля
        Assert.That(radio.Band, Is.EqualTo("FM"));
        Assert.That(radio.MinFrequencyMHz, Is.EqualTo(87.5));
        Assert.That(radio.MaxFrequencyMHz, Is.EqualTo(108.0));
        Assert.That(radio.HasRds, Is.True);
    }

    [Test]
    public void RadioReceiver_Constructor_CustomValues()
    {
        // Создаем радиоприемник с пользовательскими параметрами
        var radio = new RadioReceiver("Pioneer", "DEH-X8800BHS", "Black", 15000m, "AM/FM", 87.5, 108.0, true);
        
        // Проверяем базовые поля
        Assert.That(radio.Brand, Is.EqualTo("Pioneer"));
        Assert.That(radio.Model, Is.EqualTo("DEH-X8800BHS"));
        Assert.That(radio.Color, Is.EqualTo("Black"));
        Assert.That(radio.Price, Is.EqualTo(15000m));
        
        // Проверяем специфичные для радиоприемника поля
        Assert.That(radio.Band, Is.EqualTo("AM/FM"));
        Assert.That(radio.MinFrequencyMHz, Is.EqualTo(87.5));
        Assert.That(radio.MaxFrequencyMHz, Is.EqualTo(108.0));
        Assert.That(radio.HasRds, Is.True);
    }

    [Test]
    public void RadioReceiver_Band_ValidValues()
    {
        var radio = new RadioReceiver();
        
        // Тестируем различные допустимые значения диапазонов
        radio.Band = "AM";
        Assert.That(radio.Band, Is.EqualTo("AM"));
        
        radio.Band = "FM";
        Assert.That(radio.Band, Is.EqualTo("FM"));
        
        radio.Band = "AM/FM";
        Assert.That(radio.Band, Is.EqualTo("AM/FM"));
        
        radio.Band = "DAB";
        Assert.That(radio.Band, Is.EqualTo("DAB"));
    }

    [Test]
    public void RadioReceiver_Band_InvalidValue_ThrowsException()
    {
        var radio = new RadioReceiver();
        // Попытка установить недопустимый диапазон должна вызвать исключение
        Assert.Throws<ArgumentException>(() => radio.Band = "Invalid");
    }

    [Test]
    public void RadioReceiver_MinFrequencyMHz_TooLow_ThrowsException()
    {
        var radio = new RadioReceiver();
        // Попытка установить минимальную частоту меньше 0 должна вызвать исключение
        Assert.Throws<ArgumentOutOfRangeException>(() => radio.MinFrequencyMHz = -1.0);
    }

    [Test]
    public void RadioReceiver_MaxFrequencyMHz_TooHigh_ThrowsException()
    {
        var radio = new RadioReceiver();
        // Попытка установить максимальную частоту больше 300 МГц должна вызвать исключение
        Assert.Throws<ArgumentOutOfRangeException>(() => radio.MaxFrequencyMHz = 350.0);
    }

    [Test]
    public void RadioReceiver_UpdateFrequencyRange_UpdatesCorrectly()
    {
        // Создаем радиоприемник с начальными частотами
        var radio = new RadioReceiver("Kenwood", "KDC-X502", "Black", 8000m, "FM", 87.5, 108.0, false);
        
        // Обновляем диапазон частот
        radio.UpdateFrequencyRange(88.0, 107.5);
        
        // Проверяем, что частоты обновились корректно
        Assert.That(radio.MinFrequencyMHz, Is.EqualTo(88.0));
        Assert.That(radio.MaxFrequencyMHz, Is.EqualTo(107.5));
    }

    [Test]
    public void RadioReceiver_UpdateFrequencyRange_InvalidRange_ThrowsException()
    {
        var radio = new RadioReceiver();
        // Попытка установить максимальную частоту меньше минимальной должна вызвать исключение
        Assert.Throws<ArgumentException>(() => radio.UpdateFrequencyRange(100.0, 90.0));
    }

    [Test]
    public void RadioReceiver_PrintInfo_OutputsCorrectFormat()
    {
        // Создаем радиоприемник для тестирования
        var radio = new RadioReceiver("Alpine", "CDE-172BT", "Black", 12000m, "AM/FM", 87.5, 108.0, true);
        
        // Перенаправляем вывод консоли в StringWriter для проверки
        var stringWriter = new System.IO.StringWriter();
        Console.SetOut(stringWriter);
        
        // Вызываем метод печати информации
        radio.PrintInfo();
        
        // Получаем вывод и проверяем его содержимое
        string output = stringWriter.ToString();
        Assert.That(output, Does.Contain("Радиоприемник: Alpine CDE-172BT"));
        Assert.That(output, Does.Contain("AM/FM"));
        Assert.That(output, Does.Contain("87.5"));
        Assert.That(output, Does.Contain("108"));
        Assert.That(output, Does.Contain("RDS: да"));
        
        // Восстанавливаем стандартный вывод консоли
        Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
    }
}