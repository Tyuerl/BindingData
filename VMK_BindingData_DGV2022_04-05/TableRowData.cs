using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using VMK_BindingData_DGV2022_04_05.Annotations;

namespace VMK_BindingData_DGV2022_04_05
{

    enum Education
    {
        Bachelor,
        Magister,
        Specialist,
        Secondary
    }

    public class TableRowData : INotifyPropertyChanged
    {
        private int _id;
        private string _fname;
        private string _sname;
        //private DateTime _dateBirth;
        private bool _isMale;
        private double _salary;
        private short _delay;
        [DisplayName("Номер")]
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Фамилия")]
        public string Sname
        {
            get => _sname;
            set
            {
                if (value.Trim().Length == 0)
                    throw new BindingException("Фамилия", "Строка не должна быть пустой");
                _sname = value;
                OnPropertyChanged();
            }

        }
        [DisplayName("Имя")]
        public string Fname
        {
            get => _fname;
            set
            {
                if (value.Trim().Length == 0)
                    throw new BindingException("Имя", "Строка не должна быть пустой");
                _fname = value;
                OnPropertyChanged();
            }
        }
/*
        [DisplayName("Дата рождения")]
        public DateTime DateBirth
        {
            get => _dateBirth;
            set
            {
                if (value.Year < 14)
                    throw new BindingException("Дата рождения", "Работнику не может быть меньше 14 лет");
                _dateBirth = value;
                OnPropertyChanged();
            }
        }
        */
        [DisplayName("Мужчина?")]
        public bool IsMale
        {
            get => _isMale;
            set
            {
                _isMale = value;
            }
        }

        [DisplayName("Опозданий за квартал")]
        public short Delay
        {
            get => _delay;
            set
            {
                if (value < 0)
                    throw new BindingException("Число", "Должно быть >= нуля");
                _delay = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("З/п")]
        public double Salary
        {
            get => _salary;
            set
            {
               if (value <= 0)
                    throw new BindingException("Зарплата", "Зп не может быть <= 0");
                _salary = value;
                OnPropertyChanged();
            }
        }

        public TableRowData(int id, string fname, string sname, bool ismale, DateTime birthdate, double salary, short delay)
        {
            Id = id;
            Fname =  fname;
            Sname = sname;
            IsMale = ismale;
           // DateBirth = birthdate;
            Salary = salary;
            Delay = delay;
        }

        // Конструктор по умолчанию удобно использовать для создания новых значений
        public TableRowData()
        {
            // В нем необходимо предусмотреть, чтобы все свойства имели допустимые значения
            _id = 1;
            _fname = "-";
            _sname = "-";
            _isMale = true;
            //_dateBirth = new DateTime(1754, 01, 01);
            _salary = 1;
            _delay = 0;
        }

        // Метод для копирования объекта
        public TableRowData Copy()
        {
            TableRowData a = new TableRowData();

            a.Id = _id; 
            a.Fname = _fname;
            a.Sname = _sname;
            a.IsMale = _isMale;
            //a.DateBirth = _dateBirth;
            a.Salary = _salary;
            return (a);
        }
        
        // Метод копирования данного объекта в другой, указанный в параметре
        // (можно использовать для восстановления данных из резервной копии
        // при отказе пользователя от сохранения сделанных изменений)
        public void CopyTo(TableRowData? trd)
        {
            if (trd is not null)
            {
                trd.Fname = _fname;
                trd.Id = _id;
                trd.Sname = _sname;
                trd.IsMale = _isMale;
              //  trd.DateBirth = _dateBirth;
                trd.Salary = _salary;
            }
        }

        // Для автоматического обновления таблицы с данными
        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
